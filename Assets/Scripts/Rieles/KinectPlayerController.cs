using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MovementDetectionLibrary;

public class KinectPlayerController : MonoBehaviour
{
    public FullBody body;
    [SerializeField]
    bool debug = true;
    string debugString = "";
    public Text debugText;

    [SerializeField]
    bool autoMove = false;
    [SerializeField]
    float autoMoveSpeed = 1f;
    [SerializeField]
    float jumpThreshold = 0.15f;
    [SerializeField]
    float crouchThreshold = 0.3f;
    [SerializeField]
    float kneeUpThreshold = 0.3f;
    [SerializeField]
    float normalTimeBetweenKnees = 2f;
    [SerializeField]
    bool updateRootPosition = true;
    [SerializeField]
    bool updateRootX = true;
    [SerializeField]
    bool updateRootY = false;
    [SerializeField]
    bool updateRootZ = false;
    [SerializeField]
    Vector3 rootSpeedScaling = Vector3.one;
    [SerializeField]
    Vector3 rootOffset = Vector3.zero;
    [SerializeField]
    float minDistanceX = -2f;
    [SerializeField]
    float maxDistanceX = 2f;

    private Vector3 initialSpinePosition;
    private Vector3 spinePosition;    
    private bool initialPositionsAssigned = false;
    private bool shouldJump = false;
    private bool shouldCrouch = false;
    private Vector3 initialLeftKneePosition;
    private Vector3 initialRightKneePosition;
    private Vector3 leftKneePosition;
    private Vector3 rightKneePosition;
    private bool leftKneeUp= false;
    private bool rightKneeUp = false;
    private float timeBetweenKnees;
    private float normalizedTime;
    public enum Step { None, Left, Right };
    private Step lastStep = Step.None;

    //private bool useJog = true;
    private bool useCrouch = true;
    private bool useJump = true;
    private Vector3 move;

    private RUISInputManager inputManager;
    private RUISSkeletonManager skeletonManager;
    private RUISCoordinateSystem coordinateSystem;
    private RielesCharacter character;

    public enum bodyTrackingDeviceType
    {
        Kinect1,
        Kinect2,
        GenericMotionTracker
    }

    public bodyTrackingDeviceType bodyTrackingDevice = bodyTrackingDeviceType.Kinect1;

    public int bodyTrackingDeviceID = 0;
    public int playerId = 0;
    public bool switchToAvailableKinect = false;

    private Vector3 skeletonPosition = Vector3.zero;

    private float deltaTime = 0.03f;

    private KalmanFilter positionKalman;
    private double[] measuredPos = { 0, 0, 0 };
    private double[] pos = { 0, 0, 0 };
    private float positionNoiseCovariance = 100;

    void Awake()
    {
        inputManager = FindObjectOfType(typeof(RUISInputManager)) as RUISInputManager;

        if (inputManager)
        {
            if (switchToAvailableKinect)
            {
                if (bodyTrackingDevice == bodyTrackingDeviceType.Kinect1
                   && !inputManager.enableKinect && inputManager.enableKinect2)
                {
                    bodyTrackingDevice = bodyTrackingDeviceType.Kinect2;
                }
                else if (bodyTrackingDevice == bodyTrackingDeviceType.Kinect2
                        && !inputManager.enableKinect2 && inputManager.enableKinect)
                {
                    bodyTrackingDevice = bodyTrackingDeviceType.Kinect1;
                }
            }
        }

        coordinateSystem = FindObjectOfType(typeof(RUISCoordinateSystem)) as RUISCoordinateSystem;

        if (bodyTrackingDevice == bodyTrackingDeviceType.Kinect1)
            bodyTrackingDeviceID = RUISSkeletonManager.kinect1SensorID;
        if (bodyTrackingDevice == bodyTrackingDeviceType.Kinect2)
            bodyTrackingDeviceID = RUISSkeletonManager.kinect2SensorID;
        if (bodyTrackingDevice == bodyTrackingDeviceType.GenericMotionTracker)
            bodyTrackingDeviceID = RUISSkeletonManager.customSensorID;

        positionKalman = new KalmanFilter();
        positionKalman.initialize(3, 3);
    }

    void Start()
    {
        if (skeletonManager == null)
        {
            skeletonManager = FindObjectOfType(typeof(RUISSkeletonManager)) as RUISSkeletonManager;
            if (!skeletonManager)
                Debug.Log("The scene is missing " + typeof(RUISSkeletonManager) + " script!");
        }

        character = GetComponent<RielesCharacter>();

        skeletonPosition = transform.localPosition;

        //timeBetweenKnees = normalTimeBetweenKnees;
        move = Vector3.zero;
    }

    void Update()
    {
        deltaTime = Time.deltaTime; //1.0f / vr.hmd_DisplayFrequency;
        //Debug.Log(skeletonManager.skeletons[bodyTrackingDeviceID, playerId].isTracking);

        // Update skeleton based on data fetched from skeletonManager
        if (GameManagerRieles.gm != null && GameManagerRieles.gm.isPlaying && skeletonManager != null 
            && skeletonManager.skeletons[bodyTrackingDeviceID, playerId] != null && skeletonManager.skeletons[bodyTrackingDeviceID, playerId].isTracking)
        {
            if (updateRootPosition)
            {
                UpdateSkeletonPosition();                
                if (minDistanceX <= skeletonPosition.x && skeletonPosition.x <= maxDistanceX)
                    transform.localPosition = Vector3.Scale(skeletonPosition, rootSpeedScaling); // Root speed scaling is applied here
            }

            if (!initialPositionsAssigned)
            {
                initialSpinePosition = GetCurrentSpinePosition();
                initialLeftKneePosition = GetCurrentLeftKneePosition();
                initialRightKneePosition = GetCurrentRightKneePosition();
                initialPositionsAssigned = true;
            }

            spinePosition = GetCurrentSpinePosition();

            if (!shouldJump)
            {
                shouldJump = useJump && (spinePosition.y - initialSpinePosition.y) >= jumpThreshold;
            }

            shouldCrouch = useCrouch && (initialSpinePosition.y - spinePosition.y) >= crouchThreshold;

            leftKneeUp = !autoMove && (leftKneePosition.y - initialLeftKneePosition.y) >= kneeUpThreshold;
            rightKneeUp = !autoMove && (rightKneePosition.y - initialRightKneePosition.y) >= kneeUpThreshold;

            if (lastStep == Step.None && leftKneeUp)
            {
                lastStep = Step.Left;
                timeBetweenKnees = 0;
            }
            else if (lastStep == Step.None && rightKneeUp)
            {
                lastStep = Step.Right;
                timeBetweenKnees = 0;
            }
            else if (character.IsGrounded() && timeBetweenKnees > normalTimeBetweenKnees)
            {
                lastStep = Step.None;
                move.z = 0;
            }
            else if ((lastStep == Step.Left && rightKneeUp) || (lastStep == Step.Right && leftKneeUp))
            {
                //if (timeBetweenKnees > normalTimeBetweenKnees)
                //    timeBetweenKnees = normalTimeBetweenKnees;

                normalizedTime = 1;
                //normalizedTime = 1 - normalizedTime;

                //if (debug)
                //    debugString += "\nKnee Delta => " + timeBetweenKnees.ToString("0.00");

                move.z = normalizedTime;

                if (leftKneeUp)
                    lastStep = Step.Left;
                else
                    lastStep = Step.Right;

                timeBetweenKnees = 0;
            }            

            leftKneePosition = GetCurrentLeftKneePosition();
            rightKneePosition = GetCurrentRightKneePosition();
            
            if (debug)
                debugString += ""
                    //+ "(" + spinePosition.x.ToString("0.00") + ", " + spinePosition.y.ToString("0.00") + ", " + spinePosition.z.ToString("0.00") + ")"
                    //+ "\n" + "(" + initialSpinePosition.x.ToString("0.00") + ", " + initialSpinePosition.y.ToString("0.00") + ", " + initialSpinePosition.z.ToString("0.00") + ")"
                    //+ "\n" + (initialSpinePosition.y - spinePosition.y).ToString("0.00") + " => Crouch: " + shouldCrouch
                    //+ "\n" + (spinePosition.y - initialSpinePosition.y).ToString("0.00") + " => Jump: " + shouldJump
                    + "\n" + leftKneePosition.ToString("0.00")
                    //+ "\n" + BodyPointPositionToVector3(body.bodyMovements.bodyPointsCollection[BodyParts.KneeLeft].getCurrentPosition()).ToString("0.00")
                    + "\n" + rightKneePosition.ToString("0.00")
                    //+ "\n" + BodyPointPositionToVector3(body.bodyMovements.bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition()).ToString("0.00")
                    ;

            if (autoMove)
                move.z = autoMoveSpeed;

            if (shouldCrouch)
                move.z = 0;

            character.Move(move, shouldCrouch, shouldJump);
            shouldJump = false;

            if (character.IsGrounded())
                timeBetweenKnees += Time.deltaTime;
        }

        if (debug)
        {
            debugText.text = debugString;
            debugString = "";
        }
    }

    public void SetParameters(bool useJog, float jogThreshold, float jogTime, bool useCrouch, float crouchThreshold, bool useJump, float jumpThreshold)
    {
        autoMove = !useJog;
        kneeUpThreshold = jogThreshold;
        normalTimeBetweenKnees = jogTime;

        this.useCrouch = useCrouch;
        this.crouchThreshold = crouchThreshold;

        this.useJump = useJump;
        this.jumpThreshold = jumpThreshold;
    }

    //gets the main position of the skeleton inside the world, the rest of the joint positions will be calculated in relation to this one
    private void UpdateSkeletonPosition()
    {
        Vector3 newRootPosition = skeletonManager.skeletons[bodyTrackingDeviceID, playerId].root.position;

        measuredPos[0] = newRootPosition.x;
        measuredPos[1] = newRootPosition.y;
        measuredPos[2] = newRootPosition.z;
        positionKalman.setR(deltaTime * positionNoiseCovariance); // HACK doesn't take into account Kinect's own update deltaT
        positionKalman.predict();
        positionKalman.update(measuredPos);
        pos = positionKalman.getState();

        //skeletonPosition = new Vector3((float)pos[0], (float)pos[1], (float)pos[2]);
        if (updateRootX)
            skeletonPosition.x = (float)pos[0] + rootOffset.x;
        else
            skeletonPosition.x = transform.position.x;

        if (updateRootY)
            skeletonPosition.y = (float)pos[1] + rootOffset.y;
        else
            skeletonPosition.y = transform.position.y;

        if (updateRootZ)
            skeletonPosition.z = (float)pos[2] + rootOffset.z;
        else
            skeletonPosition.z = transform.position.z;
    }

    private Vector3 GetCurrentSpinePosition()
    {
        return skeletonManager.skeletons[bodyTrackingDeviceID, playerId].root.position;
    }

    private Vector3 GetCurrentLeftKneePosition()
    {
        return skeletonManager.skeletons[bodyTrackingDeviceID, playerId].leftKnee.position;
    }

    private Vector3 GetCurrentRightKneePosition()
    {
        return skeletonManager.skeletons[bodyTrackingDeviceID, playerId].rightKnee.position;
    }

    public Vector3 BodyPointPositionToVector3(BodyPointPosition bp)
    {
        return new Vector3(bp.x, bp.y, bp.z);
    }

    public bool UpdateRootPosition
    {
        get
        {
            return updateRootPosition;
        }

        set
        {
            updateRootPosition = value;
        }
    }
}

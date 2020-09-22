using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementDetectionLibrary;

public class Kick : MonoBehaviour
{
    public GameObject ball;
    public float speed;
    public AudioSource hitSound;

    public RUISSkeletonController skeleton;
    public FullBody mdl;
    public Transform leftHip;
    public Transform rightHip;
    public Transform leftFoot;
    public Transform rightFoot;

    public float firstFrontAngle;
    public float secondFrontAngle;
    public float thirdFrontAngle;
    public float firstBackAngle;
    public float secondBackAngle;
    public float thirdBackAngle;

    public float totalTimeToFrontKick;
    private float currentTimeToFrontKick;
    public bool kicking;
    public bool kicked;

    public GameObject indicator;
    public float indicatorSpeed;
    public GameObject barRed;
    public GameObject barYellow;
    public GameObject barGreen;

    private Vector3 ballInitialPosition;
    private Vector3 indicatorInitialPosition;
    private RectTransform indicatorTransform;
    private RectTransform barRedTransform;
    private RectTransform barYellowTransform;
    private RectTransform barGreenTransform;

    private float hipLeftAngle;
    private float hipRightAngle;
    private float kneeLeftAngle;
    private float kneeRightAngle;

    private Vector2 legLeftOrientation;
    private Vector2 legRightOrientation;

    private Vector3 eulerRotationTemp;
    private Vector2 tempPosition;
    private float tempFloat;
    private Vector2 leftVector;
    private Vector2 rightVector;

    private int calculatedTarget;
    private Vector3 calculatedTargetPosition;

    private bool firstThreshold;
    private bool secondThreshold;
    private bool thirdThreshold;
    private bool firstOrientation;
    private bool secondOrientation;
    private bool thirdOrientation;

    private float bestLeftHipFrontAngle;
    private float bestRightHipFrontAngle;
    private float bestLeftHipBackAngle;
    private float bestRightHipBackAngle;

    // Use this for initialization
    void Start ()
    {
        ballInitialPosition = ball.transform.position;
        indicatorInitialPosition = indicator.GetComponent<RectTransform>().position;
        indicatorTransform = indicator.GetComponent<RectTransform>();
        barRedTransform = barRed.GetComponent<RectTransform>();
        barYellowTransform = barYellow.GetComponent<RectTransform>();
        barGreenTransform = barGreen.GetComponent<RectTransform>();

        hipLeftAngle = 0f;
        hipRightAngle = 0f;
        kneeLeftAngle = 0f;
        kneeRightAngle = 0f;
        legLeftOrientation = new Vector2();
        legRightOrientation = new Vector2();

        firstThreshold = false;
        secondThreshold = false;
        thirdThreshold = false;
        firstOrientation = false;
        secondOrientation = false;
        thirdOrientation = false;

        tempPosition = new Vector2();

        leftVector = new Vector2(leftHip.position.x, Mathf.Abs(leftHip.position.z));
        rightVector = new Vector2(rightHip.position.x, Mathf.Abs(rightHip.position.z));

        GameObject go = GameObject.Find("Porl KinectKinect");
        skeleton = go.GetComponent<RUISSkeletonController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (kicking && mdl.bodyMovements.bodyPointsCollection != null && GameManagerTiroLibre.gm.isPlaying && GameManagerTiroLibre.gm.targetReady)
        {
            switch (GameManagerTiroLibre.gm.currentMovement)
            {
                case GameManagerTiroLibre.LegMovements.FrontLeftLeg:
                    {
                        //Debug.Log("entra a front left leg");
                        setHipLeftAngle();

                        if (!thirdThreshold && hipLeftAngle > thirdFrontAngle && GameManagerTiroLibre.gm.useHigh)
                        {
                            thirdThreshold = true;
                            currentTimeToFrontKick = totalTimeToFrontKick;
                            if (!thirdOrientation)
                            {
                                setLeftLegOrientation();
                                Debug.Log(legLeftOrientation);
                                thirdOrientation = true;
                            }

                            if (hipLeftAngle > bestLeftHipFrontAngle)
                                bestLeftHipFrontAngle = hipLeftAngle;
                        }
                        else if (!secondThreshold && hipLeftAngle > secondFrontAngle && GameManagerTiroLibre.gm.useMedium)
                        {
                            secondThreshold = true;
                            currentTimeToFrontKick = totalTimeToFrontKick;
                            if (!thirdOrientation && !secondOrientation)
                            {
                                setLeftLegOrientation();
                                Debug.Log(legLeftOrientation);
                                secondOrientation = true;
                            }

                            if (hipLeftAngle > bestLeftHipFrontAngle)
                                bestLeftHipFrontAngle = hipLeftAngle;
                        }
                        else if (!firstThreshold && hipLeftAngle > firstFrontAngle && GameManagerTiroLibre.gm.useLow)
                        {
                            firstThreshold = true;
                            currentTimeToFrontKick = totalTimeToFrontKick;
                            if (!thirdOrientation && !secondOrientation && !firstOrientation)
                            {
                                setLeftLegOrientation();
                                Debug.Log(legLeftOrientation);
                                firstOrientation = true;
                            }

                            if (hipLeftAngle > bestLeftHipFrontAngle)
                                bestLeftHipFrontAngle = hipLeftAngle;
                        }

                        if (thirdThreshold || secondThreshold || firstThreshold)
                        {
                            if (currentTimeToFrontKick <= 0)
                                KickBall();
                            else
                                currentTimeToFrontKick -= Time.deltaTime;
                        }

                        break;
                    }
                case GameManagerTiroLibre.LegMovements.FrontRightLeg:
                    {
                        //Debug.Log("entra a front right leg");
                        setHipRightAngle();

                        if (!thirdThreshold && hipRightAngle > thirdFrontAngle && GameManagerTiroLibre.gm.useHigh)
                        {
                            thirdThreshold = true;
                            currentTimeToFrontKick = totalTimeToFrontKick;
                            if (!thirdOrientation)
                            {
                                setRightLegOrientation();
                                Debug.Log(legRightOrientation);
                                thirdOrientation = true;
                            }

                            if (hipRightAngle > bestRightHipFrontAngle)
                                bestRightHipFrontAngle = hipRightAngle;
                        }
                        else if (!secondThreshold && hipRightAngle > secondFrontAngle && GameManagerTiroLibre.gm.useMedium)
                        {
                            secondThreshold = true;
                            currentTimeToFrontKick = totalTimeToFrontKick;
                            if (!thirdOrientation && !secondOrientation)
                            {
                                setRightLegOrientation();
                                Debug.Log(legRightOrientation);
                                secondOrientation = true;
                            }

                            if (hipRightAngle > bestRightHipFrontAngle)
                                bestRightHipFrontAngle = hipRightAngle;
                        }
                        else if (!firstThreshold && hipRightAngle > firstFrontAngle && GameManagerTiroLibre.gm.useLow)
                        {
                            firstThreshold = true;
                            currentTimeToFrontKick = totalTimeToFrontKick;
                            if (!thirdOrientation && !secondOrientation && !firstOrientation)
                            {
                                setRightLegOrientation();
                                Debug.Log(legRightOrientation);
                                firstOrientation = true;
                            }

                            if (hipRightAngle > bestRightHipFrontAngle)
                                bestRightHipFrontAngle = hipRightAngle;
                        }

                        if (thirdThreshold || secondThreshold || firstThreshold)
                        {
                            if (currentTimeToFrontKick <= 0)
                                KickBall();
                            else
                                currentTimeToFrontKick -= Time.deltaTime;
                        }

                        break;
                    }
                case GameManagerTiroLibre.LegMovements.BackLeftLeg:
                    {
                        //Debug.Log("entra a back left leg");
                        setHipLeftAngle();

                        if (hipLeftAngle > thirdBackAngle && GameManagerTiroLibre.gm.useHigh)
                        {
                            thirdThreshold = true;
                            if (!thirdOrientation)
                            {
                                setLeftLegOrientation();
                                Debug.Log(legLeftOrientation);
                                thirdOrientation = true;
                            }

                            if (hipLeftAngle > bestLeftHipBackAngle)
                                bestLeftHipBackAngle = hipLeftAngle;
                        }
                        else if (hipLeftAngle > secondBackAngle && GameManagerTiroLibre.gm.useMedium)
                        {
                            secondThreshold = true;
                            if (!thirdOrientation && !secondOrientation)
                            {
                                setLeftLegOrientation();
                                Debug.Log(legLeftOrientation);
                                secondOrientation = true;
                            }

                            if (hipLeftAngle > bestLeftHipBackAngle)
                                bestLeftHipBackAngle = hipLeftAngle;
                        }
                        else if (hipLeftAngle > firstBackAngle && GameManagerTiroLibre.gm.useLow)
                        {
                            firstThreshold = true;
                            if (!thirdOrientation && !secondOrientation && !firstOrientation)
                            {
                                setLeftLegOrientation();
                                Debug.Log(legLeftOrientation);
                                firstOrientation = true;
                            }

                            if (hipLeftAngle > bestLeftHipBackAngle)
                                bestLeftHipBackAngle = hipLeftAngle;
                        }
                        else if (hipLeftAngle < 15 && (thirdThreshold || secondThreshold || firstThreshold))
                        {
                            KickBall();
                        }

                        break;
                    }
                case GameManagerTiroLibre.LegMovements.BackRightLeg:
                    {
                        //Debug.Log("entra a back right leg");
                        setHipRightAngle();

                        if (hipRightAngle > thirdBackAngle && GameManagerTiroLibre.gm.useHigh)
                        {
                            thirdThreshold = true;
                            if (!thirdOrientation)
                            {
                                setRightLegOrientation();
                                Debug.Log(legRightOrientation);
                                thirdOrientation = true;
                            }

                            if (hipRightAngle > bestRightHipBackAngle)
                                bestRightHipBackAngle = hipRightAngle;
                        }
                        else if (hipRightAngle > secondBackAngle && GameManagerTiroLibre.gm.useMedium)
                        {
                            secondThreshold = true;
                            if (!thirdOrientation && !secondOrientation)
                            {
                                setRightLegOrientation();
                                Debug.Log(legRightOrientation);
                                secondOrientation = true;
                            }

                            if (hipRightAngle > bestRightHipFrontAngle)
                                bestRightHipFrontAngle = hipRightAngle;
                        }
                        else if (hipRightAngle > firstBackAngle && GameManagerTiroLibre.gm.useLow)
                        {
                            firstThreshold = true;
                            if (!thirdOrientation && !secondOrientation && !firstOrientation)
                            {
                                setRightLegOrientation();
                                Debug.Log(legRightOrientation);
                                firstOrientation = true;
                            }

                            if (hipRightAngle > bestRightHipBackAngle)
                                bestRightHipBackAngle = hipRightAngle;
                        }
                        else if (hipRightAngle < 15 && (thirdThreshold || secondThreshold || firstThreshold))
                        {
                            KickBall();
                        }

                        break;
                    }
                default:
                    break;
            }      
        }
        else if (kicked)
        {
            ball.transform.position = Vector3.MoveTowards(ball.transform.position, calculatedTargetPosition, speed * Time.deltaTime);
            //Debug.Log(GameManagerChuta.gm.getCurrentTargets()[calculatedTarget].name);
        }

        if (thirdThreshold)
            indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, barRedTransform.position, indicatorSpeed * Time.deltaTime);
        else if (secondThreshold)
            indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, barYellowTransform.position, indicatorSpeed * Time.deltaTime);
        else if (firstThreshold)
            indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, barGreenTransform.position, indicatorSpeed * Time.deltaTime);
        else
            indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, indicatorInitialPosition, indicatorSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("entra a trigger");

        if (other.gameObject.tag == "Target")
        {
            Debug.Log("entra a target");
            TargetCollision();
        }
        else if (kicked && other.gameObject.tag == "Wall")
        {
            Debug.Log("entra a wall");
            WallCollision();
        }
    }

    public void KickBall()
    {
        kicking = false;

        int pos1 = 0;
        int pos2 = 1;

        switch (GameManagerTiroLibre.gm.currentMovement)
        {
            case GameManagerTiroLibre.LegMovements.FrontLeftLeg:
                {
                    if (legLeftOrientation.y > (leftHip.position.z + 0.2f))
                    {
                        if (legLeftOrientation.x > (leftHip.position.x + 0.02f))
                            pos1 = 2;
                        else if (legLeftOrientation.x < (leftHip.position.x - 0.11f))
                            pos1 = 0;
                        else
                            pos1 = 1;
                    }
                    else
                    {
                        ClearKick();
                        return;
                    }
                    break;
                }
            case GameManagerTiroLibre.LegMovements.FrontRightLeg:
                {
                    if (legRightOrientation.y > (rightHip.position.z + 0.2f))
                    {
                        if (legRightOrientation.x > (rightHip.position.x + 0.11f))
                            pos1 = 2;
                        else if (legRightOrientation.x < (rightHip.position.x - 0.02f))
                            pos1 = 0;
                        else
                            pos1 = 1;
                    }
                    else
                    {
                        ClearKick();
                        return;
                    }
                    break;
                }
            case GameManagerTiroLibre.LegMovements.BackLeftLeg:
                {
                    if (legLeftOrientation.y < (leftHip.position.z - 0.02f))
                    {
                        if (legLeftOrientation.x > (leftHip.position.x + 0.01f))
                            pos1 = 0;
                        else if (legLeftOrientation.x < (leftHip.position.x - 0.07f))
                            pos1 = 2;
                        else
                            pos1 = 1;
                    }
                    else
                    {
                        ClearKick();
                        return;
                    }
                    break;
                }
            case GameManagerTiroLibre.LegMovements.BackRightLeg:
                {
                    if (legRightOrientation.y < (rightHip.position.z - 0.02f))
                    {
                        if (legRightOrientation.x > (rightHip.position.x + 0.07f))
                            pos1 = 0;
                        else if (legRightOrientation.x < (rightHip.position.x - 0.01f))
                            pos1 = 2;
                        else
                            pos1 = 1;
                    }
                    else
                    {
                        ClearKick();
                        return;
                    }
                    break;
                }
            default:
                break;
        }

        if (thirdThreshold)
            pos2 = 6;
        else if (secondThreshold)
            pos2 = 3;
        else
            pos2 = 0;

        calculatedTarget = pos1 + pos2;
        calculatedTargetPosition = GameManagerTiroLibre.gm.getTargetPosition(calculatedTarget);
        Debug.Log("target: " + calculatedTarget);
                
        kicked = true;
        hitSound.Play();
        //GameManagerTiroLibre.gm.targetReady = false;
    }

    public void TargetCollision()
    {
        kicked = false;
        ball.transform.position = ballInitialPosition;
        hipLeftAngle = 0f;
        hipRightAngle = 0f;
        firstThreshold = false;
        secondThreshold = false;
        thirdThreshold = false;
        firstOrientation = false;
        secondOrientation = false;
        thirdOrientation = false;
        GameManagerTiroLibre.gm.NextMovement();
        kicking = true;        
    }

    public void WallCollision()
    {
        kicked = false;
        ball.transform.position = ballInitialPosition;
        hipLeftAngle = 0f;
        hipRightAngle = 0f;
        firstThreshold = false;
        secondThreshold = false;
        thirdThreshold = false;
        firstOrientation = false;
        secondOrientation = false;
        thirdOrientation = false;
        if (GameManagerTiroLibre.gm.changeMovement)
        {
            GameManagerTiroLibre.gm.EnableTarget(GameManagerTiroLibre.gm.currentTarget, false);
            GameManagerTiroLibre.gm.NextMovement();
        }
        kicking = true;
    }

    public void ClearKick()
    {
        kicked = false;
        ball.transform.position = ballInitialPosition;
        hipLeftAngle = 0f;
        hipRightAngle = 0f;
        firstThreshold = false;
        secondThreshold = false;
        thirdThreshold = false;
        firstOrientation = false;
        secondOrientation = false;
        thirdOrientation = false;
        kicking = true;
    }

    public void setHipLeftAngle()
    {        
        hipLeftAngle = (float)mdl.bodyMovements.hipLeftExtMovement();
        //setLegLeftOrientation();
    }

    public void setHipRightAngle()
    {
        hipRightAngle = (float)mdl.bodyMovements.hipRigthExtMovement();
        //setLegRightOrientation();
    }

    public void setKneeLeftAngle()
    {
        tempFloat = (float)mdl.bodyMovements.kneeLeftMovement();        
    }

    public void setKneeRightAngle()
    {
        tempFloat = (float)mdl.bodyMovements.kneeRigthMovement();
    }

    public void setLeftLegOrientation()
    {
        legLeftOrientation.x = leftFoot.position.x;
        legLeftOrientation.y = leftFoot.position.z;
        Debug.Log(legLeftOrientation.y + " --- " + leftHip.position.z);
    }

    public void setRightLegOrientation()
    {
        legRightOrientation.x = rightFoot.position.x;
        legRightOrientation.y = rightFoot.position.z;
        Debug.Log(legRightOrientation.y + " --- " + rightHip.position.z);
    }

    public float BestLeftHipFrontAngle
    {
        get
        {
            return bestLeftHipFrontAngle;
        }

        set
        {
            bestLeftHipFrontAngle = value;
        }
    }

    public float BestRightHipFrontAngle
    {
        get
        {
            return bestRightHipFrontAngle;
        }

        set
        {
            bestRightHipFrontAngle = value;
        }
    }

    public float BestLeftHipBackAngle
    {
        get
        {
            return bestLeftHipBackAngle;
        }

        set
        {
            bestLeftHipBackAngle = value;
        }
    }

    public float BestRightHipBackAngle
    {
        get
        {
            return bestRightHipBackAngle;
        }

        set
        {
            bestRightHipBackAngle = value;
        }
    }

    public Vector3 CalculatedTargetPosition
    {
        get
        {
            return calculatedTargetPosition;
        }

        set
        {
            calculatedTargetPosition = value;
        }
    }
}

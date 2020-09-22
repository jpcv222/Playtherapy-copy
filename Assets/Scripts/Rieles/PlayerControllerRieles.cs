using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControllerRieles : MonoBehaviour
{
    public static PlayerControllerRieles pcr;

    [SerializeField]
    bool autoMove = false;
    [SerializeField]
    bool updateX = true;
    [SerializeField]
    float horizontalSpeedMultiplier = 1f;
    [SerializeField]
    float minDistanceX = -2f;
    [SerializeField]
    float maxDistanceX = 2f;

    private RielesCharacter character; // A reference to the ThirdPersonCharacter on the object
    private Transform cam;                  // A reference to the main camera in the scenes transform
    private Vector3 camForward;             // The current forward direction of the camera
    private Vector3 move;                   // the world-relative desired move direction, calculated from the camForward and user input.
    private bool jump;
    private bool crouch;
    private float h;
    private float v;
    private Vector3 newPosition;   

    // Use this for initialization
    void Start()
    {
        if (pcr == null)
            pcr = gameObject.GetComponent<PlayerControllerRieles>();

        // get the transform of the main camera
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        character = GetComponent<RielesCharacter>();
        move = Vector3.zero;
        crouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerRieles.gm != null && GameManagerRieles.gm.isPlaying)
        {
            if (!jump)
            {
                jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (updateX && Input.GetKey(KeyCode.LeftArrow))
                h = -1;
            else if (updateX && Input.GetKey(KeyCode.RightArrow))
                h = 1;
            else
                h = 0;

            newPosition = transform.position;
            newPosition.x += h * horizontalSpeedMultiplier * Time.deltaTime;
            if (minDistanceX <= newPosition.x && newPosition.x <= maxDistanceX)
                transform.position = newPosition;
            else
                newPosition = transform.position;

            if (autoMove)
                v = 1;
            else
                v = CrossPlatformInputManager.GetAxis("Vertical");

            crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (cam != null && !crouch)
            {
                // calculate camera relative direction to move:
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                move = (camForward * v);
            }
            else if (!crouch)
            {
                // we use world-relative directions in the case of no main camera
                move = (Vector3.forward * v);
            }
            else
            {
                move = Vector3.zero;
            }

            // pass all parameters to the character control script
            character.Move(move, crouch, jump);
            jump = false;
        }        
    }

    public bool Crouch
    {
        get
        {
            return crouch;
        }

        set
        {
            crouch = value;
        }
    }

    public bool UpdateX
    {
        get
        {
            return updateX;
        }

        set
        {
            updateX = value;
        }
    }

    public bool AutoMove
    {
        get
        {
            return autoMove;
        }

        set
        {
            autoMove = value;
        }
    }
}

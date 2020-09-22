using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AvatarControllerRieles : MonoBehaviour
{
    [SerializeField]
    float speed = 4f;
    [SerializeField]
    float jumpPower = 6f;
    [Range(1f, 4f)]
    [SerializeField]
    float gravityMultiplier = 2f;
    [SerializeField]
    float groundCheckDistance = 0.3f;

    private RUISSkeletonController skeleton;

    private Vector3 newPosition;

    private bool crouch;
    private bool jump;

    private Vector3 groundNormal;
    private const float k_Half = 0.5f;
    private bool isGrounded;
    private bool crouching;
    private float origGroundCheckDistance;
    private CapsuleCollider capsule;
    private Rigidbody _Rigidbody;
    private float capsuleHeight;
    private Vector3 capsuleCenter;

    // Use this for initialization
    void Start ()
    {
        //GameObject go = GameObject.Find("Porl KinectKinect");
        //skeleton = go.GetComponent<RUISSkeletonController>();

        _Rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;
        origGroundCheckDistance = groundCheckDistance;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition = transform.position;
            newPosition.z += speed * Time.deltaTime;
            transform.position = newPosition;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition = transform.position;
            newPosition.x -= speed * Time.deltaTime;
            transform.position = newPosition;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition = transform.position;
            newPosition.x += speed * Time.deltaTime;
            transform.position = newPosition;
        }

        crouch = Input.GetKey(KeyCode.C);
        jump = CrossPlatformInputManager.GetButtonDown("Jump");

        Move(Vector3.zero, crouch, jump);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            TrackSpawner.ts.SpawnNextTrack();
        }
        else if (other.gameObject.CompareTag("Obstacle Rieles"))
        {
            GameManagerRieles.gm.isPlaying = false;
            GameManagerRieles.gm.isGameOver = true;
            Debug.Log("obstacle hit");
        }
    }

    public void Move(Vector3 move, bool crouch, bool jump)
    {

        // convert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired
        // direction.
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, groundNormal);

        // control and velocity handling is different when grounded and airborne:
        if (isGrounded)
        {
            HandleGroundedMovement(crouch, jump);
        }
        else
        {
            HandleAirborneMovement();
        }

        ScaleCapsuleForCrouching(crouch);
    }

    private void ScaleCapsuleForCrouching(bool crouch)
    {
        if (isGrounded && crouch)
        {
            if (crouching) return;
            capsule.height = capsule.height / 2f;
            //capsule.height = 0;
            capsule.center = capsule.center / 2f;
            crouching = true;
        }
        else
        {
            Ray crouchRay = new Ray(_Rigidbody.position + Vector3.up * capsule.radius * k_Half, Vector3.up);
            float crouchRayLength = capsuleHeight - capsule.radius * k_Half;
            if (Physics.SphereCast(crouchRay, capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                crouching = true;
                return;
            }
            capsule.height = capsuleHeight;
            capsule.center = capsuleCenter;
            crouching = false;
        }
    }

    private void HandleGroundedMovement(bool crouch, bool jump)
    {
        // check whether conditions are right to allow a jump:
        if (jump && !crouch)
        {
            // jump!
            _Rigidbody.velocity = new Vector3(_Rigidbody.velocity.x, jumpPower, _Rigidbody.velocity.z);
            isGrounded = false;
            groundCheckDistance = 0.1f;
        }
    }

    private void HandleAirborneMovement()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
        //m_Rigidbody.AddForce(Physics.gravity);

        groundCheckDistance = _Rigidbody.velocity.y < 0 ? origGroundCheckDistance : 0.01f;
    }

    private void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
        {
            groundNormal = hitInfo.normal;
            isGrounded = true;
        }
        else if (!crouching)
        {
            isGrounded = false;
            groundNormal = Vector3.up;
        }
    }
}

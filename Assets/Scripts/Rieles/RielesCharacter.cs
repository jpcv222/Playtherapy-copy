using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class RielesCharacter : MonoBehaviour
{
    //[SerializeField]
    //float movingTurnSpeed = 360;
    //[SerializeField]
    //float stationaryTurnSpeed = 180;
    [SerializeField]
    float jumpPower = 5f;
    [Range(1f, 4f)]
    [SerializeField]
    float gravityMultiplier = 2f;
    [SerializeField]
    float runCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    [SerializeField]
    float forwardSpeedMultiplier = 1f;
    [SerializeField]
    float animSpeedMultiplier = 1f;
    [SerializeField]
    float groundCheckDistance = 0.3f;

    Rigidbody _Rigidbody;
    Animator _Animator;
    bool isGrounded;
    float origGroundCheckDistance;
    const float k_Half = 0.5f;
    //float turnAmount;
    float forwardAmount;
    Vector3 groundNormal;
    float capsuleHeight;
    Vector3 capsuleCenter;
    CapsuleCollider capsule;
    bool crouching;

    //sound
    public AudioSource leftFootSound;
    public AudioSource rightFootSound;
    public AudioSource jumpSound;

    // horizontal movement
    //public float horizontalMovementMagnitude;
    //public float horizontalSpeed;
    //private float targetX;
    //private Vector3 newPositionHorizontal;
    //private float currentLane;
    //private float horizontalMovement;
    //private bool turning;

    // roll (crouch)
    //public float rollMovementMagnitude;
    //public float rollSpeed;
    //public float rollDistanceSpeed;
    //private float targetZ;
    //private Vector3 newPositionRoll;
    //private bool rolling;


    void Start()
    {
        _Animator = GetComponent<Animator>();
        _Rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;

        _Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        _Rigidbody.useGravity = false;
        origGroundCheckDistance = groundCheckDistance;

        //horizontalMovement = 0;
        //currentLane = 0;
        //turning = false;
        //rolling = false;
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
        //m_TurnAmount = Mathf.Atan2(move.x, move.z);
        forwardAmount = move.z;

        //ApplyExtraTurnRotation();   

        //Turn()        

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
        //PreventStandingInLowHeadroom();

        // send input and other state parameters to the animator
        UpdateAnimator(move);

        //Roll(crouch);
    }
    /*
    void Turn()
    {
        if (!turning && horizontalMovement == -1 && currentLane != -1)
        {
            Debug.Log(1);
            this.horizontalMovement = horizontalMovement;
            targetX = transform.position.x + (this.horizontalMovement * horizontalMovementMagnitude);
            turning = true;
        }
        else if (!turning && horizontalMovement == 1 && currentLane != 1)
        {
            Debug.Log(2);
            this.horizontalMovement = horizontalMovement;
            targetX = transform.position.x + (this.horizontalMovement * horizontalMovementMagnitude);
            turning = true;
        }

        if (turning)
        {
            //Debug.Log(3);
            newPositionHorizontal = transform.position;
            newPositionHorizontal.x += this.horizontalMovement * horizontalSpeed * Time.deltaTime;
            transform.position = newPositionHorizontal;
        }

        if (turning && this.horizontalMovement == -1 && transform.position.x <= targetX)
        {
            Debug.Log(4);
            transform.position.Set(targetX, transform.position.y, transform.position.z);
            turning = false;
            if (currentLane == 0)
                currentLane = -1;
            else if (currentLane == 1)
                currentLane = 0;
        }
        else if (turning && this.horizontalMovement == 1 && transform.position.x >= targetX)
        {
            Debug.Log(5);
            transform.position.Set(targetX, transform.position.y, transform.position.z);
            turning = false;
            if (currentLane == -1)
                currentLane = 0;
            else if (currentLane == 0)
                currentLane = 1;
        }        
    }

    void Roll(bool crouch)
    {
        if (crouch && !rolling)
        {
            Debug.Log("C1");
            //targetZ = gameObject.transform.position.z + rollMovementMagnitude;
            rolling = true;
        }

        if (rolling)
        {
            //if (gameObject.transform.rotation.eulerAngles.x < 360)
                //gameObject.transform.Rotate(rollSpeed * Time.deltaTime, 0, 0, Space.World);
            Debug.Log("C2");
            newPositionRoll = gameObject.transform.position;
            newPositionRoll.z += 5 * Time.deltaTime;
            gameObject.transform.position = newPositionRoll;            
        }

        if (rolling && gameObject.transform.position.z >= targetZ)
        {
            Debug.Log("C3");
            rolling = false;
            gameObject.transform.rotation = Quaternion.identity;
            PlayerControllerRieles.pcr.Crouch = false;
        }
    }
    */
    void ScaleCapsuleForCrouching(bool crouch)
    {
        if (isGrounded && crouch)
        {
            if (crouching) return;
            capsule.height = capsule.height / 2f;
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

    void PreventStandingInLowHeadroom()
    {
        // prevent standing up in crouch-only zones
        if (!crouching)
        {
            Ray crouchRay = new Ray(_Rigidbody.position + Vector3.up * capsule.radius * k_Half, Vector3.up);
            float crouchRayLength = capsuleHeight - capsule.radius * k_Half;
            if (Physics.SphereCast(crouchRay, capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                crouching = true;
            }
        }
    }

    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        _Animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        //_Animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        _Animator.SetBool("Crouch", crouching);
        _Animator.SetBool("OnGround", isGrounded);
        if (!isGrounded)
        {
            _Animator.SetFloat("Jump", _Rigidbody.velocity.y);
            Debug.Log(_Animator.GetFloat("Jump"));
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        float runCycle =
            Mathf.Repeat(
                _Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + runCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * forwardAmount;
        if (isGrounded)
        {
            _Animator.SetFloat("JumpLeg", jumpLeg);
        }        

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (isGrounded && move.magnitude > 0)
        {
            _Animator.speed = animSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
            _Animator.speed = 1;
        }
    }

    void HandleAirborneMovement()
    {
        // apply extra gravity from multiplier:
        //Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
        //m_Rigidbody.AddForce(Physics.gravity);

        groundCheckDistance = _Rigidbody.velocity.y < 0 ? origGroundCheckDistance : 0.01f;
    }

    void HandleGroundedMovement(bool crouch, bool jump)
    {
        // check whether conditions are right to allow a jump:
        if (jump && !crouch && _Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            // jump!
            _Rigidbody.velocity = new Vector3(_Rigidbody.velocity.x, jumpPower, _Rigidbody.velocity.z);
            jumpSound.Play();
            isGrounded = false;
            _Animator.applyRootMotion = false;
            groundCheckDistance = 0.1f;
        }
    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        //float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
        //transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (isGrounded && Time.deltaTime > 0)
        {
            Vector3 v = (_Animator.deltaPosition * forwardSpeedMultiplier) / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = _Rigidbody.velocity.y;
            _Rigidbody.velocity = v;
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            groundNormal = hitInfo.normal;
            isGrounded = true;
            _Animator.applyRootMotion = true;
        }
        else if (!crouching)
        {
            isGrounded = false;
            groundNormal = Vector3.up;
            _Animator.applyRootMotion = false;
        }
    }

    public void SetCharacterSpeed(float speed)
    {
        forwardSpeedMultiplier = speed;
        animSpeedMultiplier = speed;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}


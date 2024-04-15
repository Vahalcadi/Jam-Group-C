using UnityEngine;

public class Player : MonoBehaviour
{
    private InputManager inputManager;

    [Header("Movement region")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    private bool isCrouching;
    //[SerializeField] private float maxVelocity;
    //private Vector2 movement;

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 1;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Animations")]
    [SerializeField] private Animator anim;

    private void Start()
    {
        inputManager = InputManager.Instance;

        transform.localRotation = Quaternion.Euler(0, CinemachinePOVExtention.Instance.StartingRotation.y, 0);
    }

    private void Update()
    {
        Jump();
        Movement();
        //MoveLeftRight();
        //StopMovement();
        CrossairMovement();
    }


    void Movement()
    {
        /*//Get value of x and y from input using Input Action component
        movement = inputManager.GetMovement();     

        if (movement.y != 0)
        {
            float finalSpeed = movement.y * speed;

            //Move player
            rb.AddForce(transform.forward * finalSpeed * Time.deltaTime);
        }

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, ClampVelocityAxis(false));
       */

        if (Input.GetKeyDown(KeyCode.LeftControl) && IsGroundDetected())
        {
            if (isCrouching)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y * 2, transform.position.z);
            }
            else
                transform.position = new Vector3(transform.position.x, transform.position.y / 2, transform.position.z);

            isCrouching = !isCrouching;
            anim.SetBool("crouching", isCrouching);
        }

        Debug.DrawLine(transform.position, transform.forward.normalized * 500, Color.black);

        Vector3 movemvent3d = new Vector3(inputManager.GetMovement().x, 0, inputManager.GetMovement().y);

        Vector3 MoveVector = transform.TransformDirection(movemvent3d) * speed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);
    }


    /**
     * 
     * if no movement inputs are registered, player will stop immediately
     * this prevents the player from slipping
     * 
     * **/
    //private void StopMovement()
    //{
    //    if (movement == Vector2.zero && IsGroundDetected())
    //    {
    //        rb.velocity = Vector3.zero;
    //        rb.angularVelocity = Vector3.zero;
    //        rb.angularDrag = 0;
    //    }
    //}

    //void MoveLeftRight()
    //{
    //    movement = inputManager.GetMovement();


    //    if (movement.x != 0)
    //    {
    //        float finalSpeed = movement.x * speed;

    //        //Move player
    //        rb.AddForce(transform.right * finalSpeed * Time.deltaTime);
    //    }

    //    rb.velocity = new Vector3(ClampVelocityAxis(true), rb.velocity.y, ClampVelocityAxis(false));
    //}


    ///**
    // * 
    // * Clamps the current velocity to the max velocity,
    // * this ensures a constant movement speed 
    // * 
    // * **/
    //private float ClampVelocityAxis(bool isX)
    //{
    //    float currentMaxVelocity = maxVelocity;
    //    float speed;

    //    if (isX)
    //        speed = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
    //    else
    //        speed = Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity);

    //    maxVelocity = currentMaxVelocity;
    //    return speed;
    //}

    public void CrossairMovement()
    {
        /**
         * 
         * Player rotation and camera rotation are not managed in the same script for semplicity purpose
         * The only thing that "binds" them is the "aimSensitivity" variable inside virtualCamera
         * This type of logic completely removes a possible desync between the player and the camera
         * 
         * **/

        //float mouseX = inputManager.GetLookPosition().normalized.x * CinemachinePOVExtention.Instance.aimSensitivity * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(0, CinemachinePOVExtention.Instance.StartingRotation.y, 0);

        //virtualCamera.transform.Rotate(new Vector3(mouseY, transform.rotation.y, transform.rotation.z));
    }

    void Jump()
    {
        /**
         * 
         * GetJump() function returns true if the button bound to jump is pressed
         * IsGroundDetected() fuction returns true when the raycast line, that starts from player, touches the ground
         * 
         * 
         * if both are true, the player will be launched into the air
         * 
         * **/

        if (inputManager.GetJump() && IsGroundDetected())
        {
            Debug.Log("JUMPED");
            rb.AddForce(rb.velocity + Vector3.up * jumpForce, ForceMode.VelocityChange);

            if (isCrouching)
            {
                isCrouching = false;
                anim.SetBool("crouching", isCrouching);
            }

        }
    }


    public bool IsGroundDetected() => Physics.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    /**
     * 
     * Graphic visualisation of IsGroundDetected() method
     * 
     * **/
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
    }
}

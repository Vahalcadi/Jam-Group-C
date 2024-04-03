using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputManager inputManager;

    [Header("Movement region")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    public float maxVelocity;
    private Vector2 movement;


    [Header("Crossair movement")]
    [SerializeField] private CinemachinePOVExtention virtualCamera;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        Movement();
        MoveLeftRight();
        CrossairMovement();
    }


    void Movement()
    {
        //Get value of x and y from input using Input Action component
        movement = inputManager.GetMovement();

        if (movement.y != 0)
        {
            float finalSpeed = movement.y * speed;

            //Move player
            rb.AddForce(transform.forward * finalSpeed * Time.deltaTime);
        }

        rb.velocity = new Vector3(ClampVelocityAxis(true), rb.velocity.y, ClampVelocityAxis(false));
    }

    void MoveLeftRight()
    {
        movement = inputManager.GetMovement();


        if (movement.x != 0)
        {
            float finalSpeed = movement.x * speed;

            //Move player
            rb.AddForce(transform.right * finalSpeed * Time.deltaTime);
        }

        rb.velocity = new Vector3(ClampVelocityAxis(true), rb.velocity.y, ClampVelocityAxis(false));
    }

    private float ClampVelocityAxis(bool isX)
    {
        float currentMaxVelocity = maxVelocity;
        float speed;

        if (isX)
            speed = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        else
            speed = Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity);

        maxVelocity = currentMaxVelocity;
        return speed;
    }

    public void CrossairMovement()
    {
        /*float mouseX = inputManager.GetLookPosition().normalized.x * aimSensitivity * Time.deltaTime;
        float mouseY = -inputManager.GetLookPosition().normalized.y * aimSensitivity * Time.deltaTime;

        transform.Rotate(new Vector3(mouseY, mouseX, transform.rotation.z));*/

        float mouseX = inputManager.GetLookPosition().normalized.x * virtualCamera.aimSensitivity * Time.deltaTime;
 

        transform.Rotate(new Vector3(transform.rotation.x, mouseX, transform.rotation.z));
        //virtualCamera.transform.Rotate(new Vector3(mouseY, transform.rotation.y, transform.rotation.z));
    }

}

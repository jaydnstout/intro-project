using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Public variables for movement and look speed, and reference to the camera object
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    bool canJump = true;
    public float jumpStrength = 5f;
    public float lookSensitivity = 3f;
    public GameObject cameraObject;

    void Start()
    {
        // Set gravity
        Physics.gravity = new Vector3(0, -24, 0);

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    void Update()
    {
        // Determine move speed based on whether the run key is held
        float moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        // Movement
        float moveX = (Input.GetAxis("Horizontal") * moveSpeed) * Time.deltaTime;
        float moveZ = (Input.GetAxis("Vertical") * moveSpeed) * Time.deltaTime;
        transform.Translate(moveX, 0, moveZ);

        // Mouse Look
        float lookV = Input.GetAxis("Mouse Y") * lookSensitivity;
        float lookH = Input.GetAxis("Mouse X") * lookSensitivity;
        transform.Rotate(0, lookH, 0);
        cameraObject.transform.Rotate(-lookV, 0, 0);
        
        // Clamp camera vertical rotation to prevent flipping
        float cameraRotationX = cameraObject.transform.eulerAngles.x;
        float playerRotationY = transform.eulerAngles.y;
        cameraObject.transform.eulerAngles = new Vector3(cameraRotationX, playerRotationY, 0);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Looper")
        {
            transform.position = new Vector3(transform.position.x, 9, -3);
        }
    }
}
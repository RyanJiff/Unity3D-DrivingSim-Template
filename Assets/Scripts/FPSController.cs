using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    /// <summary>
    /// Controls First person player
    /// </summary>


    //Declaring controllers
    PlayerController playerController = null;

    [Header("stats")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        //get controllers
        playerController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();

        //set player cam
        playerCamera = Camera.main;

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //camera rotation
        playerCamera.transform.position = transform.position + Vector3.up * 0.5f;
        playerCamera.transform.rotation = transform.rotation;

        //move while controller is active
        Move();

        //try to enter vehicle if E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnterVehicle();   
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    /// <summary>
    /// Character Movement
    /// </summary>
    void Move()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.rotation = Quaternion.Euler(rotationX, playerCamera.transform.eulerAngles.y, playerCamera.transform.eulerAngles.z);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    /// <summary>
    /// Ask Player Controller to enter Vehicle.
    /// </summary>
    void EnterVehicle()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5f))
        {
            playerController.EnterVehicle(hit.transform.gameObject);
        }
    }

    /// <summary>
    /// Ask Player Controller to interact with object.
    /// </summary>
    void Interact()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            playerController.Interact(hit.transform.gameObject);
        }
    }
}
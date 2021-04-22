using UnityEngine;

public class VehicleController : MonoBehaviour
{
    /// <summary>
    /// Controls vehicle if player is in one
    /// </summary>

    //Declaring Controllers
    PlayerController playerController = null;

    //Declaring Controller Objects
    Vehicle vehicleIn = null;
    Camera playerCamera = null;
    private Vector3 exitPosition = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerCamera = Camera.main;
    }


    private void Update()
    {
        //exit vehicle if F is pressed and brake
        if (Input.GetKeyDown(KeyCode.F))
        {
            vehicleIn.SendInputs(0, 0, 1);
            exitPosition = transform.position + (-vehicleIn.transform.right * 2) + (vehicleIn.transform.up * 1f);
            vehicleIn = null;
            return;
        }

        //while we have a vehicle control it
        if (vehicleIn)
        {
            //move with car
            transform.position = vehicleIn.transform.position;
            //move camera with car
            MoveCamera(vehicleIn.transform);
            vehicleIn.SendInputs(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
            Debug.Log("KMPH: " + vehicleIn.GetComponent<Rigidbody>().velocity.magnitude * 3.6f);
        }
        //if we have no vehicle to control tell the playercontroller to go back into FPS mode and tell him where we want to be
        else
        {
            playerController.TakeControl(exitPosition);
        }
    }


    /// <summary>
    /// Hand control of vehicle over
    /// </summary>
    public void TakeControl(Vehicle v)
    {
        if (v)
        {
            vehicleIn = v;
        }
    }

    /// <summary>
    /// Make player camera follow car
    /// </summary>
    public void MoveCamera(Transform target)
    {
        Vector3 targetPos = vehicleIn.transform.position - vehicleIn.transform.forward * 7.5f + vehicleIn.transform.up * 3f;
        playerCamera.transform.position = targetPos;
        playerCamera.transform.LookAt(vehicleIn.transform.position + vehicleIn.transform.up*2);
    }
}

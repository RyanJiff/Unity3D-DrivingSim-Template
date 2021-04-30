using UnityEngine;

[RequireComponent(typeof(FPSController))]
[RequireComponent(typeof(VehicleController))]
[RequireComponent(typeof(GUIController))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Controls which player controller is active
    /// </summary>

    //Declaring Control Modes
    public enum ControlMode { Character, Vehicle};
    [SerializeField] ControlMode controlMode = ControlMode.Character;

    //Declaring Controllers
    private FPSController fpsController = null;
    private CharacterController characterController = null;
    private VehicleController vehicleController = null;


    private void Awake()
    {
        fpsController = GetComponent<FPSController>();
        vehicleController = GetComponent<VehicleController>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(controlMode == ControlMode.Character)
        {
            characterController.enabled = true;
            fpsController.enabled = true;
            vehicleController.enabled = false;
        }
        else if(controlMode == ControlMode.Vehicle)
        {
            fpsController.enabled = false;
            characterController.enabled = false;
            vehicleController.enabled = true;
        }
    }

    public void TakeControl(Vector3 pos)
    {
        //revert to fps mode
        transform.position = pos;
        controlMode = ControlMode.Character;
    }

    /// <summary>
    /// Enter Vehicle
    /// </summary>
    public void EnterVehicle(GameObject o)
    {
        if (o.GetComponent<Vehicle>())
        {
            //we interacted with a vehicle
            vehicleController.TakeControl(o.GetComponent<Vehicle>());
            controlMode = ControlMode.Vehicle;
        }
    }
    
    /// <summary>
    /// Interact with object
    /// </summary>
    public void Interact(GameObject o)
    {
        if(o == null)
        {
            return;
        }
        else if (o.GetComponent<Vehicle>())
        {
            EnterVehicle(o);
        }
        else
        {
            Debug.Log("Interacted with " + o.name);
        }
    }

    /// <summary>
    /// returns player control mode
    /// </summary>
    public ControlMode GetPlayerMode()
    {
        return controlMode;
    }
}

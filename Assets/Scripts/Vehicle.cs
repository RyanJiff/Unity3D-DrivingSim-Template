using UnityEngine;

public class Vehicle : MonoBehaviour
{
    /// <summary>
    /// Vehicle driver class
    /// </summary>

    //stats
    [SerializeField] float torque = 500f;
    [SerializeField] float brake = 500f;
    [SerializeField] float maxSteerAngle = 35f;

    //Declaring Controller Vars
    private float _input_horizontal = 0.0f;
    private float _input_vertical = 0.0f;
    private float _input_brake = 0.0f;


    [SerializeField] WheelCollider[] steeringWheels = null;
    [SerializeField] WheelCollider[] powerWheels = null;
    [SerializeField] WheelCollider[] brakeWheels = null;

    private void Update()
    {
        //steering inputs
        if(steeringWheels.Length > 0)
        {
            for(int i = 0; i < steeringWheels.Length; i++)
            {
                steeringWheels[i].steerAngle = maxSteerAngle * _input_horizontal;
            }
        }
        //acceleration inputs
        if (powerWheels.Length > 0)
        {
            for (int i = 0; i < powerWheels.Length; i++)
            {
                powerWheels[i].motorTorque = torque * _input_vertical;
            }
        }

        //brake inputs
        if (brakeWheels.Length > 0)
        {
            for (int i = 0; i < brakeWheels.Length; i++)
            {
                brakeWheels[i].brakeTorque = brake * _input_brake;
            }
        }

    }

    /// <summary>
    /// Get inputs form vehicle controller
    /// </summary>
    public void SendInputs(float horizontal, float vertical, float brake)
    {
        _input_horizontal = horizontal;
        _input_vertical = vertical;
        _input_brake = brake;

    }
}

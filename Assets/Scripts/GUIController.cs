using UnityEngine;

public class GUIController : MonoBehaviour
{
    /// <summary>
    /// Interact with PlayerController controller to control GUIs
    /// </summary>


    //Declaring Controllers
    PlayerController playerController = null;

    //control mode which is taken from PlayerController
    PlayerController.ControlMode controlMode;

    public Rect rect1 = new Rect();


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (!playerController)
        {
            Debug.LogWarning("No player controller for GUI to interact with!");
        }
    }

    private void OnGUI()
    {
        controlMode = playerController.GetPlayerMode();
        //Put Universal GUI here

        GUI.Box(new Rect(Screen.width - 250,Screen.height - 150, 250, 150),"");

        //End universal GUI

        if(controlMode == PlayerController.ControlMode.Character)
        {
            //Character GUI
        }
        else if (controlMode == PlayerController.ControlMode.Vehicle)
        {
            //Vehicle GUI
        }
    }
}

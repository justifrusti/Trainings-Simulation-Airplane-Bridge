using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPannel : MonoBehaviour
{
    
    public BridgeController bridgeController;

    public Transform topOfJoystick;

    [SerializeField]
    private float forwardBackwardTilt = 0;
    [SerializeField]
    private float sideToSideTilt = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        forwardBackwardTilt = topOfJoystick.rotation.eulerAngles.x;
        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            forwardBackwardTilt = Mathf.Abs(forwardBackwardTilt - 360);
            Debug.Log("backward" + forwardBackwardTilt);
            bridgeController.bridgeState = BridgeController.BridgeState.Backward;
        }
        else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
        {
            Debug.Log("forward" + forwardBackwardTilt);
            bridgeController.bridgeState = BridgeController.BridgeState.Forward;
        }

        sideToSideTilt = topOfJoystick.rotation.eulerAngles.z;
        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            sideToSideTilt = Mathf.Abs(sideToSideTilt - 360);
            Debug.Log("Right" + sideToSideTilt);
            bridgeController.bridgeState = BridgeController.BridgeState.Right;
        }
        else if (sideToSideTilt > 5 && sideToSideTilt < 74)
        {
            Debug.Log("Left" + sideToSideTilt);
            bridgeController.bridgeState = BridgeController.BridgeState.Left;
        }
        TempInput();*/
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            transform.LookAt(other.transform.position, transform.up);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MoveBridgeUp")
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Right;
        }

        if (other.tag == "MoveBridgeDown")
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Left;
            //bridgeController.bridgeDown = true;
        }

        if (other.tag == "TurnHeadLeft")
        {
            bridgeController.turnHeadLeft = true;
        }

        if (other.tag == "ForwardButton")
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Forward;
        }
    }

    void OnTriggerExit(Collider other)
    {
        bridgeController.bridgeState = BridgeController.BridgeState.Stopped;
        bridgeController.turnHeadRight = false;
        bridgeController.turnHeadLeft = false;
        bridgeController.bridgeUp = false;
        bridgeController.bridgeDown = false;
    }
   
    void TempInput()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Forward;
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Stopped;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Backward;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Stopped;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Left;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Stopped;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Right;
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Stopped;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {

        }
    }
}

// bridgeController.bridgeState = BridgeController.BridgeState.right;
//TurnHeadRight
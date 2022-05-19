 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempJoystickTest : MonoBehaviour
{
    public Transform topOfJoystick;
    public Collider topColl;

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
        forwardBackwardTilt = topOfJoystick.rotation.eulerAngles.x;
        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            forwardBackwardTilt = Mathf.Abs(forwardBackwardTilt - 360);
            Debug.Log("backward" + forwardBackwardTilt);
            //bridgeController.bridgeState = BridgeController.BridgeState.Backward;
        }
        else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
        {
            Debug.Log("forward" + forwardBackwardTilt);
            //bridgeController.bridgeState = BridgeController.BridgeState.Forward;
        }

        sideToSideTilt = topOfJoystick.rotation.eulerAngles.z;
        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            sideToSideTilt = Mathf.Abs(sideToSideTilt - 360);
            Debug.Log("Right" + sideToSideTilt);
            //bridgeController.bridgeState = BridgeController.BridgeState.Right;
        }
        else if (sideToSideTilt > 5 && sideToSideTilt < 74)
        {
            Debug.Log("Left" + sideToSideTilt);
            //bridgeController.bridgeState = BridgeController.BridgeState.Left;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerHand")
        {
            transform.LookAt(other.transform.position, transform.up);
        }
    }
}

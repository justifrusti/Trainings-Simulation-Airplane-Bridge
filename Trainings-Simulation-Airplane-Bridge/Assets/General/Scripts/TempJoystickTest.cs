using System.Collections;
using System.Collections.Generic;
using System;
using Valve.VR;
using UnityEngine;

public class TempJoystickTest : MonoBehaviour
{
    public Transform topOfJoystick;

    [SerializeField]
    private float forwardBackwardTilt = 0;
    [SerializeField]
    private float sideToSideTilt = 0;

    private float rot = 0f;
    private float rotX;
    public float minRotX;
    public float maxRotX;
    
    private float rotSpeed = 3f;

    public SteamVR_Action_Single squezeAction;
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
            //bridgeController.bridgeState = BridgeController.BridgeState.Backward;
        }
        else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
        {
            //bridgeController.bridgeState = BridgeController.BridgeState.Forward;
        }

        sideToSideTilt = topOfJoystick.rotation.eulerAngles.z;
        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            sideToSideTilt = Mathf.Abs(sideToSideTilt - 360);
            //bridgeController.bridgeState = BridgeController.BridgeState.Right;
        }
        else if (sideToSideTilt > 5 && sideToSideTilt < 74)
        {
            //bridgeController.bridgeState = BridgeController.BridgeState.Left;
        }
        //lockedRotation();
        
        if (SteamVR_Actions._default.GrabGrip.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("test");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            if (SteamVR_Actions._default.GrabPinch.GetState(SteamVR_Input_Sources.Any))
            {
                if (SteamVR_Actions._default.GrabGrip.GetState(SteamVR_Input_Sources.Any))
                {
                    transform.LookAt(other.transform.position, transform.up);
                }
            }
        }
        
        
    }
    
    void lockedRotation()
    {
        rotX += Input.GetAxis("Mouse X");
        rotX = Mathf.Clamp(rotX, minRotX, maxRotX);
        //rotY += Input.GetAxis("Horizontal" * Time.deltaTime;
        //transform.rotation = Quaternion.Euler(rotX, rotationY, 0);
    }
}

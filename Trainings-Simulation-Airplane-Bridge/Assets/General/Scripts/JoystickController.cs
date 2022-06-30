using System.Collections;
using System.Collections.Generic;
using System;
using Valve.VR;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public BridgeController bridgeController;

    public Transform topOfJoystick;

    private float rot = 0f;
    private float rotY, rotX;
    public float minRotY, maxRotY, minRotX, maxRotX;
    public float rotationResetSpeed = 1.0f;

    public SteamVR_Action_Single squezeAction;

    public Quaternion baseRotation;

    public bool joystickGrabbed;

    void Start()
    {
        rotX = transform.localRotation.x;
        baseRotation = transform.rotation;
    }

    void Update()
    {
        
        lockedRotation();
        if (joystickGrabbed == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation, Time.time * rotationResetSpeed);
            bridgeController.bridgeState = BridgeController.BridgeState.Stopped;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            if (SteamVR_Actions._default.GrabPinch.GetState(SteamVR_Input_Sources.Any) && SteamVR_Actions._default.GrabGrip.GetState(SteamVR_Input_Sources.Any))
            {
                transform.LookAt(other.transform.position, transform.up);
                joystickGrabbed = true;
            }
        }

        if(other.tag == "Forward")
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Forward;
        }

        if (other.tag == "Backward")
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Backward;
        }

        if (other.tag == "Left")
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Left;
        }

        if (other.tag == "Right")
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Right;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        joystickGrabbed = false;
    }


    void lockedRotation()
    {
        rotX = Mathf.Clamp(rotX, minRotX, maxRotX);
        rotY = Mathf.Clamp(rotY, minRotY, maxRotY);

    }
}

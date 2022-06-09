using System.Collections;
using System.Collections.Generic;
using System;
using Valve.VR;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public Transform topOfJoystick;

    [SerializeField]
    private float forwardBackwardTilt = 0;
    [SerializeField]
    private float sideToSideTilt = 0;

    private float rot = 0f;
    private float rotY, rotX;
    public float minRotY, maxRotY, minRotX, maxRotX;

    public SteamVR_Action_Single squezeAction;

    private Quaternion baseRotation;
    public float rotationResetSpeed = 1.0f;

    void Start()
    {
        rotX = transform.localRotation.x;
        baseRotation = transform.rotation;
    }

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
        lockedRotation(); 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            if (SteamVR_Actions._default.GrabPinch.GetState(SteamVR_Input_Sources.Any) && SteamVR_Actions._default.GrabGrip.GetState(SteamVR_Input_Sources.Any))
            {
                transform.LookAt(other.transform.position, transform.up);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation, Time.time * rotationResetSpeed);
    }

    void lockedRotation()
    {
        rotX = Mathf.Clamp(rotX, minRotX, maxRotX);
        rotY = Mathf.Clamp(rotY, minRotY, maxRotY);

    }
}

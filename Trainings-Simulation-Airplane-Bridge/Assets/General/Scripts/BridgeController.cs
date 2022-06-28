using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [Header("movementValues")]
    public float moveSpeed;
    public float moveSpeedRolLuik;
    public Vector3 rotationPlus, rotationMin, rotationSpeed;

    [Header("movementBool")]
    public bool turnHeadRight;
    public bool turnHeadLeft, bridgeUp, bridgeDown;
    public bool rolLuikUp, rolLuikDown;

    [Header("BridgeTurnPoints")]
    public GameObject bridgeHead;
    public GameObject turnPointBase;

    [Header("Wheels")]
    public GameObject wheels;
    public Transform wheelTransform;
    public GameObject WheelElevator;

    [Header("rolLuikStuff")]
    public Transform rolLuikMovePoint;

    public GameObject tunnelExtended;
    public GameObject tunnelBase;

    public enum BridgeState {Forward, Backward, Left, Right, Up, Down, Stopped, Rotate}
    public BridgeState bridgeState;
    void Start()
    {
        bridgeState = BridgeState.Stopped;
    }

    void Update()
    {
        switch (bridgeState)
        {
            case BridgeState.Forward:

                if(wheels.transform.localRotation == Quaternion.Euler(0, 90, 0))
                {
                    rotationPlus = rotationSpeed * Time.deltaTime;
                    turnPointBase.transform.Rotate(rotationPlus);
                }else
                {
                    tunnelExtended.transform.localPosition += wheelTransform.forward * Time.deltaTime * moveSpeed;
                }
                break;

            case BridgeState.Backward:
                if (wheels.transform.localRotation == Quaternion.Euler(0, -90, 0))
                {
                     rotationPlus = rotationSpeed * Time.deltaTime;
                    turnPointBase.transform.Rotate(rotationPlus);
                }else
                {
                    tunnelExtended.transform.localPosition += -wheelTransform.forward * Time.deltaTime * moveSpeed;
                }
                break;

            case BridgeState.Left:
                wheels.transform.Rotate(0, -0.05f, 0);
                break;

            case BridgeState.Right:
                wheels.transform.Rotate(0, 0.05f, 0);
                break;
        }

        if(turnHeadRight == true)
        {
            bridgeHead.transform.Rotate(0, 0.05f, 0);
        }

        if(turnHeadLeft == true)
        {
            bridgeHead.transform.Rotate(0, -0.05f, 0);
        }

        if (bridgeUp == true)
        {
            turnPointBase.transform.Rotate(0.001f, 0, 0);
        }

        if (bridgeDown == true)
        {
            turnPointBase.transform.Rotate(-0.001f, 0, 0);
        }
        
        if(rolLuikUp == true)
        {
            rolLuikMovePoint.position += Vector3.up * moveSpeedRolLuik * Time.deltaTime;
        }

        if (rolLuikDown == true)
        {
            rolLuikMovePoint.position += -Vector3.up * moveSpeedRolLuik * Time.deltaTime;
        }
    }
   
}
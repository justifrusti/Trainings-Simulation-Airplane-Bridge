using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public ProcedureManager manager;

    [Header("movementValues")]
    public float moveSpeed;
    public float moveSpeedRolLuik;
    public Vector3 rotationPlus, rotationSpeed;
    public float rotationWheelSpeed;

    [Header("movementBool")]
    public bool turnHeadRight;
    public bool turnHeadLeft, bridgeUp, bridgeDown;
    public bool rolLuikUp, rolLuikDown;

    [Header("BridgeTurnPoints")]
    public GameObject bridgeHead;
    public GameObject turnPointBase;
    public Transform oneNTTunrPivot;
    public Transform twoNdBridgePartPivot;
    public Transform threeNdBridgePart;


    [Header("Wheels")]
    public GameObject wheels;
    public Transform wheelTransform;
    public GameObject WheelElevator;
    public Vector3 WheelElevatorSpeed;

    [Header("Other")]
    public Transform rolLuikMovePoint;
    public GameObject tunnelExtended;
    public GameObject tunnelBase;
    public enum ActiveState { Disabled, Enabled};
    public ActiveState activeState;

    [Header("player")]
    public Transform playerPOS;
    public GameObject player;

    public enum BridgeState {Forward, Backward, Left, Right, Stopped}
    public BridgeState bridgeState;
    void Start()
    {
        bridgeState = BridgeState.Stopped;
    }

    void Update()
    {
        switch(activeState)
        {
            case ActiveState.Disabled:
                if(manager.procedureState == ProcedureManager.ProcedureState.Moving)
                {
                    activeState = ActiveState.Enabled;
                }
                break;

            case ActiveState.Enabled:
                switch (bridgeState)
                {
                    case BridgeState.Forward:
                        if (wheelTransform.localEulerAngles.y >= 30 && wheelTransform.localEulerAngles.y <= 90)
                        {
                            rotationPlus = rotationSpeed * Time.deltaTime;
                            twoNdBridgePartPivot.transform.Rotate(rotationPlus);
                            turnPointBase.transform.Rotate(rotationPlus);
                        }
                        else
                        {
                            tunnelExtended.transform.localPosition += wheelTransform.forward * Time.deltaTime * moveSpeed;
                            twoNdBridgePartPivot.LookAt(oneNTTunrPivot);
                        }
                        break;

                    case BridgeState.Backward:
                        if (wheelTransform.localEulerAngles.y >= -30 && wheelTransform.localEulerAngles.y <= -90)
                        {
                            rotationPlus = rotationSpeed * Time.deltaTime;
                            turnPointBase.transform.Rotate(rotationPlus);
                            tunnelExtended.transform.localPosition += -wheelTransform.forward * Time.deltaTime * moveSpeed;
                        }
                        else if (wheelTransform.localEulerAngles.y >= -10 && wheelTransform.localEulerAngles.y <= 10)
                        {
                            tunnelExtended.transform.localPosition += -wheelTransform.forward * Time.deltaTime * moveSpeed;
                        }
                        break;

                    case BridgeState.Left:
                        wheels.transform.Rotate(0, -rotationWheelSpeed, 0);
                        break;

                    case BridgeState.Right:
                        wheels.transform.Rotate(0, rotationWheelSpeed, 0);
                        break;
                }
                break;
        }

        if(turnHeadRight == true)
        {
            bridgeHead.transform.Rotate(0, 0.03f, 0);
        }

        if(turnHeadLeft == true)
        {
            bridgeHead.transform.Rotate(0, -0.03f, 0);
        }

        if (bridgeUp == true)
        {
            turnPointBase.transform.Rotate(0.001f, 0, 0);
            threeNdBridgePart.transform.Rotate(-0.001f, 0, 0);
            WheelElevator.transform.position += -WheelElevatorSpeed;
        }

        if (bridgeDown == true)
        {
            turnPointBase.transform.Rotate(-0.001f, 0, 0);
            threeNdBridgePart.transform.Rotate(0.001f, 0, 0);
            WheelElevator.transform.position += WheelElevatorSpeed;
        }
        
        if(rolLuikUp == true)
        {
            rolLuikMovePoint.position += Vector3.up * moveSpeedRolLuik * Time.deltaTime;
        }

        if (rolLuikDown == true)
        {
            rolLuikMovePoint.position += -Vector3.up * moveSpeedRolLuik * Time.deltaTime;
        }
        player.transform.position = playerPOS.position;
    }
   
}
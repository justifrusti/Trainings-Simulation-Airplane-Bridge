using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public Vector3 rotationSpeed;

    public Vector3 rotationPlus, rotationMin;

    public bool turnHeadRight,turnHeadLeft, bridgeUp, bridgeDown;

    public GameObject bridgeHead, turnPointBase, turnPointHead, tireTurnPoint;

    public GameObject wheels;
    public Transform wheelTransform;
    

    public float moveSpeed;

    public enum BridgeState {Forward, Backward, Left, Right, Up, Down, Stopped, Rotate}
    public BridgeState bridgeState;
    // Start is called before the first frame update
    void Start()
    {
        bridgeState = BridgeState.Stopped;
    }

    // Update is called once per frame
    void Update()
    {
        switch (bridgeState)
        {
            case BridgeState.Forward:

                if(wheels.transform.localRotation == Quaternion.Euler(0, 90, 0))
                {
                    rotationSpeed = rotationPlus * Time.deltaTime;
                    turnPointBase.transform.Rotate(rotationSpeed);
                }else
                {
                    transform.localPosition += wheelTransform.forward * Time.deltaTime * moveSpeed;
                }
                break;
            case BridgeState.Backward:
                if (wheels.transform.localRotation == Quaternion.Euler(0, 90, 0))
                {
                    rotationSpeed = rotationMin * Time.deltaTime;
                    turnPointBase.transform.Rotate(rotationSpeed);
                }
                else
                {
                    transform.localPosition += -wheelTransform.forward * Time.deltaTime * moveSpeed;
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
            turnPointBase.transform.Rotate(0.1f, 0, 0);
            turnPointHead.transform.Rotate(-0.1f, 0, 0);
        }
        if (bridgeDown == true)
        {
            turnPointBase.transform.Rotate(-0.1f, 0, 0);
            turnPointHead.transform.Rotate(0.1f, 0, 0);
        }
    }
}

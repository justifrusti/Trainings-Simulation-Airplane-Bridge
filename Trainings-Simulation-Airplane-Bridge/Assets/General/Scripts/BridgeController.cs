using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public Vector3 moveDirection;

    public bool turnHeadRight,turnHeadLeft, bridgeUp, bridgeDown;

    public GameObject bridgeHead, turnPointBase, turnPointHead;

    public Quaternion rotDyrection;

    public enum BridgeState {Forward, backward, left, right, up, down, stopped}
    public BridgeState bridgeState;
    // Start is called before the first frame update
    void Start()
    {
        bridgeState = BridgeState.stopped;
    }

    // Update is called once per frame
    void Update()
    {
        switch (bridgeState)
        {
            case BridgeState.Forward:
                transform.position += moveDirection * Time.deltaTime;
                break;
            case BridgeState.backward:
                transform.position += moveDirection * Time.deltaTime;
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

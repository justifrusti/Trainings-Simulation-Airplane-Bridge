using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public Vector3 moveDirection;
    

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

    }
}
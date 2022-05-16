using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public Vector3 moveSpeed;
    

    public enum bridgestate { Forward, backward, left, right, up, down, stopped }
    public bridgestate bridgeState;
    // Start is called before the first frame update
    void Start()
    {
        bridgeState = bridgestate.stopped;
    }

    // Update is called once per frame
    void Update()
    {
        switch (bridgeState)
        {
            case bridgestate.Forward:
                transform.position += moveSpeed * Time.deltaTime;

                break;
        }

    }
    public void Movebridgeforward()
    {
        bridgeState = bridgestate.Forward;
    }
}

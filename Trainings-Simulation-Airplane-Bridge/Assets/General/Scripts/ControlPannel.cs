using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPannel : MonoBehaviour
{
    
    public BridgeController bridgeController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MoveBridgeUp")
        {
            bridgeController.bridgeUp = true;
        }

        if (other.tag == "MoveBridgeDown")
        {
            bridgeController.bridgeDown = true;
        }

        if (other.tag == "TurnHeadLeft")
        {
            bridgeController.turnHeadLeft = true;
        }

        if (other.tag == "TurnHeadRight")
        {
            bridgeController.turnHeadRight = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        bridgeController.bridgeState = BridgeController.BridgeState.stopped;
        bridgeController.turnHeadRight = false;
        bridgeController.turnHeadLeft = false;
        bridgeController.bridgeUp = false;
        bridgeController.bridgeDown = false;
    }
}

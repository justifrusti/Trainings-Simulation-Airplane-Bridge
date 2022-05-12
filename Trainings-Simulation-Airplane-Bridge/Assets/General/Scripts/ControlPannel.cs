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
        if (other.tag == "ForwardButton")
        {
            bridgeController.bridgeState = BridgeController.BridgeState.Forward;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        bridgeController.bridgeState = BridgeController.BridgeState.stopped;
    }
}

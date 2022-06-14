using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPannel : MonoBehaviour
{
    public BridgeController bridgeController;

    Vector3 originalLocation;

    public float maxTravel = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        originalLocation = other.transform.position;
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

        if (other.tag == "TunrHeadRight")
        {
            bridgeController.turnHeadRight = true;              
        }
    }

    void OnTriggerExit(Collider other)
    {
        bridgeController.bridgeState = BridgeController.BridgeState.Stopped;
        bridgeController.turnHeadRight = false;
        bridgeController.turnHeadLeft = false;
        bridgeController.bridgeUp = false;
        bridgeController.bridgeDown = false;

        Vector3.MoveTowards(other.transform.position, originalLocation, maxTravel);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPannel : MonoBehaviour
{
    public ProcedureManager procedureManager;

    public BridgeController bridgeController;

    void OnTriggerEnter(Collider other)
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

        if (other.tag == "TunrHeadRight")
        {
            bridgeController.turnHeadRight = true;              
        }

        if(other.tag == "HandButton")
        {
            procedureManager.startIsActive = true;
        }

        if(other.tag == "LampTest")
        {
            procedureManager.testedLight = true;
        }

        if(other.tag == "Claxon")
        {
            procedureManager.PlayHjonk();
        }

        if(other.tag == "AutoTrimButton")
        {
            //do something / not implemented
        }

        if (other.tag == "HuifUpButton")
        {
            //do something / not implemented
        }

        if (other.tag == "HuifDownButton")
        {
            //do something / not implemented
        }
    }

    void OnTriggerExit(Collider other)
    {
        bridgeController.bridgeState = BridgeController.BridgeState.Stopped;
        bridgeController.turnHeadRight = false;
        bridgeController.turnHeadLeft = false;
        bridgeController.bridgeUp = false;
        bridgeController.bridgeDown = false;
    }
}
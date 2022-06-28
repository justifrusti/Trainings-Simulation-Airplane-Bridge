using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolluikCollisonDetector : MonoBehaviour
{
    public BridgeController bridgeController;

    public GameObject buttonRolLuikUp, buttonRolLuikDown;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RolLuikTriggerBottom")
        {
            bridgeController.rolLuikDown = false;
            buttonRolLuikDown.tag = "Untagged";
        }

        if (other.tag == "RolLuikTriggerTop")
        {
            bridgeController.rolLuikUp = false;
            buttonRolLuikUp.tag = "Untagged";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(bridgeController.rolLuikDown == true)
        {
            buttonRolLuikUp.tag = "RolluikUpButton";
        }
        
        if(bridgeController.rolLuikUp == true)
        {
            buttonRolLuikDown.tag = "RolluikDownButton";
        }
    }
}

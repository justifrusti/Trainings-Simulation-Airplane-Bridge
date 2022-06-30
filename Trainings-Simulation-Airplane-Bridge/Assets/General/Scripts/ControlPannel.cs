using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPannel : MonoBehaviour
{
    public ProcedureManager procedureManager;

    public BridgeController bridgeController;

    public Animator anim;

    public AnimationClip clip;

    private AnimatorClipInfo clipInfo;

    void OnTriggerEnter(Collider other)
    {

        //inster audio here
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
            bool triggeredCoroutine = false;
            //do something / not implemented
            procedureManager.doneMoving = true;

            if(triggeredCoroutine == false)
            {
                triggeredCoroutine = true;

                StartCoroutine(FinishedTriggered());
            }
        }

        if (other.tag == "RolluikUpButton")
        {
            bridgeController.rolLuikUp = true;
            procedureManager.shuttersOpened = true;
        }

        if (other.tag == "RolluikDownButton")
        {
            bridgeController.rolLuikDown = true;
        }
    }

    IEnumerator FinishedTriggered()
    {
        anim.SetBool("HuifNeer", true);

        float time = clip.length * 3;

        yield return new WaitForSeconds(time);

        procedureManager.finished = true;
    }

    void OnTriggerExit(Collider other)
    {
        //insert audio here
        bridgeController.bridgeState = BridgeController.BridgeState.Stopped;
        bridgeController.turnHeadRight = false;
        bridgeController.turnHeadLeft = false;
        bridgeController.bridgeUp = false;
        bridgeController.bridgeDown = false;
        bridgeController.rolLuikUp = false;
        bridgeController.rolLuikDown = false;
    }
}
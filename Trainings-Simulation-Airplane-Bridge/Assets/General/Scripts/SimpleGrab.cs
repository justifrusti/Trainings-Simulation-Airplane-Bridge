using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SimpleGrab : MonoBehaviour
{
    private Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }
    public void OnHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();
    }
    public void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }
    public void HandHoverUpdate(Hand hand)
    {
        print("test");
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabingEnding = hand.IsGrabEnding(gameObject);

        if(interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType);
        }
        else if (isGrabingEnding)
        {
            hand.DetachObject(gameObject);
        }
    }
}

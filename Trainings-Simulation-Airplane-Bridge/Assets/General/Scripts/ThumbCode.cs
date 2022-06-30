using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ThumbCode : MonoBehaviour
{
    public PunishedBobMoveCode bobCode;

    public bool thumbUp = false;
    public bool fakeThumb;

    private void Update()
    {
        if (SteamVR_Actions._default.GrabPinch.GetState(SteamVR_Input_Sources.Any) && SteamVR_Actions._default.GrabGrip.GetState(SteamVR_Input_Sources.Any))
        {
            if(!thumbUp)
            {
                thumbUp = true;

                bobCode.playerHasThumbUp = true;
            }
        }

        if(fakeThumb)
        {
            if (!thumbUp)
            {
                thumbUp = true;

                bobCode.playerHasThumbUp = true;
            }
        }
    }
}

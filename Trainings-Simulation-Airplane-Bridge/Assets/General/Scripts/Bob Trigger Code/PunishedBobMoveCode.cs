using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunishedBobMoveCode : MonoBehaviour
{
    public bool hasAlreadyEnteredTrigger;
    public bool playerHasThumbUp;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHasThumbUp)
        {
            anim.SetBool("Tumbs UP", false);
            anim.SetBool("Walking", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasAlreadyEnteredTrigger)
        {
            hasAlreadyEnteredTrigger = true;

            if (other.gameObject.tag == "Thumbs Up")
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Tumbs UP", true);
            }

            if(other.gameObject.tag == "Death")
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(hasAlreadyEnteredTrigger)
        {
            hasAlreadyEnteredTrigger = false;
        }
    }
}

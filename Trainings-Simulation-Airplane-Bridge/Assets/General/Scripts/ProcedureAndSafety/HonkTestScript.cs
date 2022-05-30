using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonkTestScript : MonoBehaviour
{
    public AudioSource hjonk;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            hjonk.Play();
        }    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour {

    public Animator animator;
    private Unit unit;
    
    




    void Start() {
        unit = GetComponent<Unit> ();
        animator = GetComponent<Animator> ();
        

    }

    
    void Update() {
        /*
        if () {
            animator.SetBool ("Walking" , true);
        } 
        else {
            animator.SetBool ("Walking" , false);
        }
        
        if () {
            animator.SetBool ("Tumbs UP" , true);
        }
        else {
            animator.SetBool ("Tumbs UP" , false);
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buttons : MonoBehaviour
{
    public Rigidbody buttonTopRigid;
    public Transform buttonTop;
    public Transform buttonLowerLimit;
    public Transform buttonUpperLimit;
    public float force = 10;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        buttonTop.transform.localPosition = new Vector3(0, buttonTop.transform.localPosition.y, 0);
        buttonTop.transform.localEulerAngles = new Vector3(0, 0, 0);
        if (buttonTop.localPosition.y >= 0)
            buttonTop.transform.position = new Vector3(buttonUpperLimit.position.x, buttonUpperLimit.position.y, buttonUpperLimit.position.z);
        else
            buttonTopRigid.AddForce(buttonTop.transform.up * force * Time.deltaTime);

        if (buttonTop.localPosition.y <= buttonLowerLimit.localPosition.y)
            buttonTop.transform.position = new Vector3(buttonLowerLimit.position.x, buttonLowerLimit.position.y, buttonLowerLimit.position.z);

    }

}

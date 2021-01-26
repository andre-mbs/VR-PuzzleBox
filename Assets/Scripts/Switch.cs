using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Switch : MonoBehaviour
{
    public GameObject inputObj;
    public bool switchSide; // false -> left; true -> right
    private bool input;
    public bool output1;
    public bool output2;

    // Start is called before the first frame update
    void Start()
    {
        output1 = false;        
        output2 = false;
        switchSide = false;    
    }

    // Update is called once per frame
    void Update()
    {
        input = inputObj.GetComponent<Wire>().value;

        Quaternion rot = transform.rotation;
        if(!switchSide){
            Vector3 rotEuler = rot.eulerAngles;
            rotEuler.z = -90;
            rot.eulerAngles = rotEuler;
            output1 = input;
            output2 = false;
        }else{
            Vector3 rotEuler = rot.eulerAngles;
            rotEuler.z = 90;
            rot.eulerAngles = rotEuler;
            output1 = false;
            output2 = input;       
        }
        transform.rotation = rot;

    }
}

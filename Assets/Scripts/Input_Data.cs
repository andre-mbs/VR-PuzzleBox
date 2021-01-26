using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Input_Data : MonoBehaviour
{
    [HideInInspector]
    public bool output;

    public bool forceOn;

    // Start is called before the first frame update
    void Start()
    {
        if(forceOn){
            output = true; 
        }else{
            output = false; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = transform.rotation;
        if(!output){
            Vector3 rotEuler = rot.eulerAngles;
            rotEuler.z = 0;
            rot.eulerAngles = rotEuler;
        }else{
            Vector3 rotEuler = rot.eulerAngles;
            rotEuler.z = 180;
            rot.eulerAngles = rotEuler;       
        }
        transform.rotation = rot;
    }
}

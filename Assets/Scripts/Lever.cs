using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Lever : MonoBehaviour
{
    public GameObject hand;
    public GameObject inputObj;
    public bool triggerDown;
    private bool leverDown;
    private int logicDegreesCount;
    private int leverDegreesCount;
    private bool restoreLeverRotation;

    // Start is called before the first frame update
    void Start()
    {
        leverDown = false;
        logicDegreesCount = 0;
        leverDegreesCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerDown && GetComponent<Interactable>().selected && inputObj.GetComponent<Wire>().value){
            Quaternion rot = transform.rotation;
            Vector3 rotEuler = new Vector3(0, 0, 45);
            rotEuler.z = Map(2.1f, 2.6f, -135f, -45f, hand.transform.position.y);
            rot.eulerAngles = rotEuler;
            transform.rotation = rot;

            if(transform.rotation == Quaternion.Euler(0, 0, -135)){
                leverDown = true;
            }
        }
        
        if(leverDown && logicDegreesCount < 180){
            logicDegreesCount += 1;
            GameObject.Find("Logic_Puzzle").GetComponent<Transform>().Rotate(new Vector3(0, 1, 0));
        }

        if(logicDegreesCount == 180 && leverDegreesCount < 90){
            leverDegreesCount += 2;
            GetComponent<Transform>().Rotate(new Vector3(0, 0, 2));
        }

        if(leverDegreesCount == 90){
            logicDegreesCount = 0;
            leverDegreesCount = 0;
            leverDown = false;
        }

    }

    float Map(float min1, float max1, float min2, float max2, float value){
        float retValue = (min2 + (value-min1) * (max2-min2) / (max1-min1));

        if(retValue < min2){
            retValue = min2;
        }else if(retValue > max2){
            retValue = max2;
        }
        
        return retValue; 
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Actions : MonoBehaviour
{
    public SteamVR_Action_Boolean OnOff;
    public SteamVR_Input_Sources handType;

    private GameObject selectedObj = null;

    public GameObject[] interactableObjs;
    private Transform parent;
    private GameObject lastSelected;

    public bool triggerDown;

    // Start is called before the first frame update
    void Start()
    {
        triggerDown = false;
        OnOff.AddOnStateDownListener(TriggerDown, handType);
        OnOff.AddOnStateUpListener(TriggerUp, handType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource){
        triggerDown = false;
        interactableObjs = GameObject.FindGameObjectsWithTag("interactable");
        foreach (GameObject go in interactableObjs)
        {
            if(go.GetComponent<Interactable>().selected){
                selectedObj = go;
                break;
            }
            selectedObj = null;
        }

        if(selectedObj != null){
            if((selectedObj.name.Substring(0, 4) == "Gate" || selectedObj.name == "battery" || selectedObj.name == "screw_driver" || selectedObj.name == "morse_code") && selectedObj.transform.parent == GameObject.Find("RightHand").transform){
                selectedObj.transform.parent = parent;

                selectedObj.GetComponent<Rigidbody>().useGravity = true;
                selectedObj.GetComponent<Rigidbody>().isKinematic = false;

                selectedObj.GetComponent<Snap>().inPlace = true;
            }else if(selectedObj.name == "lever_pole"){
                selectedObj.GetComponent<Lever>().triggerDown = false;
            }else if(selectedObj.name.Substring(0, 4) == "tile"){
                selectedObj.GetComponent<Tile>().triggerDown = false;
            }else if(selectedObj.name == "cursor"){
                selectedObj.GetComponent<Slider>().triggerDown = false;
            }
        }else{
            if(lastSelected != null && lastSelected.name == "lever_pole" ){
                lastSelected.GetComponent<Lever>().triggerDown = false;
            }else if(lastSelected != null && lastSelected.name.Substring(0, 4) == "tile" ){
                lastSelected.GetComponent<Tile>().triggerDown = false;
            }else if(lastSelected != null && lastSelected.name == "cursor" ){
                lastSelected.GetComponent<Slider>().triggerDown = false;
            }
        }
        
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource){
        triggerDown = true;
        interactableObjs = GameObject.FindGameObjectsWithTag("interactable");
        foreach (GameObject go in interactableObjs)
        {   
            //Debug.Log(go);
            if(go.GetComponent<Interactable>().selected){
                selectedObj = go;
                break;
            }
            selectedObj = null;
        }

        if(selectedObj != null){
            if(selectedObj.name.Substring(0, 5) == "Input"){
                selectedObj.GetComponent<Input_Data>().output = !selectedObj.GetComponent<Input_Data>().output; 
            }else if(selectedObj.name == "Switch"){
                selectedObj.GetComponent<Switch>().switchSide = !selectedObj.GetComponent<Switch>().switchSide;
            }else if(selectedObj.name.Substring(0, 4) == "Gate" || selectedObj.name == "battery" || selectedObj.name == "screw_driver" || selectedObj.name == "morse_code"){
                if(!(selectedObj.name == "screw_driver" && selectedObj.GetComponent<Snap>().rotate)){
                    selectedObj.GetComponent<Snap>().inPlace = false;

                    GameObject hand = GameObject.Find("RightHand");
                    parent = selectedObj.transform.parent;
                    selectedObj.transform.parent = hand.transform;

                    selectedObj.GetComponent<Rigidbody>().useGravity = false;
                    selectedObj.GetComponent<Rigidbody>().isKinematic = true;
                }                
            }else if(selectedObj.name == "lever_pole"){
                lastSelected = selectedObj;
                selectedObj.GetComponent<Lever>().triggerDown = true;
            }else if(selectedObj.name.Substring(0, 4) == "tile"){
                lastSelected = selectedObj;
                selectedObj.GetComponent<Tile>().triggerDown = true;
            }else if(selectedObj.name == "cursor"){
                lastSelected = selectedObj;
                selectedObj.GetComponent<Slider>().triggerDown = true;
            }
        }
    }
}
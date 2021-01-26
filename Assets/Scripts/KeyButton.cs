using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System;

public class KeyButton : MonoBehaviour
{
    private GameObject actions;
    private GameObject battery;
    private GameObject keypad;
    // Start is called before the first frame update
    void Start()
    {
        actions = GameObject.Find("ActionsObj");
        battery = GameObject.Find("battery");
        keypad = GameObject.Find("keypad");
    }

    // Update is called once per frame
    void Update()
    {
        if(tag == "interactable" && GetComponent<Interactable>().selected && actions.GetComponent<Actions>().triggerDown && battery.GetComponent<Snap>().soundOn){
            keypad.GetComponent<Keypad>().AddToSet(Int32.Parse(name.Substring(8, 1)));
        }
    }
}

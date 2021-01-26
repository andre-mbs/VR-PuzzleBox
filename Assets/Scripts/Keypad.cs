using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Keypad : MonoBehaviour
{
    private GameObject battery;
    private HashSet<int> values;
    private HashSet<int> inputValues;
    public Material materialOn;
    // Start is called before the first frame update
    void Start()
    {
        battery = GameObject.Find("battery");
        values = new HashSet<int>(new int[] {5, 1, 8, 4});
        inputValues = new HashSet<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if(battery.GetComponent<Snap>().soundOn && values.SetEquals(inputValues)){
            GameObject.Find("ActionsObj").GetComponent<Start_Time>().puzzleCompleted = true;
            GameObject.Find("LED_Green").GetComponent<Renderer>().material = materialOn;
        }
    }

    public void AddToSet(int elem){
        if(inputValues.Count == 4){
            inputValues = new HashSet<int>();
        }

        inputValues.Add(elem);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public GameObject inputObj;
    public enum outNumbers {OUT1, OUT2};
    public outNumbers outNum;
    public Material materialOn;
    public Material materialOff;
    public bool value;

    // Start is called before the first frame update
    void Start()
    {
        value = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inputObj.GetComponent<Input_Data>() != null){
            value = inputObj.GetComponent<Input_Data>().output;
        }else if(inputObj.GetComponent<Gate>() != null){
            value = inputObj.GetComponent<Gate>().output;
        }else if(inputObj.GetComponent<Switch>() != null){
            if(outNum == outNumbers.OUT1){
                value = inputObj.GetComponent<Switch>().output1;
            }else if(outNum == outNumbers.OUT2){
                value = inputObj.GetComponent<Switch>().output2;
            }
        }

        if(value){
            if(transform.childCount == 0){
                GetComponent<Renderer>().material = materialOn;
            }else{
                for(int i=0; i<transform.childCount; i++){
                    transform.GetChild(i).GetComponent<Renderer>().material = materialOn;
                }
            }
        }else{
            if(transform.childCount == 0){
                GetComponent<Renderer>().material = materialOff;
            }else{
                for(int i=0; i<transform.childCount; i++){
                    transform.GetChild(i).GetComponent<Renderer>().material = materialOff;
                }
            }
        }
    }
}

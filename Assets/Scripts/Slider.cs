using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Slider : MonoBehaviour
{
    public GameObject hand;
    public GameObject pointer;
    public GameObject radio;
    public bool triggerDown;
    private AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        audioSources = radio.GetComponents<AudioSource>();
        Vector3 newPointerPos = pointer.transform.position;
        newPointerPos.x = Map(-2.932f, -1.727f, -2.74f, -2.08f, transform.position.x);
        pointer.transform.position = newPointerPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerDown){
            Vector3 newCursorPos = transform.position;
            newCursorPos.x = hand.transform.position.x;
            if(newCursorPos.x <= -1.727f && newCursorPos.x >= -2.932f){
                transform.position = newCursorPos;
                Vector3 newPointerPos = pointer.transform.position;
                newPointerPos.x = Map(-2.932f, -1.727f, -2.74f, -2.08f, newCursorPos.x);
                pointer.transform.position = newPointerPos;

                if(GameObject.Find("battery").GetComponent<Snap>().soundOn){
                    audioSources[0].volume = GetSoundVolume(Map(-1.727f, -2.932f, 0f, 1f, newCursorPos.x), "noise");
                    audioSources[1].volume = GetSoundVolume(Map(-1.727f, -2.932f, 0f, 1f, newCursorPos.x), "morse");
                }               
            }
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

    float GetSoundVolume(float sliderPosition, string soudType){
        float result;
        if(sliderPosition <= 0.6){
            if(soudType == "noise"){
                result = 1;
            }else{
                result = 0;
            }
        }else{
            if(soudType == "noise"){
                result = (13f + (-32f)*sliderPosition + 20f*sliderPosition*sliderPosition);
            }else{
                result = (-10.4f + 28f*sliderPosition + (-17.5f)*sliderPosition*sliderPosition);
            }
            
        }
        return result;
    }
}

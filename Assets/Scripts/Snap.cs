using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Snap : MonoBehaviour
{
    private Vector3 position;
    private GameObject placeholder1;
    private GameObject placeholder2;
    public bool soundOn;
    public float th;
    public bool inPlace;
    public bool rotate;
    private GameObject actions;
    private GameObject screw;
    private Transform screw_parent;

    // Start is called before the first frame update
    void Start()
    {
        th = 0.08f;
        placeholder1 = GameObject.Find("gate_placeholder1");
        placeholder2 = GameObject.Find("gate_placeholder2");
        inPlace = false;
        actions = GameObject.Find("ActionsObj");
        screw = GameObject.Find("Screw_Cross_1");
        screw_parent = screw.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        
        if(name.Substring(0, 4) == "Gate"){
            if(Vector3.Distance(position, placeholder1.transform.position) < th && inPlace){
                Vector3 newPos = placeholder1.transform.position;
                newPos.x = -1.13f;
                transform.position = newPos;
                Quaternion rot = placeholder1.transform.rotation;
                Vector3 rotEuler = rot.eulerAngles;
                rotEuler.z = 90;
                rot.eulerAngles = rotEuler;
                transform.rotation = rot;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                inPlace = false;
            }else if(Vector3.Distance(position, placeholder2.transform.position) < th && inPlace){
                Vector3 newPos = placeholder2.transform.position;
                newPos.x = -1.13f;
                transform.position = newPos;
                Quaternion rot = placeholder2.transform.rotation;
                Vector3 rotEuler = rot.eulerAngles;
                rotEuler.z = 90;
                rot.eulerAngles = rotEuler;
                transform.rotation = rot;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                inPlace = false;
            }
        }else if(name == "battery"){
            Vector3 newPos = new Vector3(-1.455f, 3.818f, -1.075f);
            //Debug.Log("current: " + position + "; newPos: " + newPos + "; dst: " + Vector3.Distance(position, newPos) + " inPlace: " + inPlace);
            if(Vector3.Distance(position, newPos) < th && inPlace){
                transform.position = newPos;
                transform.rotation = Quaternion.Euler(90f, 0f, -90f);
                inPlace = false;

                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                tag = "Untagged";
                Destroy(GetComponent<Interactable>());

                if(!soundOn){
                    AudioSource[] audioSources = GameObject.Find("radio").GetComponents<AudioSource>();
                    audioSources[0].volume = 1;
                    audioSources[1].volume = 0;
                    soundOn = true;
                }
            }
        }else if(name == "screw_driver"){
            Vector3 newPos = new Vector3(-1.3198f, 3.8926f, -1.1651f);
            //Debug.Log("current: " + position + "; newPos: " + newPos + "; dst: " + Vector3.Distance(position, newPos) + " inPlace: " + inPlace);
            if(Vector3.Distance(position, newPos) < th && inPlace){
                transform.position = newPos;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                inPlace = false;
                rotate = true;

                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
            }

            if(rotate && actions.GetComponent<Actions>().triggerDown){
                GameObject hand = GameObject.Find("RightHand");
                float zRot = hand.transform.eulerAngles.z;
                Vector3 eulerRot = new Vector3(0, 0, zRot);
                transform.rotation = Quaternion.Euler(eulerRot);
                Vector3 newPos2 = transform.position;

                screw.transform.parent = gameObject.transform;             
                newPos2.z = Map(60f, 210f, -1.1651f, -1.2343f, zRot);
                transform.position = newPos2;

                if(transform.position.z <= -1.2343f){
                    screw.transform.parent = screw_parent;
                    screw.GetComponent<Rigidbody>().useGravity = true;
                    screw.GetComponent<Rigidbody>().isKinematic = false;
                    GameObject.Find("electric_plate").GetComponent<ElectricPlate>().rotate = true;
                    rotate = false;

                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
    }

    float Map(float min1, float max1, float min2, float max2, float value){
        return (min2 + (value-min1) * (max2-min2) / (max1-min1));
    }
}

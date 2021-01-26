using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Logic_Puzzle : MonoBehaviour
{
    public bool puzzleCompleted;
    private bool resetCompleted;

    // Start is called before the first frame update
    void Start()
    {
        puzzleCompleted = false;
        resetCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(puzzleCompleted && !resetCompleted){
            GameObject[] interactableObjs = GameObject.FindGameObjectsWithTag("interactable");
            
            foreach (GameObject go in interactableObjs){
                if(go.name == "lever_pole" || go.name == "Switch" || go.name.Substring(0, 4) == "Gate" || go.name.Substring(0, 5) == "Input"){
                    go.tag = "Untagged";
                    Destroy(go.GetComponent<Interactable>());
                }
            }
            resetCompleted = true;
        }
    }
}

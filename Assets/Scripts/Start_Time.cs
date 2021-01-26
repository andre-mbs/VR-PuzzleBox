using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.Diagnostics;
using System;
using TMPro;

public class Start_Time : MonoBehaviour
{
    public GameObject startButton;
    public GameObject end_elements;
    public bool puzzleCompleted;
    private bool started;
    private bool ended;
    private Stopwatch stopWatch;
    // Start is called before the first frame update
    void Start()
    {
        stopWatch = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Actions>().triggerDown && startButton.GetComponent<Interactable>().selected && !started){
            stopWatch.Start();
            GameObject.Find("start_elements").SetActive(false);         
            
            started = true;
        }

        if(puzzleCompleted && !ended){
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            
            string elapsedTime = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            end_elements.SetActive(true);
            end_elements.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Time      " + elapsedTime;
            AudioSource[] audioSources = GameObject.Find("radio").GetComponents<AudioSource>();
            audioSources[0].volume = 0;
            audioSources[1].volume = 0;

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("interactable"))
            {
                go.tag = "Untagged";
                Destroy(go.GetComponent<Interactable>());
            }

            ended = true;
        }
    }
}

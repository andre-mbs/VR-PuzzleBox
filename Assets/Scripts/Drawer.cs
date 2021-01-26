using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 xorPosition = GameObject.Find("Gate_XOR").transform.position;
        Vector3 orPosition = GameObject.Find("Gate_OR").transform.position;
        Vector3 xorPlaceholder = GameObject.Find("gate_placeholder1").transform.position;
        Vector3 orPlaceholder = GameObject.Find("gate_placeholder2").transform.position;

        float finalX = 0.533f;

        if((xorPosition.z == xorPlaceholder.z && xorPosition.y == xorPlaceholder.y && xorPosition.x == -1.13f)
        && (orPosition.z == orPlaceholder.z && orPosition.y == orPlaceholder.y && orPosition.x == -1.13f)
        && transform.position.x < finalX){
            Vector3 newPos = transform.position;
            newPos.x += 0.005f;
            transform.position = newPos;
            GameObject.Find("Logic_Puzzle").GetComponent<Logic_Puzzle>().puzzleCompleted = true;
        }
    }
}

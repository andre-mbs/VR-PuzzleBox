using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewDriverPlate : MonoBehaviour
{
    public GameObject keyTile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(keyTile.transform.position.z <= -0.7f && transform.position.y > 1.46f){
            Vector3 newPos = transform.position;
            newPos.y -= 0.01f;
            transform.position = newPos;
        }

        if(transform.position.y <= 1.46f){
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Tile : MonoBehaviour
{
    public GameObject hand;
    public float height;
    public float length;
    public bool triggerDown;
    public GameObject[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> tilesList = new List<GameObject>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("interactable"))
        {
            if(obj.name.Substring(0, 4) == "tile"){
                tilesList.Add(obj);
            }
        }
        tiles = tilesList.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerDown){
            Vector3 newPos = transform.position;
            if(name.Substring(0, 6) == "tile_h"){
                newPos.z = hand.transform.position.z;
            }else if(name.Substring(0, 6) == "tile_v"){
                newPos.y = hand.transform.position.y;
            }

            if(name != "tile_h_2"){
                if(!CheckCollisions(newPos) && newPos.y+height/2<=3.7746f && newPos.y-height/2>=1.6012f && newPos.z+length/2<=1.4613f && newPos.z-length/2>=-0.71216f){
                    transform.position = newPos;
                }
            }else{
                if(!CheckCollisions(newPos) && newPos.y+height/2<=3.7746f && newPos.y-height/2>=1.6012f && newPos.z+length/2<=1.4613f && newPos.z-length/2>=-1.0743f){
                    transform.position = newPos;
                }
            }
            
        }
    }

    private bool CheckCollisions(Vector3 newPos){
        foreach (GameObject go in tiles)
        {
            if(go != gameObject
            && (newPos.y-height/2 <= go.transform.position.y+go.GetComponent<Tile>().height/2 && newPos.y+height/2 >= go.transform.position.y-go.GetComponent<Tile>().height/2)
            && (newPos.z-length/2 <= go.transform.position.z+go.GetComponent<Tile>().length/2 && newPos.z+length/2 >= go.transform.position.z-go.GetComponent<Tile>().length/2)){
                return true;
            }
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPlate : MonoBehaviour
{
    public bool rotate;
    private int angleCount;
    // Start is called before the first frame update
    void Start()
    {
        angleCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotate && angleCount<120){
            transform.Rotate(0f, 0f, -1f);
            angleCount++;
        }
    }
}

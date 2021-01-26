using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public enum gateTypes {OR, AND, XOR, NOT};
    public gateTypes gate;
    public GameObject input1Obj;
    public GameObject input2Obj;
    private bool input1;
    private bool input2;
    public bool output;

    // Start is called before the first frame update
    void Start()
    {
        input1 = false;
        input2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        input1 = input1Obj.GetComponent<Wire>().value;
        input2 = input2Obj.GetComponent<Wire>().value;

        switch(gate){
            case gateTypes.OR:
                output = input1 || input2;
                break;
            case gateTypes.AND:
                output = input1 && input2;
                break;
            case gateTypes.XOR:
                output = (input1 && !input2) || (!input1 && input2);
                break;
            case gateTypes.NOT:
                output = !input1;
                break;
        }
        
    }
}

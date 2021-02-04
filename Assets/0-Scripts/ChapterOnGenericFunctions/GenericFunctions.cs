using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFunctions : MonoBehaviour {
    string stringData = "DATA";
    int intData = 999;
    float floatData = 999.999f;
    Vector3 vector3Data = new Vector3 (9, 9, 9);

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            PrintData(stringData);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            PrintData(intData);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            PrintData(floatData);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            PrintData(vector3Data);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            PrintGenericData(stringData);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            PrintGenericData(intData);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            PrintGenericData(floatData);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            PrintGenericData(vector3Data);
        }
    }


    public void PrintData(string aData) {
        Debug.Log("Received string data " + aData);
    }
    public void PrintData(int aData) {
        Debug.Log("Received int data " + aData);
    }
    public void PrintData(float aData) {
        Debug.Log("Received float data " + aData);
    }
    public void PrintData(Vector3 aData) {
        Debug.Log("Received vector3 data " + aData);
    }

    public void PrintGenericData<T>(T aData) {
        Debug.Log("Received generic data "+aData);
    }

    
}

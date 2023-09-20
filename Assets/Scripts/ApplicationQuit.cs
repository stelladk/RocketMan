using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuit : MonoBehaviour
{

    void Update()
    {
        //Quit Application
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}

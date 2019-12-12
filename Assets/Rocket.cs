using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Thrust(){
        if(Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up);
        }
    }

    private void Rotate(){
        rigidBody.freezeRotation = true;

        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward);
        }else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward);
        }

        rigidBody.freezeRotation = false;
    }
}

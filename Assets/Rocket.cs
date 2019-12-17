using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rtThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    Rigidbody rigidBody;
    AudioSource audio;
    Boolean playing;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
        Music();
    }

    void OnCollisionEnter(Collision collision){
        switch(collision.gameObject.tag){
            case "Friendly":
                print("OK");
                //do nothing
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("Dead");
                //kill player
                break;
        }
    }

    private void Thrust(){
        float frameThrust = mainThrust * Time.deltaTime;

        if(Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up * frameThrust);
            playing = true;
        }else{
            playing = false;
        }
    }

    private void Rotate(){
        rigidBody.freezeRotation = true;

        float frameRotation = rtThrust * Time.deltaTime;

        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward * frameRotation);
        }else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward * frameRotation);
        }

        rigidBody.freezeRotation = false;
    }

    private void Music(){
        if(!audio.isPlaying && playing){
            audio.Play();
        }
        if(!playing){
            audio.Stop();
        }
    }
}

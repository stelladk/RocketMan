using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float rtThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip thrustSoundEffect;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody rigidBody;
    AudioSource audio_src;
    Boolean boosting, rotateLeft, rotateRight;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio_src = GetComponent<AudioSource>();
        boosting = false;
        rotateLeft = rotateRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
        Music();
        MainBoosterParticles();
        SideBoosterParticles();
    }

    private void Thrust(){
        float frameThrust = mainThrust * Time.deltaTime;

        if(Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up * frameThrust);
            boosting = true;
        }else{
            boosting = false;
        }
    }

    private void Rotate(){
        rigidBody.freezeRotation = true;

        float frameRotation = rtThrust * Time.deltaTime;
        
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward * frameRotation);
            rotateLeft = true;
            rotateRight = false;
        }else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward * frameRotation);
            rotateRight = true;
            rotateLeft = false;
        }else{
            rotateLeft = false;
            rotateRight = false;
        }

        rigidBody.freezeRotation = false;
    }

    private void Music(){
        if(!audio_src.isPlaying && boosting){
            audio_src.PlayOneShot(thrustSoundEffect);
        }
        if(!boosting){
            audio_src.Stop();
        }
    }

    private void MainBoosterParticles(){
        if(mainBoosterParticles != null){
            if(!mainBoosterParticles.isPlaying && boosting){
                mainBoosterParticles.Play();
            }
            if(!boosting){
                mainBoosterParticles.Stop();
            }
        }
    }

    private void SideBoosterParticles(){
        if(leftBoosterParticles != null && rightBoosterParticles != null){
            if(!rightBoosterParticles.isPlaying && rotateLeft){
                rightBoosterParticles.Play();
                leftBoosterParticles.Stop();
            }else if(!leftBoosterParticles.isPlaying && rotateRight){
                leftBoosterParticles.Play();
                rightBoosterParticles.Stop();
            }
            if(!rotateLeft && !rotateRight){
                rightBoosterParticles.Stop();
                leftBoosterParticles.Stop();
            }
        }
    }

}

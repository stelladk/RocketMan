using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 1.5f;
    [SerializeField] AudioClip crashSoundEffect;
    [SerializeField] AudioClip finishSoundEffect;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles; 
    
    [SerializeField] bool collisionDisabled = false;

    RocketMovement  movement;
    AudioSource audio_src;

    bool isTransisioning = false;

    void Awake()
    {
        movement = GetComponent<RocketMovement>();
        audio_src = GetComponent<AudioSource>();
    }

    void Update()
    {
        handleDebugKeys();
    }

    void OnCollisionEnter(Collision collision){
        if(isTransisioning || collisionDisabled) return;
        switch(collision.gameObject.tag){
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                //next level
                finishSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                //kill player
                crashSequence();
                break;
        }
    }

    void crashSequence(){
        isTransisioning = true;
        audio_src.Stop();
        audio_src.PlayOneShot(crashSoundEffect, 0.7f);
        Invoke("reloadLevel", nextLevelDelay);
        movement.enabled = false;
        crashParticles.Play();
    }

    void finishSequence(){
        isTransisioning = true;
        audio_src.Stop();
        audio_src.PlayOneShot(finishSoundEffect);
        Invoke("loadNextLevel", nextLevelDelay);
        movement.enabled = false;
        finishParticles.Play();
    }

    void reloadLevel(){
        int sceneIndex =  SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    void loadNextLevel(){
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (sceneIndex + 1)%SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void handleDebugKeys()
    {
        // Load next level cheat
        if(Input.GetKeyDown(KeyCode.L)){
            loadNextLevel();
        }

        // Toggle collision cheat
        if(Input.GetKeyDown(KeyCode.C)){
            collisionDisabled = !collisionDisabled;
        }
    }
}

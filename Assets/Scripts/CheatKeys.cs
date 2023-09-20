using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatKeys : MonoBehaviour
{

    CollisionHandler collision;

    void Awake()
    {
        collision = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        handleKeys();
    }

    void handleKeys(){
        //load next level cheat
        if(Input.GetKey(KeyCode.L)){
            loadNextLevel();
        }

        //disable collision cheat
        if(Input.GetKey(KeyCode.C)){
            collision.enabled = false;
        }
    }

    void loadNextLevel(){
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (sceneIndex + 1)%SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void disableCollision(){

    }
}

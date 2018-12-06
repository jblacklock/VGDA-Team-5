using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>(); 
    }

    //loads a scene at build index
    public void DoLoadScene(int sceneIndex)
    {
        audio.Play(); 
        SceneManager.LoadScene(sceneIndex);
    }
}

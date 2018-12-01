using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

        //loads a scene at build index
	    public void doLoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

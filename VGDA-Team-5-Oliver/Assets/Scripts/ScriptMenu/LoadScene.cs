using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public Animator blackFade;

    private AudioSource audio;
    private bool animating; 

    private void Start()
    {
        animating = false; 
        audio = GetComponent<AudioSource>(); 
    }
    

    //loads a scene at build index
    public void DoLoadScene(int sceneIndex)
    {
        StartCoroutine("ChangeScene", sceneIndex); 
    }
    
    public IEnumerator ChangeScene(int sceneIndex)
    {
        audio.Play();
        blackFade.SetTrigger("FadeOut");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(sceneIndex);
    }
}

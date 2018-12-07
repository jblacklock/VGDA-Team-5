using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public Animator blackFade;

    private void Start()
    {
        StartCoroutine("FadeIn");
    }

    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine("FadeOut", sceneIndex); 
    }


    public IEnumerator FadeIn()
    {
        blackFade.SetTrigger("FadeIn");

        yield return new WaitForSeconds(2);

        GameObject.Find("BlackFade").GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0); 
    }

    public IEnumerator FadeOut(int sceneIndex)
    {
        GameObject.Find("BlackFade").GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

        blackFade.SetTrigger("FadeOut");

        yield return new WaitForSeconds(4);

        SceneManager.LoadScene(sceneIndex);
    }
}


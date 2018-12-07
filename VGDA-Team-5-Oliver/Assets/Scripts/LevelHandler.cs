using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour {

    public string sceneName;
    public Animator blackFade;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Data.Instance().SetPositionInWorldMap(other.transform.position);
            Debug.Log(Data.Instance().GetPositionInWorldMap()); 
            Data.Instance().SetLastPortal(gameObject);

            StartCoroutine("FadeOut");
        }
    }

    public IEnumerator FadeOut()
    {
        GameObject.Find("BlackFade").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        blackFade.SetTrigger("FadeOut");

        yield return new WaitForSeconds(4);

        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour {

    public Animator blackFade;

    private List<string> portals; 
    
	void Start () {

        StartCoroutine("FadeIn");

        portals = Data.Instance().GetUsedPortals(); 

        foreach(string portal in portals)
        {
            GameObject.Find(portal).SetActive(false); 
        }
        
	}

    public IEnumerator FadeIn()
    {
        blackFade.SetTrigger("FadeIn");

        yield return new WaitForSeconds(2);

        GameObject.Find("BlackFade").GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        
    }

}

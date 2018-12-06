using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour {

    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Data.Instance().SetPositionInWorldMap(other.transform.position);
            Debug.Log(Data.Instance().GetPositionInWorldMap()); 
            Data.Instance().SetLastPortal(gameObject); 

            SceneManager.LoadScene(sceneName); 
        }
    }
}

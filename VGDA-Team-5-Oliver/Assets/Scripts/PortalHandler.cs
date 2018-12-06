using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour {

    private List<string> portals; 
    
	void Start () {
        portals = Data.Instance().GetUsedPortals(); 

        foreach(string portal in portals)
        {
            GameObject.Find(portal).SetActive(false); 
        }
        
	}
	
}

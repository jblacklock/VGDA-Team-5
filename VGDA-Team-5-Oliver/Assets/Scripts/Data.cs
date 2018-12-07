using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data {

    private static Data _instance; 
    private Vector3 positionInWorldMap; 
    private GameObject player;
    private string lastPortal;
    private List<string> usedPortals; 

    private Data()
    {
        usedPortals = new List<string>(); 
        positionInWorldMap = new Vector3(0f, 0f, 25f);
    }
    
    public static Data Instance()
    {
        if(_instance == null)
        {
            _instance = new Data(); 
        }
        return _instance; 
    }


    public Vector3 GetPositionInWorldMap()
    {
        return positionInWorldMap; 
    }

    public void SetPositionInWorldMap(Vector3 position)
    {
        positionInWorldMap = position; 
    }

    public void SetLastPortal(GameObject portal)
    {
        lastPortal = portal.name; 
    }

    public string GetLastPortal()
    {
        return lastPortal; 
    }

    public void DeactivateLastPortal()
    {
        if(lastPortal != null)
        {
            usedPortals.Add(lastPortal); 
        }
    }

    public List<string> GetUsedPortals()
    {
        return usedPortals; 
    }

}

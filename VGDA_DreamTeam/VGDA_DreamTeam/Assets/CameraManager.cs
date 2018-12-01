using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public float offset = 2f;

    private Vector3 cameraPos;
    private bool movable; 

	// Use this for initialization
	void Start () {
        cameraPos = gameObject.transform.position;
        movable = false; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FollowPlayer(GameObject player) 
    {
        while(movable)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offset, player.transform.position.z);
            yield return new WaitForEndOfFrame(); 
        }
    }

    public void ChangeMovable()
    {
        movable = !movable; 
    }
}

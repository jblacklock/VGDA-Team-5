using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public float offset = 2f;
    public Camera camera; 

    private Vector3 cameraStartPos;
    private bool movable; 

	// Use this for initialization
	void Start () {
        cameraStartPos = camera.transform.position;
        movable = false; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FollowPlayer(GameObject player) 
    {
        while(movable)
        {
            camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offset, player.transform.position.z - offset);
            yield return new WaitForEndOfFrame(); 
        }
    }

    public void ResetCamera()
    {
        camera.transform.position = cameraStartPos;
    }

    public void ChangeMovable()
    {
        movable = !movable; 
    }
}

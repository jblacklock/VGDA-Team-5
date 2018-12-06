using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public float playerOffset = 2f;
    public float enemyOffset = 6f; 
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
            if(player != null)
            {
                camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + playerOffset, player.transform.position.z - playerOffset);
                yield return new WaitForEndOfFrame(); 
            } else
            {
                yield break; 
            }
        }
    }

    public IEnumerator FollowEnemy(GameObject enemy)
    {
        while (movable)
        {
            if(enemy != null)
            {
                camera.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + enemyOffset, enemy.transform.position.z - enemyOffset);
                yield return new WaitForEndOfFrame();
            } else
            {
                yield break; 
            }
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

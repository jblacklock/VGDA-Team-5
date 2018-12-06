using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotator : MonoBehaviour {

    public float speed; 

    private SpriteRenderer circle;
    private bool rotate; 

	// Use this for initialization
	void Start () {
        circle = GetComponent<SpriteRenderer>();
        rotate = true; 
	}
	
	// Update is called once per frame
	void Update () {
		if(rotate)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
	}

    public void Rotate()
    { 
        rotate = !rotate;
    }
}

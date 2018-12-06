using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatMovement : MonoBehaviour {

    public Camera cam;
    public float offSetY;
    public float offSetZ;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private UnityEngine.AI.NavMeshHit navHit;


	// Use this for initialization
	void Start () {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) //if left mouse button is pressed 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //get mouse position 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) //if raycast hits something 
            {
                navAgent.SetDestination(hit.point); //set destination to mouse position 
            }
        }

        cam.transform.position = new Vector3(transform.position.x, transform.position.y + offSetY, transform.position.z - offSetZ);
    }
}

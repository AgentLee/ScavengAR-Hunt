using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavController : MonoBehaviour 
{
	NavMeshAgent agent;
	public Transform target;
	public EnemyController drone;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(target.position);

		float distance = Vector3.Distance(drone.transform.position, this.transform.position);
		Debug.Log(distance);
	}
	
	// Update is called once per frame
	void Update () 
	{
		agent.SetDestination(target.position);
		if(agent.pathStatus == NavMeshPathStatus.PathComplete) {
			drone.transform.position = Vector3.MoveTowards(drone.transform.position, this.transform.position, drone.speed * Time.deltaTime);

			// float distance = Vector3.Distance(drone.transform.position, this.transform.position);
			// Debug.Log(distance);
		}
	}
}

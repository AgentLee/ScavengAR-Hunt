using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavController : MonoBehaviour 
{
	NavMeshAgent agent;
	public Transform target;
	public EnemyController drone;

	float timeToNewTarget = 0;
	
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
		if(agent.pathStatus == NavMeshPathStatus.PathComplete && timeToNewTarget >= 3.0f) {
			// Set new target
			Vector3 randomDirection = Random.insideUnitSphere * 4.0f;
			randomDirection += target.position;

			NavMeshHit hit;
			Vector3 finalPos = Vector3.zero;

			if(NavMesh.SamplePosition(randomDirection, out hit, 4.0f, 1)) {
				finalPos = hit.position;
			}

			target.position = finalPos;

			// drone.transform.position = Vector3.MoveTowards(drone.transform.position, this.transform.position, drone.speed * Time.deltaTime);

			// float distance = Vector3.Distance(drone.transform.position, this.transform.position);
			// Debug.Log(distance);
			agent.SetDestination(target.position);

			timeToNewTarget = 0;
		}else {
			timeToNewTarget += Time.deltaTime;
		}

	}
}

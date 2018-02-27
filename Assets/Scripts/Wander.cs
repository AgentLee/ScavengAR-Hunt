using UnityEngine;
using System.Collections;
 
public class Wander : MonoBehaviour
{
	public GameObject target;

	public float speed = 0.025f;
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 30;

	Vector3 posOffset = new Vector3();
	Vector3 tempPos = new Vector3();
 
	bool hitShield = false;

	float degreesPerSec = 15.0f;
	float amp = 1.5f;
	float frequency = 0.5f;

	public bool reachedHigh = false;
	public bool fullCycle = false;

	float minSin = Mathf.Infinity;
	
	void Start ()
	{
		posOffset = transform.position;
		amp = target.GetComponent<Collider>().bounds.max.y * 1.25f;
	}

	void Update ()
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, target.transform.position);
		// Debug.DrawRay(ray.origin, ray.direction, Color.red);
		// Physics.Raycast(transform.position, target.transform.position, out hit, 5.0f)
		
		// float distToTarget = Vector3.Distance(transform.position, target.transform.position);
		// if(distToTarget > 5.0f) {
		// 	transform.position += transform.forward * speed;
		// }
		// else {
		// 	Hover();
		// 	transform.position += transform.forward * speed;

		// 	if(reachedHigh) {
		// 		target.transform.position = target.transform.position + new Vector3(10, 0, 0);
		// 		reachedHigh = false;
		// 	}
		// }

		// if(hitShield && !fullCycle) {
		// 	Hover();
		// }

		Hover();
		// Look at target
		Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
		
		// transform.position += transform.forward * speed;
	}
 
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Shield") {
			hitShield = true;
		}	
	}

	void Hover()
	{
		float sin = Mathf.Abs(Mathf.Sin(Time.fixedTime * Mathf.PI * frequency));
		
		if(sin >= 0.9f) {
			reachedHigh = true;			
		}


		tempPos = posOffset;
		tempPos.y += sin * amp;

		transform.position = new Vector3(transform.position.x, tempPos.y, transform.position.z);
	}
}
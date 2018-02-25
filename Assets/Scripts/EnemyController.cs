using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour 
{
	public GameObject player;
	public GameObject leftEye;
	public GameObject rightEye;
	public GameObject center;

	public GameObject bullet;

	private Rigidbody rb;

	public bool hit;

	public bool grounded;

	public Transform flyByPos;

	public float speed;
	bool launch;

	public bool DEBUG_LAUNCH;
	public bool DEBUG_TILT;
	public bool DEBUG_DODGE;

	public bool dodging;
	public float dodgeTime;

	private Vector3 origPos;

	public Transform targetPosition;

	float fireTime;

	public Transform cubeTarget;

	float distToCube;

	// Use this for initialization
	void Start () 
	{
		origPos = transform.position;
		
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();

		// foreach(GameObject child in this.transform) {
		// 	if(child.name == "Left Eye") {
		// 		leftEye = child;
		// 	}
		// 	else if(child.name == "Right Eye") {
		// 		rightEye = child;
		// 	}
		// 	else if(child.name == "Center") {
		// 		center = child;
		// 	}
		// }

		// Orient the drone towards the player
		// Vector3 lookAt = player.transform.position - transform.position;
		// lookAt.y = 0;

		// Quaternion rotation = Quaternion.LookRotation(lookAt);
		// transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

		hit = false;
		grounded = false;
		launch = false;
		dodging = false;
		fireTime = 0;

		prevParentPos = transform.parent.transform;
		distToCube = Vector3.Distance(cubeTarget.position, transform.position);
		// distToCube = Mathf.Abs(cubeTarget.position.z - transform.position.z);

		// StartCoroutine(FlyToPoint(3.0f));
		// flyByPos.position = this.transform.position + new Vector3(0,0, 10);
		// flyByPos.position =
		// speed = 50;
		// Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), flyByPos.position, new Quaternion());
	}
	
	// IEnumerator FlyToPoint(float duration)
	// {
	// 	float time = 0;

	// 	while(time < duration) {
	// 		time += Time.deltaTime;
	// 		this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.parent.transform.position, Time.deltaTime * 10.0f);		
	// 		yield return null;
	// 	}
	// }

	float orientSpeed = 2.5f;
	void OrientDrone()
	{
		Debug.Log("ORIENTING");
		Vector3 dir = cubeTarget.position - this.transform.position;
		dir.y = 0;

		Quaternion rot = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Slerp(transform.rotation, rot, orientSpeed * Time.deltaTime);
	}

	void Update()
	{

	}

	bool oriented = false;
	Transform prevParentPos = null;

	// Update is called once per frame
	void FixedUpdate () 
	{
		float dY = Mathf.Abs(transform.position.y - cubeTarget.position.y);
		// float dX = Mathf.Abs(transform.position.z - cubeTarget.position.z);

		float currDist = Vector3.Distance(cubeTarget.position, transform.position);
		// Debug.Log("CURRENT: "+currDist+" | FULL: "+distToCube+" | PERCENT: "+(100.0f*currDist/distToCube));

		// This only works during descents
		// Need to find a way to do ascension
		// if((currDist / distToCube) <= .90f) {
		// 	Debug.Log(dY);
		// 	if(dY > 0.1f)
		// 		transform.position -= new Vector3(0, 2.0f * Time.deltaTime, 0);
		// }

		{
			// if(fireTime < 5) {
			// 	fireTime += Time.deltaTime;
			// }
			// else {
				// fireTime = 0;
				// GameObject spawnedBullt = Instantiate(bullet, center.transform.position, bullet.transform.rotation);
				// spawnedBullt.GetComponent<Rigidbody>().AddForce(this.transform.forward * 10.0f);
				// Destroy(spawnedBullt, 0.5f);
			// }
		}

		// Check to see if the player is in the drone's fov
		{
			// RaycastHit hit;
			// Vector3 playerPosCenter = player.transform.position - this.transform.position;
			// Vector3 playerPosRight	= player.transform.position - rightEye.transform.position;
			// Vector3 playerPosLeft	= player.transform.position - leftEye.transform.position;

			// Ray Rray = new Ray(rightEye.transform.position, playerPosRight);
			// Ray Lray = new Ray(leftEye.transform.position, playerPosLeft);
			// Ray Cray = new Ray(this.transform.position, playerPosCenter);

			// Debug.DrawRay(Rray.origin, Rray.direction, Color.green);
			// Debug.DrawRay(Lray.origin, Lray.direction, Color.red);
			// Debug.DrawRay(Cray.origin, Cray.direction, Color.blue);

			// float step = 5f * Time.deltaTime;
			// transform.position = Vector3.MoveTowards(transform.position, flyByPos.position, step);

			// float distTraveled = Vector3.Distance(origPos, transform.position);
			// Debug.Log(Vector3.Distance(transform.position, targetPosition.position));
			
			// if(Vector3.Distance(transform.position, targetPosition.position) > 1) {
			// 	transform.LookAt(targetPosition);
			// 	rb.AddRelativeForce(Vector3.forward * 2.0f, ForceMode.Force);
			// }
			// else {
			// 	rb.velocity = rb.velocity * 0.9f;
			// 	// transform.LookAt(targetPosition);
			// 	// rb.AddRelativeForce(Vector3.forward * 0.9f, ForceMode.Force);
			// }
			// else {
			// 	if(!launch) {
					
			// 		rb.AddForce(transform.forward * 2.0f);
			// 		launch = true;
			// 	}
			// }
		}

		float distToTarget = Vector3.Distance(targetPosition.position, this.transform.position);
		if(distToTarget > 0.125f) {
			transform.LookAt(targetPosition);

			if(distToTarget <= 0.025f) {
				rb.velocity = rb.velocity * 0.9f;
			}
			else {
				rb.AddRelativeForce(Vector3.forward * .75f, ForceMode.Force);
			}
		}


		if(DEBUG_LAUNCH) {
			float distance = Vector3.Distance(flyByPos.position, this.transform.position);

			if(!launch) {
				rb.AddForce((flyByPos.position - this.transform.position) * 10f);
				launch = true;
			}
			else {
				rb.AddForce((flyByPos.position - this.transform.position) * 7f);			
			}

			// Debug.Log(distance);

			if(distance <= 5) {
				if(distance < 0.5f) {
					rb.velocity = Vector3.zero;
					// Prepare for next target
					launch = false;
				}
				else {
					rb.velocity = rb.velocity * 0.1f;
				}
			}
		}

		if(Time.time - dodgeTime >= 0.75 && dodging) {
			dodging = false;
			rb.velocity = -rb.velocity;
		}
		
		if(Vector3.Distance(origPos, transform.position) <= .0125f) {
			rb.velocity = Vector3.zero;
		}

		// Reset rotations
		// https://forum.unity.com/threads/smoothly-tilting-an-object-from-left-to-right.3262/
		Quaternion target = Quaternion.Euler(0, 0, 0);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2.0f);
	}

	// Each bullet can call this function. If the bullet is close to the drone,
	// the drone will try to dodge it.
	// TODO
	// Each new bullet makes it tilt. Need to remove this.
	public void DodgeBullet(GameObject bullet)
	{
		float distLeft = Vector3.Distance(bullet.transform.position, this.leftEye.transform.position);
		float distRight = Vector3.Distance(bullet.transform.position, this.rightEye.transform.position);

		if(!dodging && distLeft < 0.5 && distLeft < distRight) {
			Quaternion target = Quaternion.Euler(0, 0, -1.0f * -75.0f);
			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2.0f);
			rb.AddForce(Vector3.right * -1.0f * 1.0f);

			dodgeTime = Time.time;
		}
		else if(!dodging && distRight < 0.5 && distRight < distLeft) {
			Quaternion target = Quaternion.Euler(0, 0, 1.0f * -75.0f);
			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2.0f);
			rb.AddForce(Vector3.right * 1.0f * 1.0f);

			dodgeTime = Time.time;
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Bullet") {
			rb.useGravity = true;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			
			// Unparent to stop moving
			transform.parent = null;
			hit = true;
		}
		else if(collisionInfo.collider.tag == "Ground") {
			grounded = true;
		}
	}
}

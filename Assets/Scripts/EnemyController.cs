using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public GameObject player;
	public GameObject leftEye;
	public GameObject rightEye;

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

	// Use this for initialization
	void Start () 
	{
		origPos = transform.position;
		
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();

		Vector3 lookAt = player.transform.position - transform.position;
		lookAt.y = 0;

		Quaternion rotation = Quaternion.LookRotation(lookAt);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

		hit = false;
		grounded = false;

		launch = false;

		dodging = false;

		// flyByPos.position = this.transform.position + new Vector3(0,0, 10);
		// flyByPos.position =
		// speed = 50;
		// Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), flyByPos.position, new Quaternion());
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		// Check to see if the player is in the drone's fov
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

		if(DEBUG_TILT) {
			float axis = Input.GetAxis("Horizontal");
			// https://forum.unity.com/threads/smoothly-tilting-an-object-from-left-to-right.3262/
			Quaternion target = Quaternion.Euler(0, 0, axis * -75.0f);
			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2.0f);
		}

		if(DEBUG_DODGE) {
			GameObject bullet = GameObject.FindGameObjectWithTag("Bullet");
			if(bullet != null) {
				float leftDistance = Vector3.Distance(leftEye.transform.position, bullet.transform.position);
				float rightDistance = Vector3.Distance(rightEye.transform.position, bullet.transform.position);

				if(leftDistance < 1 && leftDistance < rightDistance) {
					Quaternion target = Quaternion.Euler(0, 0, -1.0f * -75.0f);
					transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2.0f);
					rb.AddForce(Vector3.right * -1.0f * 1.0f);
				}
				else if(rightDistance < 1 && rightDistance < leftDistance) {
					Quaternion target = Quaternion.Euler(0, 0, 1.0f * -75.0f);
					transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2.0f);
					rb.AddForce(Vector3.right * 1.0f * 1.0f);
				}
			}
		}
	}

	public void DodgeBullet(int axis)
	{
		// Left
		if(axis == -1) {
			Quaternion target = Quaternion.Euler(0, 0, -1.0f * -75.0f);
			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2.0f);
			rb.AddForce(Vector3.right * -1.0f * 1.0f);

			dodgeTime = Time.time;
		}
		// Right
		else if(axis == 1) {
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

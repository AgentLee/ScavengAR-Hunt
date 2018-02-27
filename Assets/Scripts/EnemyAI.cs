using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TARGETS
{
	TOP_RIGHT 		= 0,
	BOTTOM_RIGHT 	= 1,
	TOP_LEFT 		= 2,
	BOTTOM_LEFT 	= 3,
	BASE 			= 4
}

public class EnemyAI : MonoBehaviour 
{
	public Transform spawnPoint;
	public GameObject bullet;

	public GameObject shootable;
	public GameObject target;
	public GameObject playerBase;
	public GameObject top;
	public GameObject topRight;
	public GameObject topLeft;
	public GameObject bottomRight;
	public GameObject bottomLeft;

	public GameManager manager;

	bool reachedBase;

	float degreesPerSec = 15.0f;
	float amp = 0.25f;
	float frequency = 1.0f;

	float distToTarget;

	Vector3 posOffset = new Vector3();
	Vector3 tempPos = new Vector3();

	int currentTarget;
	// Use this for initialization
	void Start () 
	{
		shootable = GameObject.Find("Shootable");

		posOffset = transform.position;

		target = playerBase;
		currentTarget = 4;
		distToTarget = Vector3.Distance(transform.position, target.transform.position);

		reachedBase = false;

		// GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, bullet.transform.rotation);
		// spawnedBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 10.0f);		
		// spawnedBullet.GetComponent<EnemyBullet>()
	}
	
	float fireTime;
	// Update is called once per frame
	void Update () 
	{
		// DEBUG SHOT
		if(Input.GetKeyDown(KeyCode.Space)) {
			GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, transform.rotation * Quaternion.Euler(90, 0, 0));
			spawnedBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 100.0f);
		} 

		if(fireTime >= 2.0f && target == playerBase) {
			GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, transform.rotation * Quaternion.Euler(90, 0, 0));
			spawnedBullet.GetComponent<EnemyBullet>().shooter = gameObject;

			// Pick a random spot to shoot at.
			int idx = Random.Range(0, shootable.transform.childCount);

			// spawnedBullet.GetComponent<Rigidbody>().AddForce((playerBase.transform.position - transform.position).normalized * 500.0f);
			spawnedBullet.GetComponent<Rigidbody>().AddForce((shootable.transform.GetChild(idx).transform.position - transform.position).normalized * 10.0f);
			
			fireTime = 0;
		}
		else {
			fireTime += Time.deltaTime;
		}

		float currDistToTarget = Vector3.Distance(transform.position, target.transform.position);
		if(currDistToTarget > 1.5f) {
			transform.LookAt(target.transform);
			GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1.5f, ForceMode.Force);

			Debug.Log(target.transform.position);
		}
		else {
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.75f;			

			if(GetComponent<Rigidbody>().velocity.magnitude <= 0.15) {
				int randomTarget = Random.Range(0, 5);
				while(randomTarget == currentTarget) {
					randomTarget = Random.Range(0, 5);
				}

				currentTarget = randomTarget;

				switch(randomTarget) {
					case 0:
						target = topRight;
						break;
					case 1:
						target = bottomRight;
						break;
					case 2:	
						target = topLeft;
						break;
					case 3:
						target = bottomLeft;
						break;
					case 4:
						target = playerBase;
						break;
					default:
						target = playerBase;
						break;
				}
			}

			// Look at target
			Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
		}
	}

	void Hover()
	{
		tempPos = posOffset;
		tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amp;
		
		if(Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) == 1.0f || Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) == -1.0f) {
			Debug.Log(Mathf.Sin(Time.fixedTime * Mathf.PI * frequency));
		}

		transform.position = new Vector3(transform.position.x, tempPos.y, transform.position.z);
	}
}

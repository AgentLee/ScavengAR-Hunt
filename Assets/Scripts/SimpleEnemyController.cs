using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleEnemyController : MonoBehaviour 
{
	private Transform enemy;
	private Rigidbody rb;

	public bool grounded;
	public float speed;

	public GameObject target;	// Set in inspector
	public GameObject bullet;	// Set in inspector
	Scene scene;

	private int id;				
	private float fireTime, fireRate;
	public float accuracy;
	public float shotEpsilon;
	public int shotFOV;
	public bool hit;
	
	// Use this for initialization
	void Start () 
	{
		enemy = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();

		scene = SceneManager.GetActiveScene();

		speed = 0.25f;
		grounded = true;

		id = Random.Range(0,8);
		fireTime = 0;
		fireRate = Random.Range(10, 20);
		// fireRate = Random.Range(0, 10);
		shotEpsilon = 100;
		accuracy = 0;
		shotFOV = 45;

		hit = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(scene.name == "DroneTest2") {
			if(fireTime >= fireRate) {
				Fire();
				fireTime = 0;
			}
			else {
				fireTime += Time.deltaTime;
			}

			if(enemy.position.y < -25.0f) {
				Destroy(gameObject);
			}
		}
		else {
			if(!hit) {
				RaycastHit intersection;
				Ray ray = new Ray(enemy.position, -enemy.up);
				if(Physics.Raycast(ray, out intersection)) {
					if(intersection.collider.tag != "Enemy") {
						if(fireTime >= fireRate && Random.Range(0,8) == id) {
							SimplePlayerController player = GameObject.Find("Player").GetComponent<SimplePlayerController>();
							Vector3 shotDir = (player.transform.position - transform.position).normalized;

							GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);

							GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
							for(int i = 0; i < enemies.Length; ++i) {
								Physics.IgnoreCollision(enemies[i].GetComponent<Collider>(), spawnedBullet.GetComponent<Collider>(), true);
							}
							spawnedBullet.GetComponent<SimpleEnemyBulletController>().shooter = gameObject;
							spawnedBullet.GetComponent<Rigidbody>().AddForce(-enemy.up * 500.0f);

							Debug.DrawRay(transform.position, -enemy.up * 10.0f, Color.red, 2);
							fireTime = 0;
							fireRate = Random.Range(0, 10);
						}
						else {
							fireTime += Time.deltaTime;
						}
					}
				}
			}
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		transform.parent = null;

		if(collisionInfo.collider.tag == "Enemy") {
			hit = true;
		}

		if(collisionInfo.collider.tag == "Bullet" || transform.parent == null) {
			hit = true;
			rb.useGravity = true;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
		else if(collisionInfo.collider.tag == "Ground") {
			grounded = true;
		}
	}

	bool canShoot(out Vector3 shotDir)
	{
		// https://answers.unity.com/questions/321323/how-to-give-your-enemy-gun-inaccuracy.html
		// float myIntx = (float)Random.Range(-accuracy,accuracy)/1000;
		// float myInty = (float)Random.Range(-accuracy,accuracy)/1000;
		// float myIntz = (float)Random.Range(-accuracy,accuracy)/1000;
		// Vector3 newVector = new Vector3(transform.forward.x + myIntx, transform.forward.y + myInty, transform.forward.z + myIntz);

		// https://answers.unity.com/questions/180605/ai-enemy-shoot-precision.html
		// shotEpsilon = 0 --> 100% accuracy
		shotDir = target.transform.position + Random.insideUnitSphere * shotEpsilon - transform.position;  
		// Makes no sense to have the enemy shoot from behind, clamp to positive x
		shotDir.x = Mathf.Abs(shotDir.x);
		
		// Check to see the shot is within a reasonable shot range
		float dot = Vector3.Dot(transform.forward, shotDir);
		int theta = Mathf.RoundToInt(Mathf.Acos(dot) * 180 / Mathf.PI);

		return theta <= shotFOV;
	}

	void Fire()
	{
		Vector3 shotDir = Vector3.zero;
		if(canShoot(out shotDir)) {
			GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
			spawnedBullet.GetComponent<SimpleEnemyBulletController>().shooter = gameObject;
			spawnedBullet.GetComponent<Rigidbody>().AddForce(shotDir * 500.0f);
			Physics.IgnoreCollision(GetComponent<Collider>(), spawnedBullet.GetComponent<Collider>(), true);
		}

		Debug.DrawRay(transform.position, transform.forward * 10000F, Color.cyan, 2);
		Debug.DrawRay(transform.position, shotDir * 10000F, Color.red, 2);
		// Debug.DrawRay(transform.position, newVector * 1000F, Color.red, 2);  
	}
}

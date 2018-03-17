using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Powerups
{
	public bool weaponSpread;
	public bool shields;
	public bool control;

	public Powerups(bool b)
	{
		weaponSpread = false;
		shields = false;
		control = false;
	}
};

public class SimplePlayerController : MonoBehaviour 
{
	Quaternion rot;
	private Transform player;
	private Rigidbody rb;

	public int score;					
	public float speed;					
	public float minBounds, maxBounds;	// Bounds to prevent player from falling off

	private bool hit;					// Flag used to trigger blink effect
	public int numLives;				// Player starts off with 5 lives

	public GameObject bullet;
	public Joystick joystick;			// Takes input from Joystick to allow movement

	public Powerups powerups;			// Data structure to hold the player power ups collected 
	public AudioSource blasterSound;
	public AudioSource explosionSound;

	// Use this for initialization
	void Start () 
	{
		GetComponent<HighScores>().RetrieveScores();
		
		rot = transform.rotation;
		player = this.transform;
		score = 0;

		speed = 0.5f;
		minBounds = -12.0f;
		maxBounds = 12.0f;

		rb = GetComponent<Rigidbody>();
		// rb.isKinematic = false;
		// rb.useGravity = true;

		hit = false;
		numLives = (PlayerPrefs.GetInt("PowerUpFish") == 1) ? 6 : 5;

		powerups = new Powerups(true);
	}

	// Lock rotation on pitch (y)
	// void LateUpdate()
	// {
	// 	Quaternion quat = player.rotation;
	// 	player.rotation = Quaternion.Euler(quat.eulerAngles.x, rot.eulerAngles.y, quat.eulerAngles.z);
	// 	// player.rotation = rot;
	// }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		MovePlayer();

		// for(int i =0; i < Input.touchCount; ++i) {
		// 	Fire();
		// }

		if(Input.GetKey(KeyCode.Space)) {
			Fire();
		}

		// Ray ray = new Ray(player.position, player.up);
		// Ray ray2 = new Ray(player.position, player.up + player.right/3);
		// Ray ray3 = new Ray(player.position, player.up - player.right/3);
		// Debug.DrawRay(ray.origin, ray.direction, Color.red, 5.0f);
		// Debug.DrawLine(ray2.origin, ray2.direction * 100, Color.green, 5.0f);
		// Debug.DrawRay(ray3.origin, ray3.direction * 100, Color.blue, 5.0f);
	}

	public void MovePlayer()
	{
		// Touch touch = Input.GetTouch(0);
		// if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
		// 	Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
		// 	transform.position = Vector3.Lerp(transform.position, touchPos, Time.deltaTime);
		// }

		float h = 0;
		// if(PlayerPrefs.GetInt("Tilt") == 1) {
			// h = Input.acceleration.x;
		// } 
		// else {
			if(Application.isEditor) {
				h = Input.GetAxis("Horizontal");
			}
			else {
				// if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				// 	Vector2 delta = Input.GetTouch(0).deltaPosition;

				// 	h = -delta.x;
				// }

				h = joystick.InputDirection.x;
			}
		// }
		
		if(player.position.x < minBounds && h < 0 ||
			player.position.x > maxBounds && h > 0)
			h = 0;

		player.position += Vector3.right * h * speed;
	}

	public void Fire()
	{
		blasterSound.Play();
		
		// powerups.weaponSpread = PlayerPrefs.HasKey("Fish");

		bool FPS = false;
		if(FPS) {
			GameObject spawnedBullet = Instantiate(bullet, Camera.main.transform.position, bullet.transform.rotation);
			Physics.IgnoreCollision(spawnedBullet.GetComponent<Collider>(), GetComponent<Collider>(), true);
			spawnedBullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 5000.0f);

			if(powerups.weaponSpread) {
				GameObject spawnedBulletL = Instantiate(bullet, Camera.main.transform.position - new Vector3(0.5f, 0, 0), new Quaternion());			
				Physics.IgnoreCollision(spawnedBulletL.GetComponent<Collider>(), GetComponent<Collider>(), true);
				spawnedBulletL.GetComponent<Rigidbody>().AddForce((player.forward - player.right/5) * 500.0f);

				GameObject spawnedBulletR = Instantiate(bullet, Camera.main.transform.position + new Vector3(0.5f, 0, 0), new Quaternion());			
				Physics.IgnoreCollision(spawnedBulletR.GetComponent<Collider>(), GetComponent<Collider>(), true);
				spawnedBulletR.GetComponent<Rigidbody>().AddForce((player.forward + player.right/5) * 500.0f);
			}
		}
		else {
			if(PlayerPrefs.GetInt("WeaponSpread") == 1) {
				GameObject spawnedBulletL = Instantiate(bullet, player.position - new Vector3(0.5f, 0, 0), new Quaternion());			
				Physics.IgnoreCollision(spawnedBulletL.GetComponent<Collider>(), GetComponent<Collider>(), true);
				spawnedBulletL.GetComponent<Rigidbody>().AddForce((player.up - player.right/5) * 500.0f);

				GameObject spawnedBulletR = Instantiate(bullet, player.position + new Vector3(0.5f, 0, 0), new Quaternion());			
				Physics.IgnoreCollision(spawnedBulletR.GetComponent<Collider>(), GetComponent<Collider>(), true);
				spawnedBulletR.GetComponent<Rigidbody>().AddForce((player.up + player.right/5) * 500.0f);
			}

			// GameObject spawnedBullet = Instantiate(bullet, player.position, new Quaternion());			
			GameObject spawnedBullet = Instantiate(bullet, player.position, bullet.transform.rotation);
			Physics.IgnoreCollision(spawnedBullet.GetComponent<Collider>(), GetComponent<Collider>(), true);
			spawnedBullet.GetComponent<Rigidbody>().AddForce(player.up * 500.0f);
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		// if(collisionInfo.collider.tag == "Ground") {
		// 	rb.isKinematic = true;
		// } 
		if(collisionInfo.collider.tag == "Enemy Bullet") {
			if(!hit && !collisionInfo.collider.GetComponent<SimpleEnemyBulletController>().grounded) {
				explosionSound.Play();
				
				// Make sure there are enough lives for the player to blink
				if(--numLives <= 0) {
					Destroy(gameObject);
					UpdateScores();
				}
				else {
					StartCoroutine(Blink());
				}
			}
		}
	}

	IEnumerator Blink()
	{
		hit = true;

		for(int blink = 0; blink < 5; ++blink) {
			yield return new WaitForSeconds(.05f);
			GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled; 
			yield return new WaitForSeconds(.05f);
		}

		// Make sure the player is turned back on
		GetComponent<MeshRenderer>().enabled = true; 
		hit = false;
	}

	public void UpdateScores()
	{
		Debug.Log("Updating...");

		PlayerPrefs.SetInt("PlayerScore", score);

		string _name 	= PlayerPrefs.GetString("PlayerName");
		string _phone 	= PlayerPrefs.GetString("PlayerPhone");
		int _score 		= PlayerPrefs.GetInt("PlayerScore");
		string _email 	= PlayerPrefs.GetString("PlayerEmail");

		// Update the database
		GetComponent<HighScores>().AddHighScore(_name, _score, _phone, _email);
	}

	// Player Prefs information ---------------
	// dreamlo information: Name, Email, Number, Highest Score
	// Local information: Tilt
	// SH information: Collectables/Powerups, Visited Locations
	// SI information: Bases, Lives, Current Score, Collectables/Powerups -- for continuation

	// Delets all keys associated with the player.
	public void ResetPlayer()
	{
		PlayerPrefs.DeleteAll();
	}

	public bool FirstTime()
	{
		if(PlayerPrefs.HasKey("FirstPlayed")) {
			return false;
		}
		else {
			PlayerPrefs.SetInt("FirstPlayed", 1);
			return true;
		}
	}
}

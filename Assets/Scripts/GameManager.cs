﻿/*
 * Space Invaders Manager
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour 
{
	bool DEBUG = false;
	System.DateTime startTime;
	System.DateTime endTime;

	// Player Controller ----------------------
	public ARCameraScript arCamera;
	public SimplePlayerController simplePlayer;
	public GameObject bases;	
	public GameObject ground;
	public GameObject scoreText;		// Current score
	public GameObject myHighScore;		
	public GameObject topScore;		
	public GameObject myLives;

	// End conditions ----------------------
	public GameObject gameOver;
	public GameObject gameCompleted;
	private bool gameOverPlayed;
	public AudioSource gameOverSound;

	// Controls ----------------------
	public GameObject joystick;
	public GameObject fireButton;

	// Enemy Controller ----------------------
	Quaternion dronesRot;
	public GameObject redUFO;					// Prefab for spawning
	public GameObject currRedUFO;				// Current red UFO
	public GameObject drones;					// Container for all 21 drones 
	public GameObject dronePrefab;				// Prefab for spawning
	public GameObject dronesPrefab;				// Prefab for spawning
	public Vector3 dronesPos;
	private bool moveDronesDown;				// Flag for when the drones reached the bounds
	private bool increased;						// Flag for when the number of drones decrease
	public float droneMinBound, droneMaxBound;	// Drone bounds
	public float droneSpeed;					// Drone speed
	bool spawned = false;

	// Pause Menu --------------------
	public GameObject mainMenu;
	public GameObject instructions;
	public bool showingInstructions;
	public bool showingInventory;
	public bool paused;
	public int buttonPressed;

	public int numDrones;
	public Vector3[] dronePos;
	public Quaternion[] droneRot;

	// Use this for initialization
	void Start () 
	{
		/******** DEBUG ********/
		// simplePlayer.ResetPlayer();
		
		{
			// 03/14/2018 22:51:00
			// string start = "03/14/2018 22:55:00";
			// string end = "03/14/2018 22:56:00";

			// string iString = "2018-03-14 22:56 PM";
			// startTime = System.DateTime.ParseExact(iString, "yyyy-MM-dd HH:mm tt",null);

			// Debug.Log("TIME: " + System.DateTime.Now.ToString()); 
			// startTime.AddMonths(3);
			// startTime.AddDays(14);
			// startTime.AddYears(2018);
			// startTime.AddHours(22);
			// startTime.AddMinutes(52);

			// endTime.AddMonths(3);
			// endTime.AddDays(14);
			// endTime.AddYears(2018);
			// endTime.AddHours(22);
			// endTime.AddMinutes(53);

			// Debug.Log(startTime);
			// Debug.Log(endTime);
		}

		// drones.transform.rotation = arCamera.transform.rotation;
		// drones.transform.forward = -drones.transform.forward;

		dronesPos = drones.transform.position;
		numDrones = drones.transform.childCount;
		dronePos = new Vector3[numDrones];
		droneRot = new Quaternion[numDrones];
		for(int i = 0; i < numDrones; ++i) {
			// Debug.Log(drones.transform.GetChild(i).name);
			Vector3 pos = drones.transform.GetChild(i).transform.position;
			Quaternion rot = drones.transform.GetChild(i).transform.rotation;

			dronePos[i] = new Vector3(pos.x, pos.y, pos.z);
			droneRot[i] = new Quaternion(rot.x, rot.y, rot.z, rot.w);
		}

		dronesRot = drones.transform.rotation;
		droneMinBound = -18.0f;
		droneMaxBound = 18.0f;
		droneSpeed = 0.05f;

		Physics.gravity = new Vector3(0, -150.0f, 0);

		// If it's the player's first time playing, 
		// FirstPlayed in PlayerPrefs will be set and 
		// the instructions will be shown.
		// if(simplePlayer.FirstTime()) {
		// 	showingInstructions = true;
		// 	paused = true;
		// 	// StartCoroutine(ShowInstructions());
		// }
		// // Otherwise, start the game immediately.
		// else {
			showingInstructions = false;
			paused = false;
		// }

		increased = false;
		moveDronesDown = false;
		currRedUFO = Instantiate(redUFO, redUFO.transform.position + new Vector3(0,7,0), redUFO.transform.rotation);

		gameOverPlayed = false;
	}

	// Shows instructions for 5 seconds to new user
	IEnumerator ShowInstructions()
	{
		float t = 0;
		instructions.SetActive(true);
		while(t <= 5.0f) {
			// If the instructions are turned off,
			// break out of coroutine.
			if(!showingInstructions) {
				yield break;
			}

			t += Time.deltaTime;

			yield return null;
		}

		instructions.SetActive(false);
		showingInstructions = false;
		instructions.transform.Find("Close").gameObject.SetActive(true);
		paused = false;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		// Turn off proxy
		// simplePlayer.GetComponent<MeshRenderer>().enabled = false;
		
		// Only allow the game to run if the instructions 
		// and paused menus are off.
		if(!showingInstructions && !paused && !showingInventory) {
			RunGame();
		}
	}

	void RunGame()
	{
		// Shows Top Score, Current Score, My High Score
		DisplayScores();				
		// Keep strack of how many lives the player has and shows it on the bottom.
		UpdatePlayerLives();

		// Check to see if the game is over for the player
		if(!GameOver()) {
			// Keep controls showing
			EnableControls();
			
			// If the Red UFO was hit, instantiate another one and dereference it.
			if(currRedUFO.GetComponent<SimpleRedUFOController>().hit) {
				float moveTime = currRedUFO.GetComponent<SimpleRedUFOController>().moveTime;
				currRedUFO = Instantiate(redUFO, redUFO.transform.position + new Vector3(0,5,0), redUFO.transform.rotation);
				// Might have to tweak this a bit.
				currRedUFO.GetComponent<SimpleRedUFOController>().moveTime += moveTime;
			}

			{
				// Debug.Log(drones.transform.childCount);
				// if(drones.transform.childCount == 0 && !spawned) {
				// 	for(int i = 0; i < numDrones; ++i) {
				// 		GameObject d = Instantiate(dronePrefab, dronePos[i], droneRot[i]);
				// 		d.transform.parent = drones.transform;
				// 	}

				// 	spawned = true;
			// }
			}

			numDrones = drones.transform.childCount;
			if(numDrones == 0) {
				// Spawn More
				// if(!spawning) {
				// 	StartCoroutine(SpawnDrones());
				// }

				GameComplete();
			}
			else {
				if(!spawning) {
					MoveDrones();
				}
			}


{
			// If there are still drones, move them.
			// if(numDrones > 0) {
			// 	// if(spawned) {
			// 	// 	spawned = false;
			// 	// 	return;
			// 	// }
			// 	MoveDrones();
			// 	spawnMoreDrones = false;
			// }
			// // Check for level completion.
			// else {
			// 	if(numDrones == 0) {
			// 		GameComplete();
			// 		spawnMoreDrones = true;
			// 		Debug.Log("SPAWN DRONES");
			// 		numDrones = 1;
			// 		spawnMoreDrones = false;
			// 	}

			// 	if(!spawnMoreDrones) {
			// 		// spawnMoreDrones = true;
			// 	}

			// 	{
			// 		// PlayerPrefs.SetInt("CurrScore", simplePlayer.score);

			// 		// if(GameObject.Find("B1")) {
			// 		// 	PlayerPrefs.SetInt("Base1", GameObject.Find("B1").GetComponent<SimpleBaseController>().timesHit);
			// 		// }
			// 		// else {
			// 		// 	PlayerPrefs.SetInt("Base1", 100);					
			// 		// }

			// 		// if(GameObject.Find("B2")) {
			// 		// 	PlayerPrefs.SetInt("Base2", GameObject.Find("B2").GetComponent<SimpleBaseController>().timesHit);
			// 		// }
			// 		// else {
			// 		// 	PlayerPrefs.SetInt("Base2", 100);					
			// 		// }

			// 		// if(GameObject.Find("B3")) {
			// 		// 	PlayerPrefs.SetInt("Base3", GameObject.Find("B3").GetComponent<SimpleBaseController>().timesHit);
			// 		// }
			// 		// else {
			// 		// 	PlayerPrefs.SetInt("Base3", 100);					
			// 		// }

			// 		// if(GameObject.Find("B4")) {
			// 		// 	PlayerPrefs.SetInt("Base4", GameObject.Find("B4").GetComponent<SimpleBaseController>().timesHit);
			// 		// }
			// 		// else {
			// 		// 	PlayerPrefs.SetInt("Base4", 100);					
			// 		// }

				
			// 		// if(!spawned) {
			// 		// 	GameObject[] fallenDrones = GameObject.FindGameObjectsWithTag("Enemy");
			// 		// 	for(int i = 0; i < fallenDrones.Length; ++i) {
			// 		// 		Destroy(fallenDrones[i]);
			// 		// 	}
						
			// 		// 	// drones.transform.position = dronesPrefab.transform.position;
			// 		// 	// drones.transform.rotation = dronesPrefab.transform.rotation;
			// 		// 	GameObject spawnedDrones = Instantiate(dronesPrefab, dronesPrefab.transform.position, dronesPrefab.transform.rotation);
			// 		// 	spawnedDrones.SetActive(true);

			// 		// 	foreach(Transform sd in spawnedDrones.transform) {
			// 		// 		sd.parent = drones.transform;
			// 		// 	}

			// 		// 	// for(int i = 0; i < spawnedDrones.transform.childCount; i++) {
			// 		// 	// 	spawnedDrones.transform.GetChild(i).parent = drones.transform;
			// 		// 	// }


			// 		// 	spawned = true;
			// 		// }

			// 		// 	// drones.transform.position = dronesPrefab.transform.position;
			// 		// 	// drones.transform.rotation = dronesPrefab.transform.rotation;
			// 		// 	// GameObject spawns = Instantiate(dronesPrefab, drones.transform.position, drones.transform.rotation);
			// 		// 	// foreach(Transform s in spawns.transform) {
			// 		// 	// 	s.parent = drones.transform;
			// 		// 	// }

			// 		// 	// for(int i = 0; i < 3; i++) {
			// 		// 	// 	for(int j = 0; j < 7; j++) {
			// 		// 	// 		GameObject spawnedDrone = Instantiate(dronePrefab, dronePrefab.transform.position, dronePrefab.transform.rotation);
			// 		// 	// 		spawnedDrone.transform.parent = drones.transform;
			// 		// 	// 		spawnedDrone.transform.position = new Vector3(18f - (j * 6f), 8.3f - (i * 3f), 3.07f);
			// 		// 	// 	}
			// 		// 	// }


			// 		// 	// Reset
			// 		// 	droneSpeed = 0.05f;
			// 		// }
			// 	}
			// }
}
		}
		else {
			// Game over, hide the controls
			DisableControls();
		}
	}

	bool spawning = false;
	IEnumerator SpawnDrones()
	{
		Debug.Log("SPAWN DRONES");
		spawning = true;
		// Wait before jumping into moving the drones
		yield return new WaitForSeconds(2.0f);

		// Reset container position
		drones.transform.position = dronesPos;	
		
		// This spawns nonstop
		// Instantiate drones
		// for(int i = 0; i < 2; ++i) {
		// 	GameObject drone = Instantiate(dronePrefab, dronePos[i], droneRot[i]);
		// 	// Parent drones to container
		// 	drone.transform.parent = drones.transform;
		// }

		// This spawns half
		GameObject dronez = Instantiate(dronesPrefab, drones.transform.position, drones.transform.rotation);
		foreach(Transform d in dronez.transform) {
			d.parent = drones.transform;
		}

		Destroy(dronez);

		// Update numDrones
		numDrones = drones.transform.childCount;

		spawning = false;
	}

	private void DisplayScores()
	{
		scoreText.GetComponent<TextMeshProUGUI>().text 		= simplePlayer.score.ToString();	
		myHighScore.GetComponent<TextMeshProUGUI>().text 	= PlayerPrefs.GetInt("PlayerScore").ToString();		
		
		// if(PlayerPrefs.GetInt("TopScore") == 0) {
		// 	topScore.SetActive(false);
		// }
		// else {
		// 	topScore.SetActive(true);
			topScore.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("TopScore").ToString();
		// }

		UpdateScores();
	}

	// TODO
	// Need to keep track of the time that this was entered into the database.
	private void UpdateScores()
	{
		if(DEBUG) {
			System.DateTime currTime = System.DateTime.Now;

			// startTime.AddHours(22);
			// startTime.AddMinutes(51);

			// endTime.AddHours(22);
			// endTime.AddMinutes(52);

			// Debug.Log(startTime);
			// Debug.Log(endTime);

			// Too early
			if(currTime < startTime) {
				Debug.Log("Too early");
				return;
			}

			// Too late
			if(currTime > startTime) {
				Debug.Log("Too late");
				return;
			}
		}

		// Update scores on the server if the player beats their high score
		if(simplePlayer.score > PlayerPrefs.GetInt("PlayerScore", simplePlayer.score)) {
			simplePlayer.UpdateScores();
			myHighScore.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("PlayerScore").ToString();		
		}

		// Update scores on the server if the player beats the top score
		if(simplePlayer.score > PlayerPrefs.GetInt("TopScore")) {
			simplePlayer.UpdateScores();
			topScore.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("PlayerScore").ToString();		
		}
	}

	private void UpdatePlayerLives()
	{
		// Update lives
		myLives.GetComponent<Text>().text = simplePlayer.numLives.ToString();
	}

	private bool GameOver()
	{
		// If the player lost all their lives or lost all their bases then the game is over.
		if(simplePlayer.numLives <= 0 || bases.transform.childCount <= 0) {
			if(!gameOverPlayed) {
				gameOverSound.Play();
				gameOverPlayed = true;
			}
			
			gameOver.SetActive(true);
			mainMenu.SetActive(false);

			if(gameCompleted.activeSelf) {
				return false;
			}

			return true;
		}
		else {
			gameOver.SetActive(false);		
			mainMenu.SetActive(true);		// Pause Menu
			
			return false;
		}
	}

	bool spawnMoreDrones = false;
	private void GameComplete()
	{
		// Spawn more drones
		// if(spawnMoreDrones) {
		// 	for(int i = 0; i < numDrones; ++i) {
		// 		GameObject d = Instantiate(dronePrefab, dronePos[i], droneRot[i]);
		// 		d.transform.parent = drones.transform;
		// 	}
			
		// 	spawnMoreDrones = false;
		// }

		// if(drones.transform.childCount == 0 && !spawned) {
			// for(int i = 0; i < numDrones; ++i) {
			// 	GameObject d = Instantiate(dronePrefab, dronePos[i], droneRot[i]);
			// 	d.transform.parent = drones.transform;
			// }

		// 	spawned = true;
		// }

		gameCompleted.SetActive(true);
		mainMenu.SetActive(true);
	}

	private void DisableControls()
	{
		joystick.SetActive(false);
		fireButton.SetActive(false);
	}

	private void EnableControls()
	{
		joystick.SetActive(true);
		fireButton.SetActive(true);
	}

	private void IncreaseDroneSpeed()
	{
		int droneCount = drones.transform.childCount;
		if(droneCount <= 14 && droneCount > 7 && !increased) {
			droneSpeed *= 2.0f;
			increased = !increased;
		}
		else if(droneCount <= 7 && droneCount > 3 && increased) {
			droneSpeed *= 2.25f;
			increased = !increased;
		}
		else if(droneCount <= 3  && droneCount > 1 && !increased) {
			droneSpeed *= 2.5f;
			increased = !increased;
		}
		else if(droneCount == 1 && increased) {
			droneSpeed *= 3.0f;
			increased = !increased;
		}
	}

	private void MoveDrones()
	{
		// drones.transform.LookAt(Camera.main.transform.position);

		// Checks how many drones are left and increases speed as less are active
		IncreaseDroneSpeed();

		// Loop to actually move the drones left to right
		drones.transform.position += Vector3.right * droneSpeed;
		foreach(Transform drone in drones.transform) {
			if(drone.position.x < droneMinBound || drone.position.x > droneMaxBound) {
				droneSpeed = -droneSpeed;
				moveDronesDown = true;
				// drone.position -= new Vector3(0.0f, 2.0f, 0.0f);
				return;
			}
		}

		// If the drones reached the min/max bounds then the drones move down
		if(moveDronesDown) {
			foreach(Transform drone in drones.transform) {
				drone.position -= new Vector3(0.0f, 0.75f, 0.0f);			
			}

			moveDronesDown = false;
		}
	}

	void LateUpdate()
	{
		// Quaternion quat = arCamera.transform.rotation;
		// drones.transform.rotation = Quaternion.Euler(quat.eulerAngles.x, dronesRot.eulerAngles.y, quat.eulerAngles.z);
	}
	
	// Scene changes ------------------------
	// Controlled by Yes buttons in the Pause Menu
	public void LeaveSpace()
	{
		SceneManager.LoadScene(buttonPressed);

	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}

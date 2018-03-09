/*
 * Space Invaders Manager
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FPSController : MonoBehaviour 
{
	public SimplePlayerController simplePlayer;
	public GameObject scoreText;

	public GameObject myHighScore;
	public GameObject topScore;
	public GameObject myLives;
	public GameObject gameOver;
	public GameObject mainMenu;
	public GameObject joystick;
	public GameObject fireButton;
	public GameObject instructions;

	public GameObject drone;
	public GameObject currDrone;
	private bool moveDronesDown;
	public float droneMinBound, droneMaxBound;
	public float droneSpeed;

	public int level;
	private bool increased;
	private bool showingInstructions;

	// Use this for initialization
	void Start () 
	{
		level = 1;
		droneMinBound = -18.0f;
		droneMaxBound = 18.0f;
		droneSpeed = 0.05f;

		Physics.gravity = new Vector3(0, -150.0f, 0);

		// Debug
		// PlayerPrefs.SetInt("PlayerPlayed", 0);
		if(PlayerPrefs.GetInt("PlayerPlayed") == 1) {
			showingInstructions = false;		
		}
		else {
			// instructions.SetActive(true);
			showingInstructions = true;
			StartCoroutine(ShowInstructions());
			PlayerPrefs.SetInt("PlayerPlayed", 1);
		}

		increased = false;
		moveDronesDown = false;

		// Vector3 dronePos = new Vector3(Random.Range(-14, 15), Random.Range(-8, 9), 30);
		// currDrone = Instantiate(drone, dronePos, drone.transform.rotation);
		// currDrone.SetActive(true);
		// currDrone.GetComponent<SimpleEnemyController>().FPS = true;
	}

	IEnumerator ShowInstructions()
	{
		// If the user clicks out of the instructions,
		// don't need to wait all 5 seconds.
		float t = 0;
		instructions.SetActive(true);
		while(t <= 3.0f || !instructions.activeSelf) {
			t += Time.deltaTime;

			yield return null;
		}
		instructions.SetActive(false);
		showingInstructions = false;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(level == 1 && !showingInstructions) {
			RunLevelOne();
			return;
		}
	}

	float time = 0;
	float spawnDroneTime = Random.Range(2, 5);
	void RunLevelOne()
	{
		DisplayScores();				

		UpdatePlayerLives();

		if(!GameOver()) {
			if(time <= spawnDroneTime) {
				time += Time.deltaTime;
			}
			else {
				SpawnDrones();
				time = 0;
				spawnDroneTime = Random.Range(2, 5);
			}

			EnableControls();
		}
		else {
			DisableControls();
		}
	}

	private void DisplayScores()
	{
		scoreText.GetComponent<Text>().text 	= simplePlayer.score.ToString();		
		myHighScore.GetComponent<Text>().text 	= PlayerPrefs.GetInt("PlayerScore").ToString();		
		topScore.GetComponent<Text>().text 		= PlayerPrefs.GetInt("TopScore").ToString();

		UpdateScores();
	}

	private void UpdateScores()
	{
		// Update scores on the server if the player beats their high score
		if(simplePlayer.score > PlayerPrefs.GetInt("PlayerScore", simplePlayer.score)) {
			simplePlayer.UpdateScores();
			myHighScore.GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerScore").ToString();		
		}

		// Update scores on the server if the player beats the top score
		if(simplePlayer.score > PlayerPrefs.GetInt("TopScore")) {
			simplePlayer.UpdateScores();
			topScore.GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerScore").ToString();		
		}
	}

	private void UpdatePlayerLives()
	{
		// Update lives
		myLives.GetComponent<Text>().text = simplePlayer.numLives.ToString();
	}

	private bool GameOver()
	{
		if(simplePlayer.numLives <= 0) {
			gameOver.SetActive(true);
			mainMenu.SetActive(false);

			return true;
		}
		else {
			gameOver.SetActive(false);
			mainMenu.SetActive(true);
			
			return false;
		}
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

	private void SpawnDrones()
	{
		Vector3 dronePos = new Vector3(Random.Range(-14f, 15f), Random.Range(-8f, 9f), 30f);
		currDrone = Instantiate(drone, dronePos, drone.transform.rotation);
		currDrone.SetActive(true);
		currDrone.GetComponent<SimpleEnemyController>().FPS = true;	
	}

	// public void OpenInstructions()
	// {
	// 	instructions.SetActive(true);
	// 	showingInstructions = true;
	// }

	// public void CloseInstructions()
	// {
	// 	instructions.SetActive(false);
	// 	showingInstructions = false;
	// }

	// public void playScavengARHunt()
	// {
	// 	Application.LoadLevel(0);
	// }

	// public void playLevelTwo()
	// {
	// 	Application.LoadLevel(2);		
	// }

	// public void playTestLevel()
	// {
	// 	Application.LoadLevel(3);		
	// }

	// public void LoadMainMenu()
	// {
	// 	SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
	// }

	// public void RestartLevel()
	// {
	// 	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	// }
}

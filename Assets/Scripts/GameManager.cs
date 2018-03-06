/*
 * Space Invaders Manager
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour 
{
	public PlayerController player;
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

	public GameObject ground;
	public GameObject drones;
	public float droneMinBound, droneMaxBound;
	public float droneSpeed;

	public int level;

	// Use this for initialization
	void Start () 
	{
		level = 1;
		droneMinBound = -18.0f;
		droneMaxBound = 18.0f;
		droneSpeed = 0.25f;

		Physics.gravity = new Vector3(0, -150.0f, 0);

		instructions.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(level == 1) {
			RunLevelOne();
			return;
		}

		if(ground.GetComponent<MeshRenderer>().enabled) {
			ground.GetComponent<MeshRenderer>().enabled = false;
		}

		// Update score text
		// scoreText.GetComponent<Text>().text = "Drones Hit: " + player.score;		
	}

	void RunLevelOne()
	{
		scoreText.GetComponent<Text>().text = simplePlayer.score.ToString();		
		myHighScore.GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerScore").ToString();		
		topScore.GetComponent<Text>().text = PlayerPrefs.GetInt("TopScore").ToString();		

		if(simplePlayer.score > PlayerPrefs.GetInt("PlayerScore", simplePlayer.score)) {
			simplePlayer.UpdateScores();
			myHighScore.GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerScore").ToString();		
		}

		if(simplePlayer.score > PlayerPrefs.GetInt("TopScore")) {
			simplePlayer.UpdateScores();
			topScore.GetComponent<Text>().text = PlayerPrefs.GetInt("PlayerScore").ToString();		
		}

		myLives.GetComponent<Text>().text = simplePlayer.numLives.ToString();
		if(simplePlayer.numLives == 0) {
			gameOver.SetActive(true);
			mainMenu.SetActive(false);
			joystick.SetActive(false);
			fireButton.SetActive(false);
		}
		else {
			gameOver.SetActive(false);
			mainMenu.SetActive(true);
			joystick.SetActive(true);
			fireButton.SetActive(true);
		}

		drones.transform.position += Vector3.right * droneSpeed;
		foreach(Transform drone in drones.transform) {
			if(drone.position.x < droneMinBound || drone.position.x > droneMaxBound) {
				droneSpeed = -droneSpeed;
				return;
			}
		}
	}

	public void OpenInstructions()
	{
		instructions.SetActive(true);
	}

	public void CloseInstructions()
	{
		instructions.SetActive(false);
	}

	public void playScavengARHunt()
	{
		Application.LoadLevel(0);
	}

	public void playLevelTwo()
	{
		Application.LoadLevel(2);		
	}

	public void playTestLevel()
	{
		Application.LoadLevel(3);		
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

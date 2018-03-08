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

	public GameObject redUFO;
	public GameObject currRedUFO;
	public GameObject drones;
	private bool moveDronesDown;
	public float droneMinBound, droneMaxBound;
	public float droneSpeed;

	public GameObject bases;
	public GameObject ground;

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
		currRedUFO = Instantiate(redUFO, redUFO.transform.position, redUFO.transform.rotation);
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

		if(ground.GetComponent<MeshRenderer>().enabled) {
			ground.GetComponent<MeshRenderer>().enabled = false;
		}

		// Update score text
		// scoreText.GetComponent<Text>().text = "Drones Hit: " + player.score;		
	}

	void RunLevelOne()
	{
		DisplayScores();				

		UpdatePlayerLives();

		if(!GameOver()) {
			if(currRedUFO.GetComponent<SimpleRedUFOController>().hit) {
				float moveTime = currRedUFO.GetComponent<SimpleRedUFOController>().moveTime;
				currRedUFO = Instantiate(redUFO, redUFO.transform.position, redUFO.transform.rotation);
				// Might have to tweak this a bit.
				currRedUFO.GetComponent<SimpleRedUFOController>().moveTime += moveTime;
			}

			MoveDrones();
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
		if(simplePlayer.numLives <= 0 || bases.transform.childCount <= 0) {
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

	private void MoveDrones()
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
		
		drones.transform.position += Vector3.right * droneSpeed;
		foreach(Transform drone in drones.transform) {
			if(drone.position.x < droneMinBound || drone.position.x > droneMaxBound) {
				droneSpeed = -droneSpeed;
				moveDronesDown = true;
				// drone.position -= new Vector3(0.0f, 2.0f, 0.0f);
				return;
			}
		}

		if(moveDronesDown) {
			foreach(Transform drone in drones.transform) {
				drone.position -= new Vector3(0.0f, 0.75f, 0.0f);			
			}

			moveDronesDown = false;
		}
	}

	public void OpenInstructions()
	{
		instructions.SetActive(true);
		showingInstructions = true;
	}

	public void CloseInstructions()
	{
		instructions.SetActive(false);
		showingInstructions = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Registration : MonoBehaviour 
{
	public TMP_InputField nameField;
	public TMP_InputField phoneField;
	public TMP_InputField emailField;

	private string username;
	private string phone;
	private string email;

	public GameObject errorMessage;

	public PlayerScore[] scores;
	const string privateCode 	= "toXkBrVLoU-NIPomJOiC4QLbv3lvZzuEmvE6ogLTX5vA";
	const string publicCode 	= "5a9c91ff012b300a70a814b4";
	const string webURL 		= "http://dreamlo.com/lb/";

	void Start()
	{
		errorMessage.SetActive(false);

		RetrieveScores();
	}

	public void Register()
	{
		// emailField gets checked by TMP if it's a valid address
		if(nameField.text == "" || phoneField.text == "" || emailField.text == "") {
			Debug.Log("You must fill out the form");
			
			errorMessage.SetActive(true);
		}
		else {
			username 	= nameField.text;
			phone 		= phoneField.text;
			email 		= emailField.text;
			
			Submit();
		}
	}

	public void Submit()
	{
		// Check to see if user is already registered
		if(!PlayerRegistered()) {
			StartCoroutine(RegisterUser());
		}
		else {
			SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
		}
	}

	bool PlayerRegistered()
	{
		for(int i = 0; i < scores.Length; ++i) {
			if(scores[i].email == email) {
				// If the player is already registered,
				// load in their stats and continue the game.

				PlayerPrefs.SetString("PlayerName", scores[i].username);
				PlayerPrefs.SetString("PlayerEmail", scores[i].email);
				PlayerPrefs.SetString("PlayerPhone", scores[i].phone.ToString());
				PlayerPrefs.SetInt("PlayerScore", scores[i].score);
				// PlayerPrefs.SetInt("Tilt", 0);
				// PlayerPrefs.SetInt("Gamepad", 1);

				// This is an issue. I have no way of keeping track of their stats.
				// PlayerPrefs.SetInt("L2 Shield", 0);			// ENIAC - Shield
				// PlayerPrefs.SetInt("L3 Shield", 0);			// AddLab - Bar
				// PlayerPrefs.SetInt("ExtraLife", 0);			// GRASP - Franklin Fish
				// PlayerPrefs.SetInt("Book", 0);				// SigLab - Book
				// PlayerPrefs.SetInt("BulletSpread", 0);		// SigLab - Penn Dots

				return true;
			}
		}

		return false;
	}

	public void RetrieveScores()
	{
		StartCoroutine(GetScoresFromServer());
	}

	IEnumerator RegisterUser()
	{
		// Use the player's email as the "key" because dreamlo overwrites the scores based on that
		WWW dbLink = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(email) + "/" + -999999 + "/" + phone + "/" + WWW.EscapeURL(username));

		// Wait till finished uploading
		yield return dbLink;

		if(string.IsNullOrEmpty(dbLink.error)) {
			Debug.Log("Player registered.");

			// PlayerPrefs.SetInt("Registered", 1);
			PlayerPrefs.SetString("PlayerName", username);
			PlayerPrefs.SetString("PlayerEmail", email);
			PlayerPrefs.SetString("PlayerPhone", phone);
			PlayerPrefs.SetInt("PlayerScore", -999999);

			PlayerPrefs.SetInt("L3", 0);				// L3
			PlayerPrefs.SetInt("WeaponSpread", 0);		// Weapon Spread
			PlayerPrefs.SetInt("PowerUpShield", 0);		// ENIAC 	- Shield		- L2
			PlayerPrefs.SetInt("PowerUpBar", 0);		// AddLab 	- Bar			- L3 (if completed)
			PlayerPrefs.SetInt("PowerUpFish", 0);		// GRASP 	- Franklin Fish	- Life
			PlayerPrefs.SetInt("PowerUpBook", 0);		// SigLab 	- Book			- 
			PlayerPrefs.SetInt("PowerUpPenn", 0);		// SigLab 	- Penn Dots		- 
			
			PlayerPrefs.SetInt("Gamepad", 1);		
			
			// Power ups
			// PlayerPrefs.SetInt("PlayerItems", 0);

			// Start game
			SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
		}
		else {
			Debug.Log("Error: " + dbLink.error);
		}
	}

	IEnumerator GetScoresFromServer()
	{
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		
		// Wait till finished uploading
		yield return www;

		if(string.IsNullOrEmpty(www.error)) {
			// print(www.text);
			FormatScores(www.text);
		}
		else {
			Debug.Log("Error: " + www.error);
		}
	}

	void FormatScores(string textScores)
	{
		string[] entries = textScores.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		scores = new PlayerScore[entries.Length];

		for(int i = 0; i < entries.Length; ++i) {
			string[] playerInfo = entries[i].Split(new char[]{'|'});
			
			string email 	= playerInfo[0];
			int score 		= int.Parse(playerInfo[1]);
			string phone 	= playerInfo[2];
			string username = playerInfo[3];

			// New user
			if(score == -999999) {
				continue;
			}

			scores[i] = new PlayerScore(username, score, phone, email);
		}
	}

	public void CloseErrorMessage()
	{
		errorMessage.SetActive(false);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
	}
}

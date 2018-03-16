/*
 * This script is used to maintain the state of the game.
 * It should have references to Space InvadARs and the ScavengAR Hunt.
 */

using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public enum LEVELS
{
	MAIN_MENU		= 0,
	SCAVENGER_HUNT 	= 1,
	SPACE_INVADERS 	= 2,
	HIGH_SCORES 	= 3,
	REGISTRATION	= 4,
}


public class MainGameManager : MonoBehaviour 
{
	const int EST 			= 4;
	const int releaseMonth 	= 3;
	const int releaseDay 	= 20;
	const int releaseYear 	= 2018;
	const int releaseHour 	= 18;
	const int releaseMinute = 00;
	const int endHour		= 20;
	const int endMinute		= 15;

	int month 	= DateTime.Now.Month;
	int day 	= DateTime.Now.Day;
	int year 	= DateTime.Now.Year;
	int hour	= Int32.Parse(DateTime.UtcNow.Hour.ToString()) - EST;
	int minute	= DateTime.UtcNow.Minute;

	public GameObject welcome;

	// Menu Buttons
	public GameObject androidMenu;
	public GameObject iosMenu;
	public GameObject playMenu;
	public GameObject play;
	public GameObject explore;
	public GameObject about;
	public GameObject quit;
	public GameObject highScores;
	public GameObject continueMenu;
	public GameObject continueAs;
	public GameObject hunt;

	void Awake()
	{
		ChangeMenus();
	}

	private void ChangeMenus()
	{
		if(Application.platform == RuntimePlatform.Android) {
			playMenu 	= androidMenu;
			quit 		= playMenu.transform.Find("Quit").gameObject;
		}
		else if(Application.platform == RuntimePlatform.IPhonePlayer) {
			playMenu = iosMenu;
		}
		// Debug
		else {
			playMenu = iosMenu;
			// playMenu 	= androidMenu;
			// quit 		= playMenu.transform.Find("Quit").gameObject;
		}

		play 		= playMenu.transform.Find("Play Button").gameObject;
		explore 	= playMenu.transform.Find("Hunt").gameObject;
		highScores 	= playMenu.transform.Find("High Scores Button").gameObject;
		about	 	= playMenu.transform.Find("About").gameObject;

		EnableScavengAR();
	}

	// Use this for initialization
	void Start () 
	{
		{
			// Debug -- Run this to test loading screen
			// PlayerPrefs.DeleteAll();

			// Only need to run the player prefs at main menu.
			// if(SceneManager.GetActiveScene().buildIndex != (int)LEVELS.MAIN_MENU) {
			// 	return;
			// }

			// Get top score
			// GetComponent<HighScores>().RetrieveScores();

			// if(PlayerPrefs.GetInt("FirstRun") != 1) {
			// 	ShowMainMenuElements();
			// }
			// else {
			// 	PlayerPrefs.SetInt("FirstRun", 1);
			// 	ToggleContinue(true);
			// }
		}

		/******** DEBUG *********/
		// PlayerPrefs.DeleteAll();

		if(PlayerPrefs.HasKey("PlayerEmail")) {
			ToggleContinue(true);
		}
		else {
			// playMenu.SetActive(true);
			// continueMenu.SetActive(false);
			ShowMainMenuElements();
		}
	}

	private void EnableScavengAR()
	{
		if(	month != releaseMonth || day != releaseDay || year != releaseYear ||
			hour != releaseHour || minute != releaseMinute) {
			explore.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Hunt Begins\nMarch 20, 2018!";
			explore.transform.Find("Text").GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
			explore.GetComponent<Button>().interactable = false;
		}
	}

	void ToggleContinue(bool status)
	{
		if(status)
		{
			continueMenu.SetActive(status);
			playMenu.SetActive(!status);
			welcome.GetComponent<TMP_Text>().text = "Continue as " + PlayerPrefs.GetString("PlayerName") + "?";			
		}
		else {
			ShowMainMenuElements();
		}
	}

	public void ShowMainMenuElements()
	{
		playMenu.SetActive(true);
		continueMenu.SetActive(false);
	}

	public void LoadRegistration()
	{
		SceneManager.LoadScene((int)LEVELS.REGISTRATION);
	}

	public void LoadScavengerHunt()
	{
		if(!PlayerPrefs.HasKey("PlayerEmail")) {
			LoadRegistration();
		}
		else {
			SceneManager.LoadScene((int)LEVELS.SCAVENGER_HUNT);
		}

	}

	public void LoadSpaceInvaders()
	{
		if(!PlayerPrefs.HasKey("PlayerEmail")) {

			LoadRegistration();
		}
		else {
			SceneManager.LoadScene((int)LEVELS.SPACE_INVADERS);
		}
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
	}

	public void LoadHighScores()
	{
		SceneManager.LoadScene((int)LEVELS.HIGH_SCORES);
	}

	public void PennTeachIn()
	{
		Application.OpenURL("http://www.upenn.edu/teachin/");
	}

	public GameObject warning;
	public void QuitWarning()
	{	
		warning.SetActive(true);
		continueMenu.SetActive(false);
		playMenu.SetActive(false);
	}

	public void CloseWarning()
	{
		warning.SetActive(false);
		playMenu.SetActive(true);
	}

	public void Quit()
	{
		Application.Quit();
	}
}

public struct PlayerScore 
{
	public string username;
	public int score;
	public string phone;
	public string email;

	public PlayerScore(string _username, int _score, string _phone, string _email)
	{
		username = _username;
		score = _score;
		phone = _phone;
		email = _email;
	}
}

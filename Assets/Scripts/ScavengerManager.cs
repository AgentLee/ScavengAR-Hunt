/*
 * This script handles the GUI functionality of the Scavenger Hunt scenes.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Vuforia;

public class ScavengerManager : MonoBehaviour 
{
	public GameObject warning;
	public int buttonPressed;
	public GameObject pauseMenu;
	public GameObject instructions;
	public GameObject instructionsPanel;
	public GameObject pauseButtons;
	public bool paused = false;
	public GameObject pauseIcon;

	public GameObject inventory;

	void Start()
	{
		// DeletePowerUps();
	}

	/******* DEBUG *******/
	private void DeletePowerUps()
	{
		PlayerPrefs.DeleteKey("PowerUpShield");
		PlayerPrefs.DeleteKey("PowerUpBar");
		PlayerPrefs.DeleteKey("PowerUpBook");
		PlayerPrefs.DeleteKey("PowerUpFish");
		PlayerPrefs.DeleteKey("PowerUpPenn");
	}

	public void ShowInventory()
	{
		inventory.SetActive(true);
		Pause();

		// Shield
		if(PlayerPrefs.GetInt("PowerUpShield") == 1) {
			inventory.transform.Find("Shield").Find("Image").gameObject.SetActive(true);
			inventory.transform.Find("Shield").Find("Text").gameObject.SetActive(true);
		}

		// Bar
		if(PlayerPrefs.GetInt("PowerUpBar") == 1) {
			inventory.transform.Find("Bar").Find("Image").gameObject.SetActive(true);			
			inventory.transform.Find("Bar").Find("Text").gameObject.SetActive(true);			
		}

		// Books
		if(PlayerPrefs.GetInt("PowerUpBook") == 1) {
			inventory.transform.Find("Book").Find("Image").gameObject.SetActive(true);			
			inventory.transform.Find("Book").Find("Text").gameObject.SetActive(true);			
		}

		// Fish
		if(PlayerPrefs.GetInt("PowerUpFish") == 1) {
			inventory.transform.Find("Fish").Find("Image").gameObject.SetActive(true);			
			inventory.transform.Find("Fish").Find("Text").gameObject.SetActive(true);			
		}

		// Penn
		if(PlayerPrefs.GetInt("PowerUpPenn") == 1) {
			// inventory.transform.Find("Button").GetComponent<Button>().interactable = true;	
			// inventory.transform.Find("Button").GetComponent<Button>().GetComponent<UnityEngine.UI.Image>().color = Color.green;	
			// inventory.transform.Find("Button").Find("Penn").Find("Image").gameObject.SetActive(true);			
			// inventory.transform.Find("Button").Find("Penn").Find("Text").gameObject.SetActive(true);			
			// inventory.transform.Find("Button").GetComponent<Button>().interactable = true;	
			// inventory.transform.Find("Button").GetComponent<Button>().GetComponent<UnityEngine.UI.Image>().color = Color.green;	
			inventory.transform.Find("Penn").Find("Image").gameObject.SetActive(true);			
			inventory.transform.Find("Penn").Find("Text").gameObject.SetActive(true);			
		}
	}

	public void CloseInventory()
	{
		inventory.SetActive(false);
		Pause();
	}

	public void WeaponSpread()
	{
		switch(PlayerPrefs.GetInt("WeaponSpread")) 
		{
			// Turn on
			case 0:
				PlayerPrefs.SetInt("WeaponSpread", 1);		
				inventory.transform.Find("Button").GetComponent<Button>().GetComponent<UnityEngine.UI.Image>().color = Color.green;	
				break;
			// Turn off
			case 1:
				PlayerPrefs.SetInt("WeaponSpread", 0);
				inventory.transform.Find("Button").GetComponent<Button>().GetComponent<UnityEngine.UI.Image>().color = Color.red;	
				break;		
		}
	}

	public void ToSpace()
	{
		buttonPressed = (int)LEVELS.SPACE_INVADERS;
	}

	public void ToMenu()
	{
		buttonPressed = (int)LEVELS.MAIN_MENU;
	}

	// ---------------------------------
	// Warning Menu --------------------
	// ---------------------------------
	
	public void LeaveScavengeWarning()
	{
		warning.SetActive(true);
		pauseButtons.SetActive(false);
	}


	// User selects no, warning closes
	public void CloseWarning()
	{
		warning.SetActive(false);
		pauseButtons.SetActive(true);
	}

	// User selects yes, takes them to the scene that they had pressed earlier.
	public void LeaveScavenge()
	{
		SceneManager.LoadScene(buttonPressed);
	}
	
	// User presses the pause button
	public void Pause()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);		
	}

	public void Resume()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);		
	}

	public void OpenInstructions()
	{
		instructionsPanel.SetActive(true);
		// pauseButtons.SetActive(false);
		pauseMenu.SetActive(!pauseMenu.activeSelf);
	}

	public void CloseInstructions()
	{
		instructionsPanel.SetActive(false);
		// pauseButtons.SetActive(true);
		pauseMenu.SetActive(!pauseMenu.activeSelf);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
	}
}

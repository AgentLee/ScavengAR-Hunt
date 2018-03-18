/*
 * Script is to handle all menu operations.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{
	GameManager manager;

	// Pause Menu --------------------
	public GameObject pauseIcon;
	public GameObject pauseMenu;
	public GameObject inventory;
	public GameObject instructions;
	public GameObject pauseButtons;
	private bool _paused, _showingInstructions;

	// Toggle Gamepad --------------------
	public Button toggleGamepad;
	public GameObject gamepad;
	bool normalSetting = true;

	// Toggle Tilt --------------------
	bool tiltEnable = false;
	public Button toggleTilt;
	public Color controlEnabledColor;
	public Color controlDisabledColor;

	// Use this for initialization
	void Start () 
	{
		manager = GetComponent<GameManager>();

		// if(SystemInfo.supportsAccelerometer) {
		// 	toggleTilt.GetComponent<Image>().color = controlDisabledColor;
		// }
		// else {
		// 	toggleTilt.gameObject.SetActive(false);
		// }

		tiltEnable = (PlayerPrefs.GetInt("Tilt") == 1) ? true : false;
		normalSetting = (PlayerPrefs.GetInt("Gamepad") == 1) ? true : false;
		// if(PlayerPrefs.GetInt("Tilt") == 1) {
		// 	Debug.Log("TILT");
		// 	tiltEnable = true;
		// 	toggleTilt.GetComponent<Image>().color = controlEnabledColor;
		// 	toggleGamepad.GetComponent<Image>().color = controlDisabledColor;
		// }
		// else {
		// 	Debug.Log("NO TILT");
		// 	toggleTilt.GetComponent<Image>().color = controlDisabledColor;
		// 	toggleGamepad.GetComponent<Image>().color = controlEnabledColor;
		// }
	}

	public void ShowInventory()
	{
		inventory.SetActive(true);
		manager.showingInventory = true;
		pauseIcon.SetActive(false);
		pauseButtons.SetActive(false);

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
			inventory.transform.Find("Button").GetComponent<Button>().interactable = true;		
			inventory.transform.Find("Button").Find("Penn").Find("Image").gameObject.SetActive(true);			
			inventory.transform.Find("Button").Find("Penn").Find("Text").gameObject.SetActive(true);			
		}
	}

	public void WeaponSpread()
	{
		switch(PlayerPrefs.GetInt("WeaponSpread")) 
		{
			// Turn on
			case 0:
				PlayerPrefs.SetInt("WeaponSpread", 1);		
				break;
			// Turn off
			case 1:
				PlayerPrefs.SetInt("WeaponSpread", 0);
				break;		
		}
	}

	public void CloseInventory()
	{
		inventory.SetActive(false);
		pauseIcon.SetActive(true);
		manager.showingInventory = false;
		pauseButtons.SetActive(true);
	}

	public GameObject warning;
	public void Pause()
	{
		manager.paused = !manager.paused;
		pauseMenu.SetActive(manager.paused);		
	}

	public void Resume()
	{
		manager.paused = false;
		pauseMenu.SetActive(false);
	}

	public void LeaveSpaceWarning()
	{
		warning.SetActive(true);
		pauseButtons.SetActive(false);
	}

	public void CloseWarning()
	{
		warning.SetActive(false);
		pauseButtons.SetActive(true);
	}

	public void ToMenu()
	{
		manager.buttonPressed = (int)LEVELS.MAIN_MENU;
		Debug.Log(manager.buttonPressed);
	}

	public void ToExplore()
	{
		manager.buttonPressed = (int)LEVELS.SCAVENGER_HUNT;
		Debug.Log(manager.buttonPressed);
	}

	public void OpenInstructions()
	{
		instructions.SetActive(true);
		manager.showingInstructions = true;
		pauseIcon.SetActive(false);
		pauseButtons.SetActive(false);
	}

	public void CloseInstructions()
	{
		instructions.SetActive(false);
		manager.showingInstructions = false;
		pauseIcon.SetActive(true);
		pauseButtons.SetActive(true);
	}

	public void ToggleGamepad()
	{
		Vector3 lhs = gamepad.transform.Find("LHS").position;
		Vector3 rhs = gamepad.transform.Find("RHS").position;

		if(normalSetting) {
			PlayerPrefs.SetInt("Gamepad", 1);
			manager.fireButton.transform.position = lhs;
			manager.joystick.transform.position = rhs;
		}
		else {
			PlayerPrefs.SetInt("Gamepad", 0);
			manager.fireButton.transform.position = rhs;
			manager.joystick.transform.position = lhs;
		}

		normalSetting = !normalSetting;
	}

	public void ToggleTilt()
	{
		if(PlayerPrefs.GetInt("Tilt") == 0) {
			PlayerPrefs.SetInt("Tilt", 1);
			toggleTilt.GetComponent<Image>().color = controlEnabledColor;
			toggleGamepad.GetComponent<Image>().color = controlDisabledColor;
		}
		else {
			PlayerPrefs.SetInt("Tilt", 0);
			toggleTilt.GetComponent<Image>().color = controlDisabledColor;
			toggleGamepad.GetComponent<Image>().color = controlEnabledColor;
		}

		tiltEnable = !tiltEnable;
	}
}

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

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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
		pauseButtons.SetActive(false);
	}

	public void CloseInstructions()
	{
		instructionsPanel.SetActive(false);
		pauseButtons.SetActive(true);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
	}
}

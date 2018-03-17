using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FutureFinale : MonoBehaviour 
{
	public GameObject sophia;
	public GameObject future;
	public GameObject puzzleDesc;

	// Sophia to Future page
	public void NextPage()
	{	
		// Check for all first
		// Give player Level 3 shields
		PlayerPrefs.SetInt("PowerUpBar", 1);

		if(	PlayerPrefs.GetInt("PowerUpShield") == 1 &&
			PlayerPrefs.GetInt("PowerUpBar") == 1 &&
			PlayerPrefs.GetInt("PowerUpBook") == 1 &&
			PlayerPrefs.GetInt("PowerUpFish") == 1 &&
			PlayerPrefs.GetInt("PowerUpPenn") == 1) 
		{
			puzzleDesc.GetComponent<TextMeshProUGUI>().text += "\nYou now have Level 3 shields!";
		}
		else {
			puzzleDesc.SetActive(false);			
		}


		// PlayerPrefs.SetInt("PowerUpShield", 0);		// ENIAC 	- Shield		- L2
		// PlayerPrefs.SetInt("PowerUpBar", 0);		// AddLab 	- Bar			- L3 (if completed)
		// PlayerPrefs.SetInt("PowerUpFish", 0);		// GRASP 	- Franklin Fish	- Life
		// PlayerPrefs.SetInt("PowerUpBook", 0);		// SigLab 	- Book			- 
		// PlayerPrefs.SetInt("PowerUpPenn", 0);		// SigLab 	- Penn Dots		- 
		
		sophia.SetActive(false);
		future.SetActive(true);
	}

	// Future page to Thank You
	public void ThankYou()
	{
		future.transform.Find("Page1").gameObject.SetActive(false);
		future.transform.Find("Page2").gameObject.SetActive(true);
	}

	public void PennTeachIn()
	{
		Application.OpenURL("http://pennteachin.org");
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaming : MonoBehaviour 
{
	public GameObject intro;
	public GameObject deepBlue;
	public GameObject watson;
	public GameObject alphaGo;
	public GameObject nextClue;
	public GameObject book;

	int currPage;
	bool fading;
	
	// Use this for initialization
	void Start () 
	{
		intro 		= transform.Find("Intro").gameObject;	
		deepBlue	= transform.Find("Deep Blue").gameObject;
		watson		= transform.Find("Watson").gameObject;
		alphaGo		= transform.Find("AlphaGo").gameObject;
		nextClue	= transform.Find("Next Clue").gameObject;

		currPage = 0;
		fading = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Continue()
	{
		GameObject fadeOut, fadeIn;
		switch(currPage) 
		{
			case 0:
				fadeOut = intro;
				fadeIn = deepBlue;
				
				// intro.SetActive(false);
				// deepBlue.SetActive(true);
				++currPage;
				break;
			case 1:
				fadeOut = deepBlue;
				fadeIn = watson;
				
				// deepBlue.SetActive(false);
				// watson.SetActive(true);
				++currPage;
				break;
			case 2:
				fadeOut = watson;
				fadeIn = alphaGo;
				
				// watson.SetActive(false);
				// alphaGo.SetActive(true);
				++currPage;
				break;
			case 3:
				fadeOut = alphaGo;
				fadeIn = nextClue;

				// alphaGo.SetActive(false);
				// nextClue.SetActive(true);
				++currPage;
				break;
			default:
				fadeOut = null;
				fadeIn = null;
				break;
		}

		fadeOut.SetActive(false);
		fadeIn.SetActive(true);

		// if(!fading) {
		// 	StartCoroutine(Fade(fadeOut.GetComponent<CanvasGroup>(), fadeIn, fadeOut.GetComponent<CanvasGroup>().alpha, 0.0f));
		// }
		// else {
		// 	fading = false;
		// }
	}

	public void WatsonToAlpha()
	{
		watson.SetActive(false);
		alphaGo.SetActive(true);
	}
	
	public void AlphaToClue()
	{
		// Give player spread
		PlayerPrefs.SetInt("PowerUpPenn", 1);
		
		// PlayerPrefs.SetInt("PowerUpShield", 0);		// ENIAC 	- Shield		- L2
		// PlayerPrefs.SetInt("PowerUpBar", 0);			// AddLab 	- Bar			- L3 (if completed)
		// PlayerPrefs.SetInt("PowerUpFish", 0);		// GRASP 	- Franklin Fish	- Life
		// PlayerPrefs.SetInt("PowerUpBook", 0);		// SigLab 	- Book			- 
		// PlayerPrefs.SetInt("PowerUpPenn", 0);		// SigLab 	- Penn Dots		- 

		alphaGo.SetActive(false);
		nextClue.SetActive(true);
	}
	
	public void GetBook()
	{
		deepBlue.transform.Find("Book").gameObject.SetActive(true);
		deepBlue.transform.Find("Panel1").gameObject.SetActive(false);

		PlayerPrefs.SetInt("PowerUpBook", 1);		// SigLab 	- Book			-
	}

	// Fade In: start = 0.01, end = 1.0
	// Fade Out: start = 1.0, end = 0.0
	IEnumerator Fade(CanvasGroup group, GameObject next, float start, float end, float timeLerp = 0.5f)
	{
		group.gameObject.SetActive(true);
		
		float timeStartedLerp = Time.time;
		float timeSinceStart = Time.time - timeStartedLerp;
		float progress = timeSinceStart / timeLerp;

		while(true) {
			timeSinceStart = Time.time - timeStartedLerp;
			progress = timeSinceStart / timeLerp;

			float currVal = Mathf.Lerp(start, end, progress);

			group.alpha = currVal;

			if(progress >= 1) {
				break;
			}

			yield return new WaitForSeconds(0.05f);
		}


		if(next != null) {
			StartCoroutine(Fade(next.GetComponent<CanvasGroup>(), null, 0.01f, 1.0f));
		}

		// group.gameObject.SetActive(false);
		fading = true;
	}
}

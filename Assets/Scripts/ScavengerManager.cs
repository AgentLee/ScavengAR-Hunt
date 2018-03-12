using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

enum LOCATIONS
{
	START = 0,
	ENIAC = 1,
	GRASP = 2,
}

enum ANSWERS
{
	A = 0,
	B = 1,
	C = 2,
	D = 3
}

public class ScavengerManager : MonoBehaviour 
{
	public bool rothPowerup;
	private int location;

	private int currAnswer;

	// Start
	public GameObject starter;

	// Use this for initialization
	void Start () 
	{
		// Player just started
		if(!PlayerPrefs.HasKey("Location")) {
			PlayerPrefs.SetInt("Location", (int)LOCATIONS.START);
			location = (int)LOCATIONS.START;
		}
		else {
			location = PlayerPrefs.GetInt("Location");
		}

		currAnswer = -1;

		rothPowerup = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		LoadEvent(); 

		// if(rothPowerup) {
		// 	Debug.Log("COLLECTED ITEM");
		// }
	}

	private void LoadEvent()
	{
		switch(PlayerPrefs.GetInt("Location")) 
		{
			case (int)LOCATIONS.START:
				StartCoroutine(ShowStarter());
				break;
			case (int)LOCATIONS.ENIAC:
				ShowENIAC();
				break;
			default:
				break;
		}
	}

	IEnumerator WaitToShow(GameObject obj)
	{
		yield return new WaitForSeconds(3.0f);
	}

	IEnumerator ShowStarter()
	{
		int realAnswer = (int)ANSWERS.B;
		starter.SetActive(true);

		GameObject intro 		= starter.transform.Find("Intro").gameObject;
		GameObject nextClue		= starter.transform.Find("Next Clue").gameObject;
		GameObject title 		= intro.transform.Find("Title").gameObject;  
		GameObject description 	= intro.transform.Find("Description").gameObject;  
		GameObject question 	= intro.transform.Find("Question").gameObject;  
		GameObject answers 		= intro.transform.Find("Answers").gameObject;

		yield return new WaitForSeconds(3.0f);

		description.SetActive(true);

		if(description.GetComponent<TextReveal>().completed) {
			// TODO
			// Fix pause
			// StartCoroutine(DramaticPause(question, 2.0f));
			question.SetActive(true);
		}
	
		if(question.GetComponent<TextReveal>().completed) {
			// StartCoroutine(DramaticPause(answers, 1.5f));
			answers.SetActive(true);

			// TODO
			// Figure out fade
			// GameObject A = answers.transform.Find("A").gameObject;
			// GameObject B = answers.transform.Find("B").gameObject;
			// GameObject C = answers.transform.Find("C").gameObject;

			// A.GetComponent<FadeEffect>().StartFadeIn();
			// B.GetComponent<FadeEffect>().StartFadeIn();
			// C.GetComponent<FadeEffect>().StartFadeIn();
		}

		switch(currAnswer) 
		{
			// Player didn't pick anything yet.
			case -1:
				break;
			case (int)ANSWERS.A:
				GameObject A = answers.transform.Find("A").gameObject;
				TextMeshProUGUI choiceA = A.transform.Find("Text").GetComponent<TextMeshProUGUI>();
				choiceA.fontStyle = FontStyles.Strikethrough;
				
				// answers.transform.Find("A").gameObject.SetActive(false);
				break;
			case (int)ANSWERS.B:
				answers.transform.Find("B").gameObject.GetComponent<Image>().color = new Color(0, 1, 0, 1);		

				// StartCoroutine(FadeIn(intro.GetComponent<CanvasGroup>(), 1, 0, 3.0f));
				StartCoroutine(FadeOut(intro.GetComponent<CanvasGroup>(), nextClue, intro.GetComponent<CanvasGroup>().alpha, 0));

				PlayerPrefs.SetInt("Location", PlayerPrefs.GetInt("Location") + 1);
				
				break;
			case (int)ANSWERS.C:
				GameObject C = answers.transform.Find("C").gameObject;
				TextMeshProUGUI choiceC = C.transform.Find("Text").GetComponent<TextMeshProUGUI>();
				choiceC.fontStyle = FontStyles.Strikethrough;

				// answers.transform.Find("C").gameObject.SetActive(false);
				break;
			case (int)ANSWERS.D:
				break;
			default:
				break;
		}
	}

	// http://unity.grogansoft.com/fade-your-ui-in-and-out/
	IEnumerator FadeIn(CanvasGroup canvas, float startAlpha, float endAlpha, float duration)
    {
         // keep track of when the fading started, when it should finish, and how long it has been running&lt;/p&gt; &lt;p&gt;&a
         var startTime = Time.time;
         var endTime = Time.time + duration;
         var elapsedTime = 0f;
 
         // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
         canvas.alpha = startAlpha;
         // loop repeatedly until the previously calculated end time
         while (Time.time <= endTime)
         {
             elapsedTime = Time.time - startTime; // update the elapsed time
             var percentage = 1/(duration/elapsedTime); // calculate how far along the timeline we are
             if (startAlpha > endAlpha) // if we are fading out/down 
             {
                  canvas.alpha = startAlpha - percentage; // calculate the new alpha
             }
             else // if we are fading in/up
             {
                 canvas.alpha = startAlpha + percentage; // calculate the new alpha
             }
 
             yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }
        canvas.alpha = endAlpha; // force the alpha to the end alpha before finishing – this is here to mitigate any rounding errors, e.g. leaving the alpha at 0.01 instead of 0
	}


	IEnumerator FadeOut(CanvasGroup group, GameObject next, float start, float end, float timeLerp = 0.5f)
	{
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

		yield return new WaitForSeconds(2.0f);

		next.SetActive(true);
		// StartCoroutine(FadeIn(next.GetComponent<CanvasGroup>(), 0.75f, 1, 3.0f));
	}

	IEnumerator DramaticPause(GameObject obj, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		obj.SetActive(true);
	}

	public void ChoiceA()
	{
		currAnswer = (int)ANSWERS.A;
	}

	public void ChoiceB()
	{
		currAnswer = (int)ANSWERS.B;
	}

	public void ChoiceC()
	{
		currAnswer = (int)ANSWERS.C;
	}

	public void ChoiceD()
	{
		currAnswer = (int)ANSWERS.D;
	}

	public void ShowENIAC()
	{
		
	}

	public void DisplayItems()
	{
		if(PlayerPrefs.HasKey("Shield")) {
			Debug.Log("Shield");
		}

		if(PlayerPrefs.HasKey("Bar")) {
			Debug.Log("Bar");
		}

		if(PlayerPrefs.HasKey("Fish")) {
			Debug.Log("Fish");
		}

		if(PlayerPrefs.HasKey("Book")) {
			Debug.Log("Book");
		}
	}

	// ENIAC 
	public GameObject ENIACPanel;
	public GameObject AIIntroPanel;
	public GameObject nextENIACClue;
	public void ContinueENIAC()
	{
		ENIACPanel.SetActive(false);
		AIIntroPanel.SetActive(true);
	}

	public void NextClueENIAC()
	{
		AIIntroPanel.SetActive(false);
		nextENIACClue.SetActive(true);
	}

	// GRASP
	public GameObject GRASPPanel;
	public GameObject autonomousPanel;
	public GameObject nextGRASPClue;
	public GameObject nextGRASPCluePanel1;
	public GameObject nextGRASPCluePanel2;
	public GameObject fish;
	public void ContinueGRASP()
	{
		GRASPPanel.SetActive(false);
		autonomousPanel.SetActive(true);
	}

	public void NextClueGRASP()
	{
		autonomousPanel.SetActive(false);
		nextGRASPClue.SetActive(true);
	}

	public void GRASP_A()
	{
		GameObject answers = GameObject.Find("Answers");
		GameObject choice = answers.transform.Find("A").gameObject;  
		GameObject text = choice.transform.Find("Text").gameObject;
		text.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
	}

	public void GRASP_B()
	{
		GameObject answers = GameObject.Find("Answers");
		GameObject choice = answers.transform.Find("B").gameObject;  
		GameObject text = choice.transform.Find("Text").gameObject;
		text.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
	}

	public void GRASP_C()
	{
		GameObject answers = GameObject.Find("Answers");
		answers.transform.Find("C").gameObject.GetComponent<Image>().color = new Color(0, 1, 0, 1);		
		fish.SetActive(true);
		GRASPtoSIG();
	}

	public void GRASPtoSIG()
	{
		nextGRASPCluePanel1.SetActive(false);
		nextGRASPCluePanel2.SetActive(true);
	}

	// Gaming
	public void AlphaGoPhoto()
	{
		Application.OpenURL("http://www.escapistmagazine.com/news/view/166782-Lee-Sedol-vs-Google-DeepMinds-AlphaGo-Program#&gid=gallery_5939&pid=1");		
	}

	public void openRothLink()
	{
		Application.OpenURL("http://www.cis.upenn.edu/~aaroth/");
	}

	public void playSpaceInvadARs()
	{
		Application.LoadLevel(1);
	}

	public GameObject warning;
	public int buttonPressed;
	public void LeaveScavengeWarning()
	{
		warning.SetActive(true);
		pauseButtons.SetActive(false);
	}

	public void ToSpace()
	{
		buttonPressed = (int)LEVELS.SPACE_INVADERS;
		Debug.Log(buttonPressed);
	}

	public void ToMenu()
	{
		buttonPressed = (int)LEVELS.MAIN_MENU;
		Debug.Log(buttonPressed);
	}

	public void CloseWarning()
	{
		warning.SetActive(false);
		pauseButtons.SetActive(true);
	}

	public void LeaveScavenge()
	{
		SceneManager.LoadScene(buttonPressed);
	}

	public GameObject pauseMenu;
	public GameObject instructions;
	public GameObject pauseButtons;
	public bool paused = false;
	public void Pause()
	{
		paused = !paused;
		pauseMenu.SetActive(paused);		
	}

	public void Resume()
	{
		paused = false;
		pauseMenu.SetActive(false);
	}

	public void OpenInstructions()
	{
		instructions.SetActive(true);
		pauseButtons.SetActive(false);
	}

	public void CloseInstructions()
	{
		instructions.SetActive(false);
		pauseButtons.SetActive(true);
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



}

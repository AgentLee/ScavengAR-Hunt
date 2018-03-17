/*
 * This script handles events when the user's image target is at the GRASP Lab.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Autonomy : MonoBehaviour 
{
	GameObject intro;
	public GameObject page1;
	public GameObject nextClue;
	GameObject panel1;
	GameObject panel2;
	GameObject question;
	GameObject answers;

	int currPage;
	
	// Use this for initialization
	void Start () 
	{
		intro 		= transform.Find("GRASP").gameObject;
		page1 		= transform.Find("Cars").gameObject;
		nextClue 	= transform.Find("Next Clue").gameObject;
		currPage 	= 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// On the clue page
		if(currPage == 2) {
			panel1	= nextClue.transform.Find("Panel1").gameObject;
			panel2	= nextClue.transform.Find("Panel2").gameObject;

			question = panel1.transform.Find("Question").gameObject;
			answers = panel1.transform.Find("Answers").gameObject;
		}
	}

	public void Continue()
	{
		switch(currPage) 
		{
			case 0:
				intro.SetActive(false);
				page1.SetActive(true);
				++currPage;
				break;
			case 1:
				page1.SetActive(false);
				nextClue.SetActive(true);
				++currPage;
				break;
			default:
				break;
		}
	}

	// Cars to Clue
	public void GetNextClue()
	{
		page1.SetActive(false);
		nextClue.SetActive(true);
		currPage = 2;

		// Give player extra life
		PlayerPrefs.SetInt("ExtraLife", 1);
	}

	IEnumerator DelayText(GameObject obj)
	{
		yield return new WaitForSeconds(2.0f);
		obj.SetActive(true);
	}

	public void ChoiceA()
	{
		GameObject choice = answers.transform.Find("A").gameObject;  
		GameObject text = choice.transform.Find("Text").gameObject;
		text.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
	}

	public void ChoiceB()
	{
		GameObject choice = answers.transform.Find("B").gameObject;  
		GameObject text = choice.transform.Find("Text").gameObject;
		text.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
	}

	public void ChoiceC()
	{
		GameObject answers = GameObject.Find("Answers");
		answers.transform.Find("C").gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 1, 0, 1);		
		
		StartCoroutine(Fade(panel1.GetComponent<CanvasGroup>(), panel2, 1.0f, 0.0f));
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
	}
}

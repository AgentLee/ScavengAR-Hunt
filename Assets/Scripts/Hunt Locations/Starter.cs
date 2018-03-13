using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Starter : MonoBehaviour 
{
	GameObject intro;
	GameObject nextClue;
	GameObject title;
	GameObject description;
	GameObject question;
	GameObject answers;

	// Use this for initialization
	void Start () 
	{
		intro 		= transform.Find("Intro").gameObject;
		nextClue	= transform.Find("Next Clue").gameObject;
		title 		= intro.transform.Find("Title").gameObject;  
		description = intro.transform.Find("Description").gameObject;  
		question 	= intro.transform.Find("Question").gameObject;  
		answers 	= intro.transform.Find("Answers").gameObject;
		
		description.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(description.GetComponent<TextReveal>().completed) {
			StartCoroutine(DelayText(question));
		}

		if(question.GetComponent<TextReveal>().completed) {
			StartCoroutine(DelayText(answers));
		}
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
		answers.transform.Find("B").gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 1, 0, 1);		
		StartCoroutine(Fade(intro.GetComponent<CanvasGroup>(), nextClue, intro.GetComponent<CanvasGroup>().alpha, 0));
	}

	public void ChoiceC()
	{
		GameObject choice = answers.transform.Find("C").gameObject;  
		GameObject text = choice.transform.Find("Text").gameObject;
		text.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
	}

	public void ChoiceD()
	{
		
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

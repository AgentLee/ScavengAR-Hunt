/*
 * This script is based on a tutorial by YouTube user Zolran. 
 * https://www.youtube.com/watch?v=U85gbZY6oo8
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextReveal : MonoBehaviour 
{
	private TextMeshProUGUI text;
	public bool completed;

	// void Start()
	// {
	// 	gameObject.SetActive(false);
	// 	completed = false;
	// }

	// public void StartReveal()
	// {
	// 	gameObject.SetActive(true);
	// 	StartCoroutine(Reveal());
	// }

	// Use this for initialization
	IEnumerator Start() 
	{
		text = GetComponent<TextMeshProUGUI>();

		int numVisibleChar = text.textInfo.characterCount;
		int count = 0;

		while(true)
		{
			// Determine how many characters to show
			int visCount = count % (numVisibleChar + 1);
			text.maxVisibleCharacters = visCount;

			// Wait a second before revealing the next character
			if(visCount > numVisibleChar) {
				yield return new WaitForSeconds(1.0f);
			}
			// Reached the last character, stop the loop
			else if(visCount == numVisibleChar) {
				completed = true;
				yield break;
			}

			// Iterate to the next character
			++count;

			yield return new WaitForSeconds(0.05f);
		}
	}
}

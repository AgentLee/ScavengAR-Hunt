using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScores : MonoBehaviour 
{
	const string privateCode = "toXkBrVLoU-NIPomJOiC4QLbv3lvZzuEmvE6ogLTX5vA";
	const string publicCode = "5a9c91ff012b300a70a814b4";
	const string webURL = "http://dreamlo.com/lb/";
	 
	public PlayerScore[] scores;
	public GameObject scoreText;

	void Awake()
	{
		// AddHighScore("Jon", 100, 123456789, "jon@email");
		// AddHighScore("Jon", 150, 123456789, "jonlee@email");
		// AddHighScore("Shlane", 90, 123456789, "shlane@email");
		// AddHighScore("Paulo", 75, 123456789, "paulo@email");
		// AddHighScore("Sombra", 999999, 123456789, "sombra@email");
		// AddHighScore("Sombra", 999998, 123456789, "sombr3333a@email");

		RetrieveScores();
	}

	void Update()
	{

	}

	public void AddHighScore(string user, int score, string phone, string email)
	{
		Debug.Log("Updating score for " + user + " with " + score + " at " + email);
		StartCoroutine(UpdateScores(user, score, phone, email));
	}

	public void RetrieveScores()
	{
		StartCoroutine(GetScoresFromServer());
	}

	IEnumerator UpdateScores(string user, int score, string phone, string email)
	{
		float time = 0.0f;
		// Use the player's email as the "key" because dreamlo overwrites the scores based on that
		WWW dbLink = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(email) + "/" + score + "/" + phone + "/" + WWW.EscapeURL(user));

		// Wait till finished uploading
		while(!dbLink.isDone) {
			if(time >= 3.0f) {
				Debug.Log("SHow message");
			}
			else {
				time += Time.deltaTime;
			}

			yield return dbLink;
		}

		if(string.IsNullOrEmpty(dbLink.error)) {
			Debug.Log("Scores updated.");
		}
		else {
			Debug.Log("Error: " + dbLink.error);
		}
	}

	private string[] ERROR_MESSAGES = {	"Whoops, something went wrong...Try restarting the app!",
										"Forgot to feed the hamsters keeping score...Try restarting the app!",
										"My score keeper went on a coffee break...Try restarting the app!"};
	IEnumerator GetScoresFromServer()
	{
		float time = 0.0f;
		WWW www = new WWW(webURL + publicCode + "/pipe/");

		// Wait till finished uploading
		while(!www.isDone) {
			scoreText.GetComponent<TMP_Text>().text = "Loading";
			
			// Timeout message
			if(time >= 3.0f) {
				scoreText.GetComponent<TMP_Text>().text += "\n";
				scoreText.GetComponent<TMP_Text>().text += ERROR_MESSAGES[Random.Range(0, 4)];
			}
			else {
				time += Time.deltaTime;
			}

			yield return www;
		}

		if(string.IsNullOrEmpty(www.error)) {
			print(www.text);
			FormatScores(www.text);
		}
		else {
			Debug.Log("Error: " + www.error);
			scoreText.GetComponent<TMP_Text>().text = "Couldn't load scores";
		}
	}

	void FormatScores(string textScores)
	{
		scoreText.GetComponent<TMP_Text>().text = "";
		
		string[] entries = textScores.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		scores = new PlayerScore[entries.Length];

		for(int i = 0; i < entries.Length; ++i) {
			string[] entryInfo 	= entries[i].Split(new char[]{'|'});
			string email 		= entryInfo[0];
			int score 			= int.Parse(entryInfo[1]);
			string phone 		= entryInfo[2];
			string username 	= entryInfo[3];

			// New user, don't show score
			if(score == -999999) {
				continue;
			}

			scores[i] = new PlayerScore(username, score, phone, email);

			if(i < 5) {
				if(i == 0) {
					PlayerPrefs.SetInt("TopScore", scores[i].score);
				}

				// scoreText.GetComponent<TMP_Text>().text += (i + 1).ToString() + ". " + username + " - " + score + "\n";
				scoreText.GetComponent<TMP_Text>().text += username + " - " + score + "\n";
			}

			// print(scores[i].username + "-" + scores[i].email + ": " + scores[i].score + "-" + scores[i].phone);
		}
	}
}


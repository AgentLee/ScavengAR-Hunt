using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FutureFinale : MonoBehaviour 
{
	public GameObject sophia;
	public GameObject future;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	// Sophia to Future page
	public void NextPage()
	{	
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

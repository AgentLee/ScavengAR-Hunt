using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public PlayerController player;
	public GameObject scoreText;

	public GameObject ground;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(ground.GetComponent<MeshRenderer>().enabled) {
			ground.GetComponent<MeshRenderer>().enabled = false;
		}

		// Update score text
		scoreText.GetComponent<Text>().text = "Drones Hit: " + player.score;		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public PlayerController player;
	public GameObject scoreText;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{	
		// Update score text
		scoreText.GetComponent<Text>().text = "Drones Hit: " + player.score;		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour 
{
	public PlayerController player;
	public SimplePlayerController simplePlayer;
	public GameObject scoreText;

	public GameObject ground;
	public GameObject drones;
	public float droneMinBound, droneMaxBound;
	public float droneSpeed;

	public int level;

	// Use this for initialization
	void Start () 
	{
		level = 1;
		droneMinBound = -18.0f;
		droneMaxBound = 18.0f;
		droneSpeed = 0.25f;

		Physics.gravity = new Vector3(0, -150.0f, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(level == 1) {
			RunLevelOne();
			return;
		}

		if(ground.GetComponent<MeshRenderer>().enabled) {
			ground.GetComponent<MeshRenderer>().enabled = false;
		}

		// Update score text
		// scoreText.GetComponent<Text>().text = "Drones Hit: " + player.score;		
	}

	void RunLevelOne()
	{
		scoreText.GetComponent<Text>().text = simplePlayer.score.ToString();		

		drones.transform.position += Vector3.right * droneSpeed;
		foreach(Transform drone in drones.transform) {
			if(drone.position.x < droneMinBound || drone.position.x > droneMaxBound) {
				droneSpeed = -droneSpeed;
				return;
			}
		}
	}
}

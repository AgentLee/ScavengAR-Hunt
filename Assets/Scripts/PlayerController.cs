using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public GameObject bullet;
	public int score;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Fire()
	{
		Instantiate(bullet, Camera.main.transform.position, Camera.main.transform.rotation * Quaternion.Euler(-90, 0, 0));
	}
}

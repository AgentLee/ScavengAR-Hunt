/*
 * This script is based on the script found on the Unity Wiki page. 
 * http://wiki.unity3d.com/index.php?title=Shield
 * 
 * The main purpose of this is to test material properties of the base.
 * This does not get used in the acutal game.
 * Handling of the actual base is found in SimpleBase.cs
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour 
{
	private Material material;
	private bool hit;
	public int timesHit;
	private int shieldLevel;

	// Use this for initialization
	void Start () 
	{
		material = this.GetComponent<Renderer>().material;
 
		hit = false;
		timesHit = 0;

		shieldLevel = 3;
	}
 
	// Update is called once per frame
	void Update () 
	{
		// Testing shader params
		if(Input.GetKey(KeyCode.A)) {
			Debug.Log("CHANGE STRENGTH");
			material.SetFloat("_Strength", material.GetFloat("_Strength") * 1.1f);
		}
		else if(material.GetFloat("_Strength") > 0.96f) {
			material.SetFloat("_Strength", material.GetFloat("_Strength") * 0.9f);			
		}

		if(Input.GetKeyDown(KeyCode.P)) {
			// StartCoroutine(RegenShield(3.0f));
			// Debug.Log(material.color + " " + Color.yellow + " " + Color.red);
			StartCoroutine(ChangeShieldLevel());
		}
		
	}

	IEnumerator RegenShield(float duration)
	{
		material.SetFloat("_Strength", 0.01f);

		float time = 0;

		while(time < duration) {
			time += Time.deltaTime;
			Debug.Log(material.GetFloat("_Strength"));
			if(material.GetFloat("_Strength") >= 0.96) {
				break;
			}

			material.SetFloat("_Strength", material.GetFloat("_Strength") * 1.15f);			
			yield return null;
		}
	}

	// This does an interesting blink
	// IEnumerator ShieldsUp(float duration)
	// {
	// 	while(duration >= 0.0f) {
	// 		duration -= Time.deltaTime;

	// 		material.SetFloat("_Strength", material.GetFloat("_Strength") * 1.1f);
		
	// 		yield return null;
	// 	}
	// }

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Bullet" || collisionInfo.collider.tag == "Enemy Bullet") {
			// Physics.IgnoreCollision(collisionInfo.collider, this.GetComponent<Collider>());
			if(!hit) {
				StartCoroutine(Blink(3.0f, 0.2f, collisionInfo.collider));

				if(++timesHit > 2) {
					// Change color to indicate shield level.
					// material.SetColor("_Color", Color.red);

					StartCoroutine(ChangeShieldLevel());
				}
			}
			else {
				// Need to figure out how to bounce the bullets off.
				// It gets stuck on the top of the shield.
				Destroy(collisionInfo.collider.gameObject);
			}
		}
	}

	IEnumerator ChangeShieldLevel()
	{
		// StartCoroutine(Blink(3.0f, 0.2f, null));
		
		Color changeTo;

		// Level 3
		if(shieldLevel == 3) {
			changeTo = Color.yellow;
		}
		// Level 2
		else if(shieldLevel == 2) {
			changeTo = Color.red;
		}
		// Level 1
		else {
			// Already red don't have to change colors.
			yield break;
		}

		float time = 0;
		while(material.color != changeTo) {
			time += Time.deltaTime;
			material.color = Color.Lerp(material.color, changeTo, time);

			yield return null;
		}

		--shieldLevel;
		timesHit = 0;
		// StartCoroutine(Blink(3.0f, 0.2f, null));
	}

	// void OnTriggerEnter(Collider collider)
	// {
	// 	if(!hit) {
	// 		StartCoroutine(Blink(3.0f, 0.2f, collider));
	// 	}
	// }

	IEnumerator Blink(float duration, float blinkTime, Collider collider)
	{
		hit = true;

		if(collider != null) {
			Destroy(collider.gameObject);
		}

		while(duration >= 0f) {
			// duration -= Time.deltaTime;
			duration -= 0.5f;

			// Blink 
			GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

			yield return new WaitForSeconds(blinkTime);
		}
		
		// Physics.IgnoreCollision(collider, this.GetComponent<Collider>());
		this.GetComponent<Renderer>().enabled = true;
		hit = false;

		StartCoroutine(RegenShield(3.0f));
	}
}
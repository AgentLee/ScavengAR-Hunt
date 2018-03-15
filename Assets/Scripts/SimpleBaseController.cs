/*
 * The main purpose of this script is to handle all base interactions.
 *
 * This script is based on the script found on the Unity Wiki page. 
 * http://wiki.unity3d.com/index.php?title=Shield
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBaseController : MonoBehaviour 
{
	private Material material;
	public bool hit;
	public int timesHit;
	public int shieldLevel;
	private int vulnerability;
	private AudioSource shieldRegen;

	// Use this for initialization
	void Start () 
	{
		material 		= this.GetComponent<Renderer>().material;
		hit 			= false;
		timesHit 		= 0;

		// Debug.Log(PlayerPrefs.GetInt("Shield"));

		// PlayerPrefs.DeleteKey("Shield");
		if(PlayerPrefs.HasKey("Shield")) {
			switch(PlayerPrefs.GetInt("Shield"))
			{
				case 1:
					shieldLevel 	= 1;
					vulnerability 	= 5;
					material.color 	= Color.yellow;
					break;
				case 2:
					shieldLevel 	= 2;
					vulnerability 	= 10;
					material.color 	= Color.green;
					break;
				// case 3:
				// 	shieldLevel 	= 3;
				// 	vulnerability 	= 10;
				// 	material.color 	= Color.green;
				// 	break;
			}
		}
		else {
			shieldLevel 	= 1;
			vulnerability 	= 2;
			material.color 	= Color.red;
		}

		shieldRegen = GetComponent<AudioSource>();
	}
 
	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Bullet" || collisionInfo.collider.tag == "Enemy Bullet") {
			if(collisionInfo.collider.tag == "Bullet") {
				// If it already hit an enemy, the debris shouldn't affect the shield.
				if(collisionInfo.collider.GetComponent<SimpleBulletController>().hit) {
					return;
				}
			}
			
			// Physics.IgnoreCollision(collisionInfo.collider, this.GetComponent<Collider>());
			if(!hit) {
				shieldRegen.Play();

				StartCoroutine(Blink(4.25f, 0.2f, collisionInfo.collider));

				if(++timesHit >= vulnerability) {
					// Base is dead
					if(shieldLevel == 1) {
						Destroy(gameObject);
					}
					else {
						StartCoroutine(ChangeShieldLevel());
						StartCoroutine(RegenShield(4.25f));
					}
				}
			}
			else {
				// Need to figure out how to bounce the bullets off.
				// It gets stuck on the top of the shield.
				Destroy(collisionInfo.collider.gameObject);
			}
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

	IEnumerator ChangeShieldLevel()
	{
		Color changeTo;
		switch(shieldLevel) 
		{
			// Level 3
			case 3:
				vulnerability = 5;
				changeTo = Color.yellow;
				break;
			// Level 2
			case 2:
				vulnerability = 2;
				changeTo = Color.red;
				break;
			// Level 1
			case 1:
				timesHit = 0;
				yield break;
			// Shouldn't get to this
			default:
				changeTo = Color.black;
				break;
		}

		float time = 0;
		while(material.color != changeTo) {
			time += Time.deltaTime;
			material.color = Color.Lerp(material.color, changeTo, time);

			yield return null;
		}

		--shieldLevel;
		timesHit = 0;
	}

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

		// StartCoroutine(RegenShield(3.0f));
	}
}

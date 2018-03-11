using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour 
{
	private SpriteRenderer renderer;

	// Use this for initialization
	void Start () 
	{
		renderer = GetComponent<SpriteRenderer>();
	}
	
	IEnumerator FadeIn()
	{
		for(float f = 0.05f; f <= 1.0f; f += 0.05f) {
			Color color = renderer.material.color;
			color.a = f;
			renderer.material.color = color;

			yield return new WaitForSeconds(0.05f);
		}
	}

	IEnumerator FadeOut()
	{
		for(float f = 1.0f; f >= -0.05f; f -= 0.05f) {
			Color color = renderer.material.color;
			color.a = f;
			renderer.material.color = color;

			yield return new WaitForSeconds(0.05f);
		}
	}

	public void StartFadeIn()
	{
		Color color = renderer.material.color;
		color.a = 0.0f;
		renderer.material.color = color;	
		
		StartCoroutine(FadeIn());
	}

	public void StartFadeOut()
	{
		StartCoroutine(FadeOut());
	}
}


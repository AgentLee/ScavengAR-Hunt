using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SplashFade : MonoBehaviour 
{
	public Image splashImage;
	public TextMeshProUGUI splashText1;
	public TextMeshProUGUI splashText2;

	IEnumerator Start()
	{
		splashImage.canvasRenderer.SetAlpha(0.0f);
		splashText1.canvasRenderer.SetAlpha(0.0f);
		splashText2.canvasRenderer.SetAlpha(0.0f);

		FadeIn();
		yield return new WaitForSeconds(2.0f);
		FadeOut();
		yield return new WaitForSeconds(2.0f);

		SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
	}

	void FadeIn()
	{
		splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
		splashText1.CrossFadeAlpha(1.0f, 1.5f, false);
		splashText2.CrossFadeAlpha(1.0f, 1.5f, false);
	}

	void FadeOut()
	{
		splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
		splashText1.CrossFadeAlpha(0.0f, 2.5f, false);
		splashText2.CrossFadeAlpha(0.0f, 2.5f, false);
	}
}

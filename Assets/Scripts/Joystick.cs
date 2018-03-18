using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	private Image bgImage;
	private Image joystickImage;

	public Vector3 InputDirection{ set; get; }

	private void Start()
	{
		bgImage 		= GetComponent<Image>();
		joystickImage 	= transform.GetChild(0).GetComponent<Image>();
		InputDirection 	= Vector3.zero;
	}

	public virtual void OnDrag(PointerEventData pointerEventData)
	{
		Vector2 pos = Vector2.zero;

		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(	bgImage.rectTransform, 
																	pointerEventData.position, 	
																	pointerEventData.pressEventCamera, 
																	out pos)) 
		{
			pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

			float x = (bgImage.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
			// float y = (bgImage.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;
			float y = 0.0f;

			InputDirection = new Vector3(x, 0, y);
			InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection; 

			joystickImage.rectTransform.anchoredPosition = new Vector2(	InputDirection.x * (bgImage.rectTransform.sizeDelta.x / 3), 
																		InputDirection.z * (bgImage.rectTransform.sizeDelta.y / 3));
		}
	}

	public virtual void OnPointerDown(PointerEventData pointerEventData)
	{
		OnDrag(pointerEventData);

		// StartCoroutine(HideJoystick());
	}

	IEnumerator HideJoystick()
	{
		yield return new WaitForSeconds(.75f);
		
		bgImage.color = new Color(bgImage.color.r, bgImage.color.b, bgImage.color.g, 0.0f);
		joystickImage.color = new Color(joystickImage.color.r, joystickImage.color.b, joystickImage.color.g, 0.0f);
	}

	public virtual void OnPointerUp(PointerEventData pointerEventData)
	{
		InputDirection = Vector3.zero;
		joystickImage.rectTransform.anchoredPosition = Vector3.zero;

		bgImage.color = new Color(bgImage.color.r, bgImage.color.b, bgImage.color.g, 1.0f);
		joystickImage.color = new Color(joystickImage.color.r, joystickImage.color.b, joystickImage.color.g, 1.0f);
	}
}

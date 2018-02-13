	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour 
{
	public Player player;	
	public Camera camera;

	public DeviceType device;
	
	private float lastClickTime = 0;
	private float catchTime = .25f;
	
	bool oneClick = false;
	bool timerRunning;
	float timerDoubleClick;
	float delay = .25f;
	int clicks = 0;

	public GameObject collectedCanvas;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		device = SystemInfo.deviceType;
		collectedCanvas = GameObject.Find("Collected");
	}
	
	// Update is called once per frame
	void Update () 
	{
		// This might hinder performance. Need to test.
		if(device.Equals(DeviceType.Handheld)) {
			// Loop through all the touch points
			for(int i = 0; i < Input.touchCount; ++i) {
				if(Input.GetTouch(i).phase == TouchPhase.Began) {
					// Shoot a ray from the touch position to the scene
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

					RaycastHit objectHit;
					if(Physics.Raycast(ray, out objectHit)) {
						player.collectItem(objectHit.collider.gameObject);
					}
				}
			}
		}
		else {
			if(Input.GetMouseButtonDown(0)) {
				float dT = Time.time - timerDoubleClick;

				++clicks;

				// Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				// RaycastHit hit;	

				// if(clicks == 2 && dT < 0.25f) {
				// 	Debug.Log("DOUBLE");
				// 	// Update time
				// 	timerDoubleClick = Time.time;
				// 	clicks = 0;

				// 	if(Physics.Raycast(ray, out hit)) {
				// 		if(hit.collider.gameObject.tag == "Cube") {
				// 			player.collectItem(hit.collider.gameObject);
				// 			Debug.Log(player.numItems());
				// 		}
				// 	}
				// }
				// else {
				// 	Debug.Log("SINLGE");
				// }


				// if(dT > 0.25f) {
				// 	// Double click
				// }

				// if(!oneClick) {
				// 	oneClick = true;
				// 	timerDoubleClick = Time.time;
				// 	Debug.Log("ONE CLICK");
				// }
				// else {
				// 	Debug.Log("Delta " + (Time.time - timerDoubleClick).ToString());
				// 	oneClick = false;
				// 	Debug.Log("DOUBLE CLICK");

				// 	Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				// 	RaycastHit hit;	

				// 	if(Physics.Raycast(ray, out hit)) {
				// 		if(hit.collider.gameObject.tag == "Cube") {
				// 			player.collectItem(hit.collider.gameObject);
				// 			Debug.Log(player.numItems());
				// 		}
				// 	}
				// }
				// if(oneClick) {
				// 	// Debug.Log("Delta " + (Time.time - timerDoubleClick).ToString());
				// 	if((Time.time - timerDoubleClick) > .02f) {
				// 		oneClick = false;
				// 	}
				// }

				Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;	

				// if(Time.time - lastClickTime < catchTime) {
				// 	Debug.Log("Double Click");
					
					if(Physics.Raycast(ray, out hit)) {
						player.collectItem(hit.collider.gameObject);
					}
				// }
				// else {
				// 	Debug.Log("Click");
				// }

				// lastClickTime = Time.time;
			}	
		}
	}
}

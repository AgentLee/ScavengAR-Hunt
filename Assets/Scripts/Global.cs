	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour 
{
	public Player player;	
	public Camera camera;

	public DeviceType device;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		device = SystemInfo.deviceType;
		GameObject.FindGameObjectWithTag("Canvas").SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// This might hinder performance. Need to test.
		if(device.Equals(DeviceType.Handheld)) {
			Debug.Log("smartphone");

			// Loop through all the touch points
			for(int i = 0; i < Input.touchCount; ++i) {
				if(Input.GetTouch(i).phase == TouchPhase.Began) {
					// Shoot a ray from the touch position to the scene
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

					RaycastHit objectHit;
					if(Physics.Raycast(ray, out objectHit)) {
						GameObject cubeUI = GameObject.FindGameObjectWithTag("CubeUI");
						if(!cubeUI) Debug.Log("couldnt' find");
						// cubeUI.SetActive(true);
						
						Destroy(objectHit.collider.gameObject);
					}
				}
			}
		}
		else {
			if(Input.GetMouseButtonDown(0)) {
				Ray ray = camera.ScreenPointToRay(Input.mousePosition);

				RaycastHit hit;
				if(Physics.Raycast(ray, out hit)) {
					if(hit.collider.gameObject.tag == "Cube") {
						player.collectItem(hit.collider.gameObject);
						Debug.Log(player.numItems());
					}
				}
			}	
		}
	}
}

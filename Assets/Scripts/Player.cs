using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	private List<GameObject> items;

	// Inventory 
	public GameObject inventory;
	public Image cubeImage, sphereImage;

	// Show these if player comes back to the marker
	public GameObject collectedCube;
	public GameObject collectedSphere;

	// Use this for initialization
	void Start () 
	{
		items = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void collectItem(GameObject item)
	{
		items.Add(item);

		// Can't delete the object?
		item.SetActive(false);

		// Update Inventory UI
		if(item.name == "Cube") {
			collectedCube.SetActive(true);
			cubeImage.enabled = true;
		}
		else if(item.name == "Sphere") {
			collectedSphere.SetActive(true);
			sphereImage.enabled = true;
		}

		inventory.SetActive(true);

		Debug.Log("Add to inventory");
	}

	public int numItems()
	{
		return items.Count;
	}
}

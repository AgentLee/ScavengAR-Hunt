using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	private List<GameObject> items;
	private int totalItems = 1;

	public Image img;

	// Use this for initialization
	void Start () 
	{
		items = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if(items.Count == totalItems) {
		// 	// Debug.Log("COLLECTED ALL");
		// 	for(int i = 0; i < items.Count; ++i) {
		// 		Debug.Log(items[i].tag);
		// 	}
		// }	
	}

	public void collectItem(GameObject item)
	{
		items.Add(item);

		// Can't delete the object?
		item.SetActive(false);

		// Update Inventory UI
		img.enabled = true;

		Debug.Log("Add to inventory");
	}

	public int numItems()
	{
		return items.Count;
	}
}

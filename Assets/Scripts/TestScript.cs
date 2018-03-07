using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// http://www.donovankeith.com/2016/05/making-objects-float-up-down-in-unity/
public class TestScript : MonoBehaviour 
{
    float amplitude = 5.0f;
    float frequency = 1.0f;
 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    // Use this for initialization
    void Start () 
	{
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }
     
    void Update () 
	{
		Bounce();
    }

	void Bounce()
	{
        tempPos = posOffset;
		tempPos.z += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
	}
}

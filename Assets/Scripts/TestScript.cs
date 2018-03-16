using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// http://www.donovankeith.com/2016/05/making-objects-float-up-down-in-unity/
public class TestScript : MonoBehaviour 
{
    Quaternion rot;
    float degPerSec = 15.0f;
    float amplitude = 5.0f;
    float frequency = 1.0f;

    public bool rotate;
    public bool bounce;
 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    // Use this for initialization
    void Start () 
	{
        rot = transform.rotation;
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }
     
    void Update () 
	{
        if(rotate) {
            Rotate();
        }

        if(bounce) {
		    Bounce();
        }
    }

    void LateUpdate()
    {
        // Quaternion quat = transform.rotation;
		// transform.rotation = Quaternion.Euler(quat.eulerAngles.x, rot.eulerAngles.y, quat.eulerAngles.z);
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degPerSec, 0f));
    }

	void Bounce()
	{
        tempPos = posOffset;
		tempPos.z += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
	}
}

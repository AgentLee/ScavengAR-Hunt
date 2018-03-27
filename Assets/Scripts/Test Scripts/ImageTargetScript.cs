/*
 * Script based on https://developer.vuforia.com/forum/faq/unity-how-can-i-play-audio-when-targets-get-detected
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTargetScript : MonoBehaviour, ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;

	public GameObject ground, player, bases;

	// Use this for initialization
	void Start () 
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if(mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                    	TrackableBehaviour.Status newStatus)
    {
		if(FoundTarget(previousStatus, newStatus)) {
            Debug.Log("FOUND TARGET");

			ground.SetActive(true);
			player.SetActive(true);
			bases.SetActive(true);
		}
		else {
            Debug.Log("LOST TARGET");
		}
    }   

	public bool FoundTarget(	TrackableBehaviour.Status previousStatus,
								TrackableBehaviour.Status newStatus)
	{
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
			return true;
        }
        else
        {
			return false;
        }
	}
}

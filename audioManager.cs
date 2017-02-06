using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour {
    public AudioClip[] musics;
    AudioSource audio;
	// Use this for initialization
	void Start () {
        gameManager.Notifications.AddListener(notificationManager.EVENT_TYPE.PLAY_SOUND, playSound);
        audio = GetComponent<AudioSource>();
	}
    void playSound(int soundTrack)
    {
        try
        {
            audio.clip = musics[soundTrack];
            audio.Play();
        }
        catch
        {
            Debug.Log("error in sound number");
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}

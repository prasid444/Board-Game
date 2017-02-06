using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//for receiving and sending notifications

[RequireComponent(typeof(notificationManager))]
public class gameManager : MonoBehaviour {



	public enum inputState
	{
		inputOn,inputOff
	}
	public static gameManager Instance{
		get{
			if(instance==null)
				instance=new GameObject("gameManager").AddComponent<gameManager>();
				return instance;
		}
		
	}

	void Start(){
		//speedFactor = 1f;
		DontDestroyOnLoad(gameObject);
	}
	void OnLevelWasLoaded() {
		notifications.ClearListeners ();
		DontDestroyOnLoad(gameObject);
	}
//to retrieve notification manager
public static notificationManager Notifications{
	get{
		if(notifications==null)
			notifications=instance.GetComponent<notificationManager>();
		return notifications;
	}
	
}
//internal reference for singleton behaviour
private static gameManager instance=null;
private static notificationManager notifications=null;
void Awake(){
	//check if existing
	if((instance)&&instance.GetInstanceID()!=GetInstanceID())
		DestroyImmediate(gameObject);
	else{
		instance=this;
		DontDestroyOnLoad(gameObject);
			Debug.Log ("here");

	}

}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class notificationManager : MonoBehaviour {
	//speed is increased in tunnel stuffs
	public enum EVENT_TYPE{
        FLOOR_SET_TRANSPARENT,TRANS_FLOOR_ON,FLOOR_OFF,FIND_DICE_FACE,MOVE_PLAYER,PLAY_SOUND
	};
	public  delegate void argumnentDelegate(int roomNo);
	private Dictionary<EVENT_TYPE,List<argumnentDelegate>> listenersNew =new Dictionary<EVENT_TYPE,List<argumnentDelegate>> ();

	public void AddListener(EVENT_TYPE Etype,argumnentDelegate listener){
		if (!listenersNew.ContainsKey (Etype)) {
			listenersNew.Add (Etype, new List<argumnentDelegate> ());
		}
			listenersNew[Etype].Add (listener);
			//Debug.Log (Etype);
	}

	public void postNotification(EVENT_TYPE Etype,int room){
		if (!listenersNew.ContainsKey (Etype)) {
			return;
		}
			foreach(argumnentDelegate listener in listenersNew[Etype]){
				listener (room);
			}
		}


	public void removeRedundancies(){
		 Dictionary<EVENT_TYPE,List<argumnentDelegate>> templistenersNew =new Dictionary<EVENT_TYPE,List<argumnentDelegate>> ();
		foreach (KeyValuePair<EVENT_TYPE,List<argumnentDelegate>>Item in listenersNew) {
			for (int i = Item.Value.Count - 1; i >= 0; i--) {
				if (Item.Value [i] == null)
					Item.Value.RemoveAt (i);
			}
			if (Item.Value.Count > 0)
				templistenersNew.Add (Item.Key, Item.Value);
		}
		//replace listeners object with optimized dictionary
		listenersNew=templistenersNew;
	}
	public void ClearListeners(){
		listenersNew.Clear ();
		}
	void OnLevelWasLoaded(){
		removeRedundancies ();
	}
}
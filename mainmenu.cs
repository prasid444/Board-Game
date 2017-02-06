using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour {
	
	private Transform cameraTransform;
	private Transform cameraDesiredToLook;
	// Use this for initialization
	void Start () {
		cameraTransform = Camera.main.transform;
	



		
	}
	private void Update(){
		if (cameraDesiredToLook != null) {
			cameraTransform.rotation = Quaternion.Slerp (cameraTransform.rotation, cameraDesiredToLook.rotation, 3 * Time.deltaTime);
		
		}
		
	}
//	// Update is called once per frame
//	void Update () {
//		
//	}

	public void LookAtMenu(Transform menuTransform){
		cameraDesiredToLook = menuTransform;
	}
	public void playBoard(){
		Application.LoadLevel ("scene1");
	}
	public void notAvailable(){
		Debug.Log ("Game not available");
	}
	public void exitGame(){
		Application.Quit();

	}
}

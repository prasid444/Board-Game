using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void OnEnable()
    {
        Invoke("setDisable", 2);
    }
    void setDisable()
    {
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

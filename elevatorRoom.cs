using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorRoom : MonoBehaviour {
	// Use this for initialization
    Material elevatorMaterial;
	void Start () {
        elevatorMaterial = Resources.Load<Material>("elevator");
        gameObject.GetComponent<Renderer>().material = elevatorMaterial;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

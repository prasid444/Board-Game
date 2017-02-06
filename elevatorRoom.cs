using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorRoom : MonoBehaviour {
	// Use this for initialization
    Material elevatorMaterial;
    TextMesh text;
	void Start () {
        elevatorMaterial = Resources.Load<Material>("elevator");
        gameObject.GetComponent<Renderer>().material = elevatorMaterial;
        text = GetComponentInChildren<TextMesh>();
        if (text != null)
        {
            text.fontSize = 12;
            text.text = "E";
        }
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

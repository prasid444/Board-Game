using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeRoom : MonoBehaviour {
    Material holeMaterial;
    TextMesh text;
    void Start()
    {
        holeMaterial = Resources.Load<Material>("hole");
        gameObject.GetComponent<Renderer>().material = holeMaterial;
        text = GetComponentInChildren<TextMesh>();
        if (text != null)
        {
            text.fontSize = 12;
            text.text = "H";
        }
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

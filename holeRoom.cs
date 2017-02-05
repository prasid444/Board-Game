using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeRoom : MonoBehaviour {
    Material holeMaterial;
    void Start()
    {
        holeMaterial = Resources.Load<Material>("hole");
        gameObject.GetComponent<Renderer>().material = holeMaterial;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

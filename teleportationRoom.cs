using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportationRoom : MonoBehaviour {

    Material teleportationMaterial;
    void Start()
    {
        teleportationMaterial = Resources.Load<Material>("teleportation");
        gameObject.GetComponent<Renderer>().material = teleportationMaterial;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

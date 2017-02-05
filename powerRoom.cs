using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerRoom : MonoBehaviour {
    Material powerMaterial;
    void Start()
    {
        powerMaterial = Resources.Load<Material>("power");
        gameObject.GetComponent<Renderer>().material = powerMaterial;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerRoom : MonoBehaviour {
    Material powerMaterial;
    public GameObject powerParticleEffect;
 
    void Start()
    {
        powerMaterial = Resources.Load<Material>("power");
        powerParticleEffect = Resources.Load<GameObject>("particleEffectPower");
        powerParticleEffect.transform.position = this.transform.position;
        Instantiate(powerParticleEffect);
        gameObject.GetComponent<Renderer>().material = powerMaterial;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

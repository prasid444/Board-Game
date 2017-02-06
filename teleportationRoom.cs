using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportationRoom : MonoBehaviour {

    Material teleportationMaterial;
    public GameObject teleportationParticleEffect;
    void Start()
    {
        teleportationMaterial = Resources.Load<Material>("teleportation");
        teleportationParticleEffect = Resources.Load<GameObject>("particleEffectTele");
        teleportationParticleEffect.transform.position = this.transform.position;
        Instantiate(teleportationParticleEffect);
        gameObject.GetComponent<Renderer>().material = teleportationMaterial;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

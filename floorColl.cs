using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorColl : MonoBehaviour {
    Transform trans;
    // Use this for initialization
    void Start()
    {
        trans = this.transform;
        gameManager.Notifications.AddListener(notificationManager.EVENT_TYPE.FLOOR_OFF, floorOff);
      
    }
    void floorOff(int floorNo)
    {
        int counter = 0;
        foreach (Transform t in trans)
        {
            if (counter != floorNo)
            {

                t.gameObject.SetActive(false);
            }
            else
            {
                t.gameObject.SetActive(true);
            }
            counter++;
        }
       
    } 
	// Update is called once per frame
	void Update () {
		
	}
}

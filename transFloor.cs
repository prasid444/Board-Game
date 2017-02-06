using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transFloor : MonoBehaviour {
    Transform trans;
    int childNo;
	// Use this for initialization
	void Start () {
        gameManager.Notifications.AddListener(notificationManager.EVENT_TYPE.FLOOR_SET_TRANSPARENT, switchFloor);
        gameManager.Notifications.AddListener(notificationManager.EVENT_TYPE.TRANS_FLOOR_ON, transparentFloorOn);
        trans = this.transform;
        childNo = trans.childCount - 1;
        foreach (Transform t in trans)
        {
            t.gameObject.SetActive(false);
        }
       
	}
    void transparentFloorOn(int floorOn)
    {
        int counter = 0;
        foreach (Transform t in trans)
        {
            if (counter != floorOn)
            {

                t.gameObject.SetActive(true);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
            counter++;
        }
        gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.FLOOR_OFF, floorOn);

    }
    void switchFloor(int floorNum)
    {
        Debug.Log("called");
        Debug.Log(floorNum);
        Debug.Log(childNo);
        for (int i = 0; i <= childNo; i++)
        {
            if (floorNum == i)
            {
                transparentFloorOn(i);

                Debug.Log("ball is in "+i+1+"floor");


            }
        }
           

    }
	// Update is called once per frame
	void Update () {
		
	}
}

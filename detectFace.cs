using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectFace : MonoBehaviour {

	public LayerMask diceFaceColliderLayer;
	public int currentval ;
    RaycastHit hit;
	// Use this for initialization
	void Start () {
        gameManager.Notifications.AddListener(notificationManager.EVENT_TYPE.FIND_DICE_FACE, findDiceFace);

	}
    void findDiceFace(int x)
    {
        if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity, diceFaceColliderLayer))
        {
            currentval = hit.collider.GetComponent<facevalue>().value;
        }
        gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.MOVE_PLAYER, currentval);
        Debug.Log("found");
    }
	// Update is called once per frame
	void FixedUpdate () {
		

		
	}
}

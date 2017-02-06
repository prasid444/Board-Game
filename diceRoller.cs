using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceRoller : MonoBehaviour {
	public GameObject diceObj;
	//public Vector3 spawnPoint;
	public float thrust;
	public string fireBtn = "Fire1";
    bool once;
	private Rigidbody diceRb;
    public Transform spawnPoint;
    public static gameManager.inputState _inputState;

	// Use this for initialization
	void Start () {
        once = false;
		diceObj.SetActive (false);
        _inputState = gameManager.inputState.inputOn;

	}
	
	// Update is called once per frame
	void ixedUpdate () {
		if (Input.GetButtonDown (fireBtn)) {
			//diceObj = Instantiate(dicePrefab, spawnPoint, new Quaternion(1,2,3,0)) as GameObject;
			diceObj.SetActive(true);
            once = true;
			diceRb = diceObj.GetComponent<Rigidbody> ();
			float y = Random.Range (0.0f,0.4f);
			diceRb.AddForce ((transform.forward+new Vector3 (0,-y,0)) * thrust, ForceMode.VelocityChange);
			diceRb.AddTorque (Random.onUnitSphere * thrust, ForceMode.VelocityChange);
		}
		if (diceRb && diceRb.IsSleeping()&&once) {

            gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.FIND_DICE_FACE, 1);
            once = false;
		}
	}
    void FixedUpdate()
    {
        if (diceRb && diceRb.IsSleeping() && once)
        {
            Debug.Log("adas");
            gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.FIND_DICE_FACE, 1);
            diceObj.transform.position = spawnPoint.position;
            diceObj.SetActive(false);
            once = false;
           
        }
    }
    public void onClick()
    {
        if (_inputState == gameManager.inputState.inputOn)
        {
            Invoke("playsound", .7f);
           diceObj.SetActive(true);
            once = true;
            diceRb = diceObj.GetComponent<Rigidbody>();
            float y = Random.Range(0.0f, 0.4f);
            diceRb.AddForce((transform.forward + new Vector3(0, -y, 0)) * thrust, ForceMode.VelocityChange);
            diceRb.AddTorque(Random.onUnitSphere * thrust, ForceMode.VelocityChange);
            _inputState = gameManager.inputState.inputOff;

        }

    }
    public void Replay()
    {
        Application.LoadLevel(00);
    }
    void playsound()
    {
        gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.PLAY_SOUND, 3);
           

    }
}

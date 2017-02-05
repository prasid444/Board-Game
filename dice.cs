using UnityEngine;

public class dice : MonoBehaviour
{
    enum diceFaceNumber{
        ONE,TWO,THREE,FOUR,FIVE,SIX,NONE
    }
    enum direction
    {
        RIGHT,FORWARD,LEFT,UP,DOWN
    }
    direction _direction,_prevDirection;
    diceFaceNumber _diceFaceNumber, _prevDiceFaceNumber;
    Transform diceTransform;
    float gravity;
    Vector3 moveDirection;
     float speed;
    CharacterController _characterController;
    Ray ray;
    RaycastHit hit;
     int roomSize;
    // Use this for initialization
    bool moveOnXAxis;
    Vector3 pointToMove;
    Vector3 directionalVector;
    Vector3 initialPosition;
      float noOfRows;
     float noOfColumns;
    int roomNumber;
	 int roomGap;
    GameObject diceTracking;
    bool movePlayer;
    int counter = 0;
    int floorClimbed;
    Vector3 initialRoomPosition;
    public int teleportationValue;
    GameObject[] rooms;
    void Start()
    {
        runTimeInstantiateAndInitializing();
        
   
        if (Physics.Raycast(diceTransform.position, -Vector3.up, out hit))
        {
            initialPosition = hit.collider.gameObject.transform.position;
            initialRoomPosition = hit.collider.gameObject.transform.position;
			diceTransform.position= hit.collider.gameObject.transform.position+new Vector3(0,roomSize/2,0);
            Debug.Log(initialPosition);
             diceTracking = new GameObject("diceTracking");
            diceTracking.transform.position = initialPosition;
            diceTracking.transform.SetParent(diceTransform);
        }
        rooms = GameObject.FindGameObjectsWithTag ("room");
    }


    void FixedUpdate()
    {

        // Debug.Log(_characterController.isGrounded);
        if (!_characterController.isGrounded)
        {
            moveDirection.y -= (speed * (gravity * Time.deltaTime));
            _characterController.Move(moveDirection * Time.deltaTime);
        }
       
        if (movePlayer)
        {
            
                if (moveOnXAxis)
                {
                    // Debug.Log("diceTracking.transform.position" + diceTracking.transform.position);
                    // Debug.Log("differencePoint"+(pointToMove - diceTracking.transform.position).magnitude);
                    _characterController.Move(moveDirection * Time.deltaTime * speed);
                    if ((pointToMove - diceTracking.transform.position).magnitude < .7f)
                    {
                        moveOnXAxis = false;
                        functionRequierdToMoveDice();

                    }
                }
         
        }
        
    }
    void findInitialPosition()
    {
        if (Physics.Raycast(diceTransform.position, -Vector3.up, out hit))
        {

            initialPosition = hit.collider.gameObject.transform.position;
        }
    }
    public void OnClick()
    {
        rollTheDice();
         functionRequierdToMoveDice();
        movePlayer = true;
    
    }
  
    void checkRoomProperty()
    {

        if (Physics.Raycast(diceTransform.position, -Vector3.up, out hit))
        {
            if (hit.collider.tag == "hole")
            {
                Debug.Log("HOLE");
            }
            else if (hit.collider.tag == "elevator")
            {
				makeBallGoUp();

                Debug.Log("elevator");
            }
            else if (hit.collider.tag == "power")
            {
                Debug.Log("power");
            }
            else if (hit.collider.tag == "teleportation")
            {
                Debug.Log("teleportation");
                counter = teleportationValue;
            }
        }
    }
	void makeBallGoUp(){
        findInitialPosition();
        floorClimbed++;
        changeFloor(initialPosition);
      Debug.Log(pointToMove);
     // counter = 23;
     // functionRequierdToMoveDice();

	}
    void functionRequierdToMoveDice()
    {
        // Debug.Log("hellow");
        counter--;
        if (counter >= 0)
        {

			hideotherfloor();
			Debug.Log ("floor Hidden");
            //  Debug.Log("claaedhellow");
            makeDirectionNormal();
            findInitialPosition();
            findDirectionalVectorAndMove();
           // moveOneStep();
        }
        else
        {
            //Debug.Log("checkRoomProperty");
            checkRoomProperty();
        }
        Debug.Log(counter);

    }
   
	
    void makeDirectionNormal(){
       
          if (_direction == direction.FORWARD)
        {
           // Debug.Log("hererere");
            if (_prevDirection == direction.RIGHT)
            {
                _direction = direction.LEFT;
                directionalVector = Vector3.left;
                _prevDirection = direction.FORWARD;
            }
            else if (_prevDirection == direction.LEFT)
            {
                _direction = direction.RIGHT;
                directionalVector = Vector3.right;
                _prevDirection = direction.FORWARD;
            }
        }
		else if(_direction==direction.UP){
		if(_prevDirection==direction.RIGHT){
				_direction = direction.RIGHT;
                directionalVector = Vector3.right;
                _prevDirection = direction.FORWARD;
		}
		else if(_prevDirection==direction.LEFT){
				_direction = direction.LEFT;
                directionalVector = Vector3.left;
                _prevDirection = direction.FORWARD;
		}
        }
        else if (_direction == direction.DOWN)
        {
            directionalVector = Vector3.right;
            _direction = direction.RIGHT;
            _prevDirection = direction.RIGHT;
        }
		
          Debug.Log(_direction);
    }
    
    

    void findDirectionalVectorAndMove()
    {
        roomNumber++;
        if(roomNumber%(noOfRows*noOfColumns)==0)
        {
            floorClimbed++;
            changeFloor(initialRoomPosition);
            findInitialPosition();
            counter--;
            if (counter <= 0)
            {
                return;
            }
           // findInitialPosition();
            
        }
        else if (roomNumber % noOfRows == 0)
        {
          //  Debug.Log("change directions");
     
            changeDirection();
        }
        
       
        //Debug.Log("_direction"+_direction);
      //  Debug.Log("_prevDirection" + _prevDirection);
        
        setDestination();
        moveOneStep();

    }
    void changeFloor(Vector3 roomPosition)
    {
       // Debug.Log("change floor");
        diceTransform.position = roomPosition + new Vector3(0, floorClimbed * roomGap + roomSize, 0);
        findInitialPosition();
        //making ball to fall on floor
        pointToMove = initialPosition;
        _direction = direction.DOWN;
        directionalVector = Vector3.down;
        counter--;
        moveOneStep();
        

    }
   
    void changeDirection()
    {
        if (_direction == direction.RIGHT)
        {
            directionalVector = Vector3.forward;
            _direction = direction.FORWARD;
            _prevDirection = direction.RIGHT;
        }
        else if (_direction == direction.FORWARD)
        {
            if (_prevDirection == direction.RIGHT)
            {

                directionalVector = Vector3.left;
                _direction = direction.LEFT;
                _prevDirection = direction.FORWARD;
            }
            if (_prevDirection == direction.LEFT)
            {
                directionalVector = Vector3.right;
                _direction = direction.LEFT;
                _prevDirection = direction.FORWARD;
            }
        }
        else if (_direction == direction.LEFT)
        {
            directionalVector = Vector3.forward;
            _direction = direction.FORWARD;
            _prevDirection = direction.LEFT;
        }
       
        else
        {
            Debug.Log("umdefined case");
        }
        
    }

   
    void setDestination()
    {
        Debug.Log(directionalVector);
        pointToMove = initialPosition + directionalVector * roomSize;
        Debug.Log("pointToMove" +pointToMove);
    }
    
    void moveOneStep()
    {
        moveDirection = directionalVector;
        moveOnXAxis = true;
        Debug.Log("moved");
    }
    
    void rollTheDice()
    {
        int diceNumber = Random.Range(1, 7);
        if (diceNumber == 1)
        {
            counter = 1;
            _diceFaceNumber = diceFaceNumber.ONE;
        }
        else if (diceNumber == 2)
        {
            counter = 2;
            _diceFaceNumber = diceFaceNumber.TWO;

        }
        else if (diceNumber == 3)
        {
            counter = 3;
            _diceFaceNumber = diceFaceNumber.THREE;

        }
        else if (diceNumber == 4)
        {
            counter = 4;
            _diceFaceNumber = diceFaceNumber.FOUR;
        }
        else if (diceNumber == 5)
        {
            counter = 5;
            _diceFaceNumber = diceFaceNumber.FIVE;
        }
        else
        {
            counter = 6;
            _diceFaceNumber = diceFaceNumber.SIX;
        }
        if (_diceFaceNumber == _prevDiceFaceNumber)
        {
            _prevDiceFaceNumber = _diceFaceNumber;
        }
		counter=1;
        Debug.Log(_diceFaceNumber);
    }
    void runTimeInstantiateAndInitializing()
    {
        _diceFaceNumber = new diceFaceNumber();
        _direction = new direction();
        _prevDirection = new direction();
        _prevDiceFaceNumber = new diceFaceNumber();
        _diceFaceNumber = diceFaceNumber.NONE;
        _prevDiceFaceNumber = diceFaceNumber.NONE;
        diceTransform = this.transform;
        gravity = 10;
        moveDirection = Vector3.right;
        moveDirection *= speed;
        _characterController = GetComponent<CharacterController>();
        roomSize = genericBuilding.roomSize;
        noOfRows = genericBuilding.rowsOfRoom;
        noOfColumns = genericBuilding.columnsOfRoom;
        roomGap = genericBuilding.floorGap;
        moveOnXAxis = false;
        speed = genericBuilding.speed;
        roomNumber = 0;
        floorClimbed = 0;
        directionalVector = Vector3.right;
        _direction = direction.RIGHT;
        _prevDirection = direction.RIGHT;
        movePlayer = false;
	
    }
      void hideotherfloor(){

		Debug.Log ("Room Number:  " + roomNumber);
		//need to be called in every steps, even while eaten or just fucked up


		//first show all floor need to be changed later
		foreach (var b in rooms) {

			Renderer[] renderers = b.GetComponentsInChildren<Renderer> ();
			foreach (var r in renderers) {
				// change something in renderer...
				r.enabled = true; //enabling alll the rooms, i.e showing them all

			}

		}
		switch ((roomNumber+1) / 25) {

		case 0:
			foreach (var b in rooms) {
				if (b.name != "0floor") {
					Renderer[] renderers = b.GetComponentsInChildren<Renderer> ();
					foreach (var r in renderers) {
						// change something in renderer...
						r.enabled = false; // like disable it for example. 
					}
				}
			}
			break;
		case 1:
			foreach (var b in rooms) {
				if (b.name != "1floor") {
					Renderer[] renderers = b.GetComponentsInChildren<Renderer> ();
					foreach (var r in renderers) {
						// change something in renderer...
						r.enabled = false; // like disable it for example. 
					}
				}
			}
			break;
		case 2:
			foreach (var b in rooms) {
				if (b.name == "3floor") {
					Renderer[] renderers = b.GetComponentsInChildren<Renderer> ();
					foreach (var r in renderers) {
						// change something in renderer...
						r.enabled = false; // like disable it for example. 
					}
				}
			}
			break;
		case 3:
			foreach (var b in rooms) {
				if (b.name == "4floor") {
					Renderer[] renderers = b.GetComponentsInChildren<Renderer> ();
					foreach (var r in renderers) {
						// change something in renderer...
						r.enabled = false; // like disable it for example. 
					}
				}
			}
			break;
//		case 4:
//			foreach (var b in rooms) {
//				if (b.name == "5floor") {
//					Renderer[] renderers = b.GetComponentsInChildren<Renderer> ();
//					foreach (var r in renderers) {
//						// change something in renderer...
//						r.enabled = false; // like disable it for example. 
//					}
//				}
//			}
//			break;
		default:
			break;

		}
	}

}

using UnityEngine;


public class dice : MonoBehaviour
{
 
    enum direction
    {
        RIGHT,FORWARD,LEFT,UP,DOWN
    }
    enum playerState
    {
        POWER,NON_POWER
    }
    direction _direction,_prevDirection;
    playerState _playerState;
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
    int powerEffectTurn;
    public int teleportationValue;
     GameObject particleEffect;
    void Start()
    {
        runTimeInstantiateAndInitializing();
        gameManager.Notifications.AddListener(notificationManager.EVENT_TYPE.MOVE_PLAYER, movePlayerWithDiceNumber);
      //  particleEffect.transform.position = diceTransform.position;
        //particleEffect.transform.SetParent(diceTransform);
        //particleEffect.SetActive(true);
        if (Physics.Raycast(diceTransform.position, -Vector3.up, out hit))
        {
            initialPosition = hit.collider.gameObject.transform.position;
            initialRoomPosition = hit.collider.gameObject.transform.position;
			diceTransform.position= hit.collider.gameObject.transform.position+new Vector3(0,roomSize/2,0);
          //  Debug.Log(initialPosition);
             diceTracking = new GameObject("diceTracking");
            diceTracking.transform.position = initialPosition;
            diceTracking.transform.SetParent(diceTransform);
        }
    }


    void FixedUpdate()
    {

        // Debug.Log(_characterController.isGrounded);
        if (!_characterController.isGrounded)
        {
            moveDirection.y -= (speed * (gravity * Time.deltaTime));
            _characterController.Move(moveDirection * Time.deltaTime);
        }
        if (counter < 0)
        {
            diceRoller._inputState = gameManager.inputState.inputOn;
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
        try
        {
            if (Physics.Raycast(diceTransform.position, -Vector3.up, out hit))
            {

                initialPosition = hit.collider.gameObject.transform.position;
            }
        }
        catch
        {
            Debug.Log("Asddddddddddddddddd");
        }
    }
    public void OnClick()
    {

        if (powerEffectTurn <= 0)
        {
            _playerState = playerState.NON_POWER;
        }
        else
        {
            _playerState=playerState.POWER;
            powerEffectTurn--;
        }
            //rollTheDice();
            functionRequierdToMoveDice();
            movePlayer = true;
           // particleEffect.SetActive(true);
            Debug.Log(powerEffectTurn);
            Debug.Log(_playerState);
    }
    void movePlayerWithDiceNumber(int steps)
    {
        counter = steps;
        OnClick();
  }
    void checkRoomProperty()
    {

        if (Physics.Raycast(diceTransform.position, -Vector3.up, out hit))
        {
            if (hit.collider.tag == "hole")
            {
                if (_playerState == playerState.NON_POWER)
                {
                    Debug.Log("HOLE");

                    gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.PLAY_SOUND, 1);
                    makeBallGoDown();
                }
            }
            else if (hit.collider.tag == "elevator")
            {
				makeBallGoUp();
                gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.PLAY_SOUND, 0);
                Debug.Log("elevator");
            }
            else if (hit.collider.tag == "power")
            {
                Debug.Log("power");
                powerEffectTurn = 2;
                gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.PLAY_SOUND, 2);
                particleEffect.SetActive(true);
            }
            else if (hit.collider.tag == "teleportation")
            {
                Debug.Log("teleportation");
                particleEffect.SetActive(true);
                teleportationValue = Random.Range(6, 10);
                counter = teleportationValue;
                functionRequierdToMoveDice();
            }
        }
    }
	void makeBallGoUp(){
        findInitialPosition();
        floorClimbed++;
        gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.TRANS_FLOOR_ON, floorClimbed);

        changeFloor(initialPosition);
     // Debug.Log(pointToMove);
     // counter = 23;
     // functionRequierdToMoveDice();

	}
    void makeBallGoDown()
    {
        findInitialPosition();
       
       

        changeFloorDown(initialPosition);
        floorClimbed--;
        gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.TRANS_FLOOR_ON, floorClimbed);
       
    }
    void functionRequierdToMoveDice()
    {
        // Debug.Log("hellow");
        counter--;
        if (counter >= 0)
        {
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
        //Debug.Log(counter);

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
            if (_prevDirection == direction.LEFT)
            {
                directionalVector = Vector3.left;
                _direction = direction.LEFT;
                _prevDirection = direction.LEFT;
            }
            else if (_prevDirection == direction.RIGHT)
            {
                directionalVector = Vector3.right;
                _direction = direction.RIGHT;
                _prevDirection = direction.RIGHT;

            }
          
        }
		
        //  Debug.Log(_direction);
    }
    
    

    void findDirectionalVectorAndMove()
    {
        roomNumber++;
        if(roomNumber%(noOfRows*noOfColumns)==0)
        {
            floorClimbed++;
            //make that floor visible
            //other invisible
            gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.TRANS_FLOOR_ON, floorClimbed);
            findInitialPosition();
            changeFloor(initialRoomPosition);
            findInitialPosition();
            counter--;
            if (counter < 0)
            {
                Debug.Log("adsssssssssssssssssssssss");
                return;
            }
            roomNumber++;
            makeDirectionNormal();
           // findInitialPosition();
            
        }
        else if (roomNumber % noOfRows == 0)
        {
          //  Debug.Log("change directions");
     
            changeDirection();
        }
        
       
        //Debug.Log("_direction"+_direction);
      //  Debug.Log("_prevDirection" + _prevDirection);
     //   Debug.Log("roomNumber" + roomNumber);
        setDestination();
        moveOneStep();

    }
    void changeFloor(Vector3 roomPosition)
    {
        Debug.Log("change floor");
        Debug.Log(floorClimbed);
        diceTransform.position = roomPosition + new Vector3(0, floorClimbed* roomGap + roomSize, 0);
        findInitialPosition();
        //making ball to fall on floor
        pointToMove = initialPosition;
        _prevDirection = _direction;
        _direction = direction.DOWN;
        directionalVector = Vector3.down;
        counter--;
        moveOneStep();
        

    }
    void changeFloorDown(Vector3 roomPosition)
    {
        Debug.Log("change floor");
        Debug.Log(floorClimbed);
        diceTransform.position = roomPosition -new Vector3(0, floorClimbed * roomGap - roomSize, 0);
        findInitialPosition();
        //making ball to fall on floor
        pointToMove = initialPosition;
        _prevDirection = _direction;
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
       // Debug.Log(directionalVector);
        pointToMove = initialPosition + directionalVector * roomSize;
       // Debug.Log("pointToMove" +pointToMove);
    }
    
    void moveOneStep()
    {
        moveDirection = directionalVector;
        moveOnXAxis = true;
      //  Debug.Log("moved");
    }
    
   
    void runTimeInstantiateAndInitializing()
    {
      
        _direction = new direction();
        _prevDirection = new direction();
        _playerState = playerState.NON_POWER;
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
        particleEffect = diceTransform.GetChild(0).transform.gameObject;
      
        particleEffect.SetActive(false);
        powerEffectTurn = 0;
        
	
    }

}

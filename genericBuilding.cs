using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class genericBuilding : MonoBehaviour {
    class roomProperties
    {
        public static int roomNumber = 0;
        public float row, column, height;
         public  roomProperties(float _row, float _column, float _height)
         {
             row = _row;
             column = _column;
             height = _height;
           
         }
      
    };
    enum ROOMPROPERTIES
    {
        ELEVATOR,HOLE,POWER,TELEPORTATION
    }

    ROOMPROPERTIES _ROOMPROPERTIES, _PREVROOMPROPERTIES;
    roomProperties _roomProperties;
   
    public static float rowsOfRoom;
    
    public static float columnsOfRoom;
   
   public static int roomSize;

   public static int roomGap;
  
   public static int speed;

   public GameObject roomWithNoNumber;
    [SerializeField]
    int floorOfBuilding;
    [SerializeField]
    GameObject roomOfBuilding;
    [SerializeField]
    public static int floorGap;
    [SerializeField]
    private GameObject diceRoll;
    const int FONT_SIZE = 12;
    const float TOTAL_ELEVATOR_ROOM_PRECENTAGE = .2f;
    const float TOTAL_HOLE_ROM_PERCENTAGE = .1f;
    const string elevatorRoom = "ELEVATOR_ROOM";
    GameObject building;
    Queue<GameObject> roomsStore = new Queue<GameObject>();
    TextMesh roomNumberText;
    Dictionary<ROOMPROPERTIES, List<int>> roomSpecialProperties = new Dictionary<ROOMPROPERTIES, List<int>>();
    GameObject floor;
    GameObject floorTransparent;
         GameObject floorTransColl ;
         GameObject floorColl;
	// Use this for initialization
	void Start () {
      
        roomSize = 2;
        rowsOfRoom = 5;
        columnsOfRoom = 5;
        floorGap =5;
        speed = 5;
      
        createRunTimeGameObjects();
        createRoomSpecialProperties();
        setBuildingAndRoomProperties();
        makeBuilding();
        Invoke("makeClearSceneBuilding", 2f);
        
	}
    void makeClearSceneBuilding()
    {
        gameManager.Notifications.postNotification(notificationManager.EVENT_TYPE.TRANS_FLOOR_ON, 0);
       

    }
    void createRunTimeGameObjects()
    {
        building = new GameObject("building" + rowsOfRoom + "*" + columnsOfRoom+"*"+floorOfBuilding);

    }
    void createRoomSpecialProperties()
    {
        _ROOMPROPERTIES = new ROOMPROPERTIES();
        _PREVROOMPROPERTIES = new ROOMPROPERTIES();
        float totalNoOfRooms = rowsOfRoom * columnsOfRoom * floorOfBuilding;
        float counter = 0;
        //putting nothing properties in 1st row
        counter += rowsOfRoom;
        //choose property to be aaplied
        if (rowsOfRoom == 5 && columnsOfRoom == 5 && floorOfBuilding == 5)
        {
            //first flooor
          //  makeHole();
            makeElevator(8);

            makeTeleportation(18);
            makeElevator(20);
            makeTeleportation(5);

          //  makeElevator(4);
           // makeElevator(5);
          //  makeElevator(6);
           // makeElevator(7);
           // makeElevator(8);
            

         //   makePower();

            //second floor
            makeElevator(43);

            makeTeleportation(34);

            makePower(28);

           // makeHole(31);
           // makeHole(38);
            makeHole(49);

            //third floor
            makeHole(54);
            makePower(56);
            makeHole(60);
            makeTeleportation(63);
            makeElevator(64);
            makeElevator(70);
            makeHole(72);

            //fourth floor
            makeHole(77);
            makeElevator(78);
            makePower(79);
            makeHole(82);
            makeTeleportation(87);
            makeElevator(95);

            //fifth floor
            makePower(102);
            makeHole(106);
            makeTeleportation(108);
            makePower(110);
            makeHole(113);
            makeHole(124);
 
        }
        else
        {
            while (counter < totalNoOfRooms)
            {
                chooseRoomProperty();
                counter = addEspecificProperty(_ROOMPROPERTIES, counter);
                // Debug.Log(counter);
                counter += Random.Range(3, 5);
                // Debug.Log("After adding"+counter);
            }
        }
    }
    void makeElevator(int roomNuber)
    {
        _ROOMPROPERTIES = ROOMPROPERTIES.ELEVATOR;
        addEspecificProperty(_ROOMPROPERTIES, roomNuber);
    }
    void makeHole(int roomNuber)
    {
        _ROOMPROPERTIES = ROOMPROPERTIES.HOLE;
        addEspecificProperty(_ROOMPROPERTIES, roomNuber);
    }
    void makeTeleportation(int roomNuber)
    {
     
         _ROOMPROPERTIES= ROOMPROPERTIES.TELEPORTATION;
         addEspecificProperty(_ROOMPROPERTIES, roomNuber);
    }
    void makePower(int roomNuber)
    {
        _ROOMPROPERTIES = ROOMPROPERTIES.POWER;
        addEspecificProperty(_ROOMPROPERTIES, roomNuber);
    }
   
    void  chooseRoomProperty()
    {

        int random = Random.Range(0, 4);
        if (random == 0)
        {
            _ROOMPROPERTIES= ROOMPROPERTIES.ELEVATOR;
        }
        else if (random == 1)
        {
            _ROOMPROPERTIES= ROOMPROPERTIES.HOLE;
        }
        else
        {
            _ROOMPROPERTIES= ROOMPROPERTIES.POWER;
        }
        if (_PREVROOMPROPERTIES == _ROOMPROPERTIES)
        {
            //same consequetively
            chooseRoomProperty();
        }
        else
        {
            _PREVROOMPROPERTIES = _ROOMPROPERTIES;
        }

    }
    int addEspecificProperty(ROOMPROPERTIES Rp, float roomCounter)
    {
        int roomNumber = Random.Range((int)roomCounter, (int)(roomCounter + rowsOfRoom));
        if (!roomSpecialProperties.ContainsKey(Rp))
        {
            roomSpecialProperties.Add(Rp, new List<int>());
        }
        roomSpecialProperties[Rp].Add(roomNumber);
 

        return roomNumber;

    }
     void addEspecificProperty(ROOMPROPERTIES Rp, int roomCounter)
    {
        if (!roomSpecialProperties.ContainsKey(Rp))
        {
            roomSpecialProperties.Add(Rp, new List<int>());
        }
        roomSpecialProperties[Rp].Add(roomCounter);


      

    }
    void setBuildingAndRoomProperties()
    {
       
        roomOfBuilding.transform.localScale = new Vector3(roomSize, roomSize, roomSize);
       
      
    }
   
    
    void makeBuilding(){
         floorTransColl = new GameObject("floorTransColl");
         floorColl = new GameObject("floorColl");
        floorColl.transform.SetParent(building.transform);
        floorTransColl.transform.SetParent(building.transform);
       int rowNumber = 0;
        for (int height = 0; height < floorOfBuilding; height++)
        {
            

            floorTransparent =new GameObject("floorTransparent"+height);
            floorTransparent.transform.SetParent(floorTransColl.transform);
             floor = new GameObject("floorNONTRANS" + height);
             floor.transform.SetParent(floorColl.transform);
            for (int col = 0; col < columnsOfRoom; col++)
            {
                if (col % 2 != 0)
                {


                    for (int row = (int)(rowsOfRoom-1); row >= 0; row--)
                    {
                        roomProperties.roomNumber++;

                        GameObject tempStorage = Instantiate(roomOfBuilding);
                        GameObject tempStorage1 = Instantiate(roomWithNoNumber);
                        
                        
                        _roomProperties = new roomProperties(row, col, height);

                        numberAndPositionTheRoom(tempStorage, _roomProperties,floor.transform);
                        numberAndPositionTheRoom(tempStorage1, _roomProperties, floorTransparent.transform);
                        checkAndSetEachRoomProperty(tempStorage);
                        if (roomProperties.roomNumber == 1)
                        {
                            //put dice
                            Debug.Log("sdadasda");
                            diceRoll = Instantiate(diceRoll, tempStorage.transform.position + new Vector3(0, roomSize, 0), tempStorage.transform.rotation);

                        }
                        roomsStore.Enqueue(tempStorage);
                    }
                }
                
                
                else
                {
                    for (int row = 0; row < rowsOfRoom; row++)
                    {
                        roomProperties.roomNumber++;

                        GameObject tempStorage = Instantiate(roomOfBuilding);
                        GameObject tempStorage1 = Instantiate(roomWithNoNumber);
                       
                        _roomProperties = new roomProperties(row, col, height);

                        numberAndPositionTheRoom(tempStorage, _roomProperties,floor.transform);
                   
                        numberAndPositionTheRoom(tempStorage1, _roomProperties, floorTransparent.transform);

                        checkAndSetEachRoomProperty(tempStorage);
                        // Debug.Log(roomProperties.roomNumber);
                        if (roomProperties.roomNumber == 1)
                        {
                            //put dice
                         //   Debug.Log("sdadasda");
                            diceRoll = Instantiate(diceRoll, tempStorage.transform.position + new Vector3(0, roomSize, 0), tempStorage.transform.rotation);

                        }
                        // tempStorage.SetActive(false);
                        roomsStore.Enqueue(tempStorage);
                    }
                }
                rowNumber++;
            }
            rowNumber = 0;
        }
        floorTransColl.AddComponent<transFloor>();
        floorColl.AddComponent<floorColl>();
    }
    void numberAndPositionTheRoom(GameObject room, roomProperties properties,Transform parent)
    {
        roomNumberText = room.GetComponentInChildren<TextMesh>();
        if (roomNumberText != null)
        {
            roomNumberText.fontSize = FONT_SIZE;
            roomNumberText.text = "" + roomProperties.roomNumber;
        }
        room.name = properties.row + "*" + properties.column + "*" + properties.height + "room";
        room.transform.position = new Vector3(properties.row * roomSize, floorGap*(properties.height+1) /*+ (properties.height * roomSize * 2)*/, properties.column * roomSize);
        room.transform.SetParent(parent);
    }
   
    void checkAndSetEachRoomProperty(GameObject room)
    {
        foreach (ROOMPROPERTIES _roomProperty in roomSpecialProperties.Keys)
        {
            if (roomSpecialProperties[_roomProperty].Contains(roomProperties.roomNumber))
            {
                assignScriptAccordingToProperty(_roomProperty,room);
            }
           
        }
    }
   
    void assignScriptAccordingToProperty(ROOMPROPERTIES _roomProperty,GameObject room)
    {
        switch (_roomProperty)
        {
            case ROOMPROPERTIES.ELEVATOR:
                {
                    room.tag = "elevator";
                    room.AddComponent<elevatorRoom>();
                    break;
                }
            case ROOMPROPERTIES.HOLE:
                {
                    room.tag = "hole";
                    room.AddComponent<holeRoom>();
                    break;
                }
            case ROOMPROPERTIES.POWER:
                {
                    room.tag = "power";
                    room.AddComponent<powerRoom>();
                    break;
                }
            case ROOMPROPERTIES.TELEPORTATION:
                {
                    room.tag = "teleportation";
                    room.AddComponent<teleportationRoom>();
                    break;

                }
        }
    }
    public void onClick()
    {
        dice _dice = diceRoll.GetComponent<dice>();
        _dice.OnClick();
    }

}

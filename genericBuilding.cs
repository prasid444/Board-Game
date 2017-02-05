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
        ELEVATOR,HOLE,POWER
    }

    ROOMPROPERTIES _ROOMPROPERTIES, _PREVROOMPROPERTIES;
    roomProperties _roomProperties;
   
    public static float rowsOfRoom;
    
    public static float columnsOfRoom;
   
   public static int roomSize;
   public static int roomGap;
   public static int speed;
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
	// Use this for initialization
	void Start () {
        roomSize = 2;
        rowsOfRoom = 5;
        columnsOfRoom = 5;
        floorGap = 5;
        speed = 5;
        createRunTimeGameObjects();
        createRoomSpecialProperties();
        setBuildingAndRoomProperties();
        makeBuilding();
        
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
        while (counter < totalNoOfRooms)
        {
            chooseRoomProperty();
            counter = addEspecificProperty(_ROOMPROPERTIES, counter);
           // Debug.Log(counter);
            counter += Random.Range(3,5);
           // Debug.Log("After adding"+counter);
        }
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
    void setBuildingAndRoomProperties()
    {
       
        roomOfBuilding.transform.localScale = new Vector3(roomSize, roomSize, roomSize);
       
      
    }
   
    
    void makeBuilding(){
       int rowNumber = 0;
        for (int height = 0; height < floorOfBuilding; height++)
        {
            for (int col = 0; col < columnsOfRoom; col++)
            {
                if (col % 2 != 0)
                {

                    for (int row = (int)(rowsOfRoom-1); row >= 0; row--)
                    {
                        roomProperties.roomNumber++;

                        GameObject tempStorage = Instantiate(roomOfBuilding);

                        
                        
                        _roomProperties = new roomProperties(row, col, height);

                        numberAndPositionTheRoom(tempStorage, _roomProperties);

                        checkAndSetEachRoomProperty(tempStorage);
                        // Debug.Log(roomProperties.roomNumber);
                        if (roomProperties.roomNumber == 1)
                        {
                            //put dice
                            Debug.Log("sdadasda");
                            diceRoll = Instantiate(diceRoll, tempStorage.transform.position + new Vector3(0, roomSize, 0), tempStorage.transform.rotation);

                        }
                        // tempStorage.SetActive(false);
                        roomsStore.Enqueue(tempStorage);
                    }
                }
                
                
                else
                {
                    for (int row = 0; row < rowsOfRoom; row++)
                    {
                        roomProperties.roomNumber++;

                        GameObject tempStorage = Instantiate(roomOfBuilding);

                       
                        _roomProperties = new roomProperties(row, col, height);

                        numberAndPositionTheRoom(tempStorage, _roomProperties);

                        checkAndSetEachRoomProperty(tempStorage);
                        // Debug.Log(roomProperties.roomNumber);
                        if (roomProperties.roomNumber == 1)
                        {
                            //put dice
                            Debug.Log("sdadasda");
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
    }
    void numberAndPositionTheRoom(GameObject room, roomProperties properties)
    {
        roomNumberText = room.GetComponentInChildren<TextMesh>();
        roomNumberText.fontSize = FONT_SIZE;
        roomNumberText.text = "" + roomProperties.roomNumber;
        //room.name = properties.row + "*" + properties.column + "*" + properties.height + "room";
		room.name=properties.height+"floor";
        room.transform.position = new Vector3(properties.row * roomSize, floorGap + (properties.height * roomSize * 2), properties.column * roomSize);
        room.transform.SetParent(building.transform);
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
        }
    }
    public void onClick()
    {
        dice _dice = diceRoll.GetComponent<dice>();
        _dice.OnClick();
    }

}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static DoorScript;

public class RoomScript : MonoBehaviour
{
    private PolygonCollider2D roomCollider;
    public GameObject virtualCameraParent;
    public CinemachineConfiner2D virtualCameraConfiner;
    private GameObject player;
    private RoomControllerScript parentRoomController;

    public bool activeRoom = false;

    public int width;
    public int height;

    public int X;
    public int Y;


    public DoorScript leftDoor;
    public DoorScript rightDoor;
    public DoorScript topDoor;
    public DoorScript bottomDoor;

    public List<DoorScript> doors = new List<DoorScript>();

    public EnemyScript[] enemies = null;
    public int numOfEnemies = 0;

    private bool updatedDoors = false;

    public bool isBossRoom = false;

    public RoomScript(int x, int y) {
        X = x;
        Y = y;
    }
    // Start is called before the first frame update
    void Start()
    {


        if (RoomControllerScript.instance == null)
        {
            Debug.Log("Room controller not found. Pressed play in wrong scene");
            return;
        }

        roomCollider = GetComponentInChildren<PolygonCollider2D>();

        player = GameObject.FindWithTag(TagsCons.playerTag);

        virtualCameraParent.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;

        virtualCameraParent.SetActive(false);

        parentRoomController = FindObjectOfType<RoomControllerScript>();

        RoomControllerScript.instance.RegisterRoom(this);

        DoorScript[] ds = GetComponentsInChildren<DoorScript>();

        foreach (DoorScript door in ds)
        {
            doors.Add(door);
            switch (door.doorType)
            {
                case DoorScript.DoorType.right:
                    rightDoor = door; break;
                case DoorScript.DoorType.left:
                    leftDoor = door; break;
                case DoorScript.DoorType.up:
                    topDoor = door; break;
                case DoorScript.DoorType.down:
                    bottomDoor = door; break;
                default:
                    break;
            }
        }

        






    }

    public void StartGameMain()
    {
                if (GetRoomCenter().y == 0 && GetRoomCenter().x == 0)
        {
            virtualCameraParent.SetActive(true);
            parentRoomController.currentRoom = this;
            activeRoom = true;
        }
    }

    private void Update()
    {
        if(name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }

        if (activeRoom)
        {
            // Check if enemies array is not null before accessing its length
            if (enemies != null)
            {
                numOfEnemies = enemies.Length;
            }
            else
            {
                // If enemies array is null, set numOfEnemies to 0
                numOfEnemies = 0;
            }

            
            foreach (DoorScript door in doors)
            {
                
                if (numOfEnemies > 0)
                {
                    door.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    door.GetComponentInChildren<SpriteRenderer>().sortingOrder = 10;
                }
                else
                {
                    door.GetComponentInChildren<SpriteRenderer>().color = Color.black;
                    door.GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;
                }
            }

            

        }

        if (isBossRoom && numOfEnemies <= 0)
        {
            print("game over");
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (DoorScript door in doors)
        {

            switch (door.doorType)
            {
                
                case DoorScript.DoorType.right:

                    if (GetRight() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case DoorScript.DoorType.left:
                    if (GetLeft() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case DoorScript.DoorType.up:

                    if (GetTop() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case DoorScript.DoorType.down:

                    if (GetBottom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public RoomScript GetRight()
    {
        if (RoomControllerScript.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomControllerScript.instance.FindRoom(X + 1, Y);
        }

        return null;
    }

    public RoomScript GetLeft()
    {
        if (RoomControllerScript.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomControllerScript.instance.FindRoom(X - 1, Y);
        }

        return null;
    }

    public RoomScript GetTop()
    {
        if (RoomControllerScript.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomControllerScript.instance.FindRoom(X, Y + 1);
        }

        return null;
    }

    public RoomScript GetBottom()
    {
        if (RoomControllerScript.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomControllerScript.instance.FindRoom(X, Y - 1);
        }

        return null;
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3( X * width, Y * height, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public void DoorCollisionDetected(DoorScript doorScript, Collider2D collision)
    {
        Debug.Log("door collided " + doorScript.doorType);
        RoomScript newRoom = null;
        if (activeRoom && numOfEnemies <= 0)
        {
            
            virtualCameraParent.SetActive(false);
            activeRoom = false;

            switch (doorScript.doorType)
            {
                case DoorType.left:
                    newRoom = GetLeft();
                    break;
                case DoorType.right:
                    newRoom = GetRight();
                    break;
                case DoorType.up:
                    newRoom = GetTop();
                    break;      
                case DoorType.down:
                    newRoom = GetBottom();
                    break;
                default:
                    break;

            }

            parentRoomController.currentRoom = newRoom; 
            newRoom.TurnOnCamera();
            collision.gameObject.GetComponentInParent<PlayerControllerScript>().roomScript = newRoom;
            collision.gameObject.GetComponentInParent<PlayerControllerScript>().SetCenterOfRoom(newRoom.GetRoomCenter(), newRoom);
        }

       
    }


    public void TurnOnCamera()
    {
        virtualCameraParent.SetActive(true);
    }

    public void TurnOffCamera()
    {
        virtualCameraParent.SetActive(false);
    }
}

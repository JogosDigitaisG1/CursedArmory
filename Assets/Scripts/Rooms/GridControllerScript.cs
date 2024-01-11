using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControllerScript : MonoBehaviour
{
    public RoomScript room;

    [System.Serializable]
    public struct Grid
    {
        public int cols, rows;
        public float verticalOffset, horizontalOffset;
    }

    public Grid grid;

    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();

    private void Awake()
    {
        room = GetComponentInParent<RoomScript>();
        grid.cols = room.width - 2; 
        grid.rows = room.height - 2;
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        for (int y = 0; y < grid.rows; y++) { 
            for (int x = 0; x < grid.cols; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = 
                    new Vector2(x - (grid.cols - grid.horizontalOffset), 
                    y - (grid.rows - grid.verticalOffset));

                go.name = "X: " + x + ", Y: " + y;
                availablePoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }

        if (transform.parent.TryGetComponent<ObjectRoomSpawner>(out ObjectRoomSpawner objectRoomSpawner))
        {
            objectRoomSpawner.InitializeObjectSpawning ();
        }
    }
}

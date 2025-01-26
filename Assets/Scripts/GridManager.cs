using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Grid grid;
    public Grid tileGrid;
    public Tilemap tilemap;

    [Header ("Environment Tiling")]
    public Tile floorTile;
    public Tile wallTile;
    public Tile normalDoorTile;
    public Tile RedDoorTile;
    public Tile GreenDoorTile;
    public Tile BlueDoorTile;


    [Header ("Initial Settings")]
    public GameObject[,] rooms;

    public TextAsset level;
    public GameObject roomPrefab;

    public Player player;
    public Vector2Int startingRoom;

    public Key redKey;
    public GameObject redKeyUI;
    public Vector2Int redStart = new Vector2Int {x = 2, y = 3};
    public Key greenKey;
    public GameObject greenKeyUI;
    public Vector2Int greenStart = new Vector2Int {x = 6, y = 1};
    public Key blueKey;
    public GameObject blueKeyUI;
    public Vector2Int blueStart = new Vector2Int {x = 4, y = 4};

    public static GridManager instance {get; private set;}

    public GameObject smokeFX;
    
    private int width = 7;
    private int height = 7;
    private Vector2Int currentGrid;

    private void Awake(){
        if(instance != null && instance != this){
            Destroy(this);
        }
        else{
            instance = this;
        }
    }

    void Start()
    {
        LoadLevel();
        GenerateGrid();
    }

    void Update()
    {
        
    }

    public RoomBehaviour GetPlayerRoom()
    {
        int x = player.x;
        int y = player.y;

        return rooms[x,y].GetComponent<RoomBehaviour>();
    }

    private void GenerateGrid(){
        MovePlayer(startingRoom.x, startingRoom.y);
        MoveKey(redStart.x, redStart.y, "RedDoor");
        MoveKey(greenStart.x, greenStart.y, "GreenDoor");
        MoveKey(blueStart.x, blueStart.y, "BlueDoor");

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                
                if(!rooms[i, j]){
                    continue;
                }

                Room room = rooms[i, j].GetComponent<RoomBehaviour>().room;

                Vector3Int cell = GridtoTileCell(new Vector3Int(i,j,0));

                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 7; y++)
                    {
                        Vector3Int cell_ = cell;
                        cell_.x += x;
                        cell_.y += y;

                        SetTile(cell_, wallTile);
                    }
                }

                for (int x = 1; x < 6; x++)
                {
                    for (int y = 1; y < 6; y++)
                    {
                        Vector3Int cell_ = cell;
                        cell_.x += x;
                        cell_.y += y;

                        SetTile(cell_, null);
                    }
                }

                if(room.doorRight == "None"){
                    Vector3Int cell_ = cell;
                    cell_.x += 6;
                    cell_.y += 3;

                    SetTile(cell_, null);
                }

                if(room.doorLeft == "None"){
                    Vector3Int cell_ = cell;
                    cell_.x += 0;
                    cell_.y += 3;

                    SetTile(cell_, null);
                }

                if(room.doorTop == "None"){
                    Vector3Int cell_ = cell;
                    cell_.x += 3;
                    cell_.y += 6;

                    SetTile(cell_, null);
                }

                if(room.doorBottom == "None"){
                    Vector3Int cell_ = cell;
                    cell_.x += 3;
                    cell_.y += 0;

                    SetTile(cell_, null);
                }

                // Door Object
                if(room.doorRight == "Door"){
                    Vector3Int cell_ = cell;
                    cell_.x += 6;
                    cell_.y += 3;

                    SetTile(cell_, normalDoorTile);
                }

                if(room.doorLeft == "Door"){
                    Vector3Int cell_ = cell;
                    cell_.x += 0;
                    cell_.y += 3;

                    SetTile(cell_, normalDoorTile);
                }

                if(room.doorTop == "Door"){
                    Vector3Int cell_ = cell;
                    cell_.x += 3;
                    cell_.y += 6;

                    SetTile(cell_, normalDoorTile);
                }

                if(room.doorBottom == "Door"){
                    Vector3Int cell_ = cell;
                    cell_.x += 3;
                    cell_.y += 0;

                    SetTile(cell_, normalDoorTile);
                }

                // Red Door Object
                if(room.doorRight == "RedDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 6;
                    cell_.y += 3;

                    SetTile(cell_, RedDoorTile);
                }

                if(room.doorLeft == "RedDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 0;
                    cell_.y += 3;

                    SetTile(cell_, RedDoorTile);
                }

                if(room.doorTop == "RedDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 3;
                    cell_.y += 6;

                    SetTile(cell_, RedDoorTile);
                }

                if(room.doorBottom == "RedDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 3;
                    cell_.y += 0;

                    SetTile(cell_, RedDoorTile);
                }

                // Green Door Object
                if(room.doorRight == "GreenDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 6;
                    cell_.y += 3;

                    SetTile(cell_, GreenDoorTile);
                }

                if(room.doorLeft == "GreenDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 0;
                    cell_.y += 3;

                    SetTile(cell_, GreenDoorTile);
                }

                if(room.doorTop == "GreenDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 3;
                    cell_.y += 6;

                    SetTile(cell_, GreenDoorTile);
                }

                if(room.doorBottom == "GreenDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 3;
                    cell_.y += 0;

                    SetTile(cell_, GreenDoorTile);
                }

                // Blue Door Object
                if(room.doorRight == "BlueDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 6;
                    cell_.y += 3;

                    SetTile(cell_, BlueDoorTile);
                }

                if(room.doorLeft == "BlueDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 0;
                    cell_.y += 3;

                    SetTile(cell_, BlueDoorTile);
                }

                if(room.doorTop == "BlueDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 3;
                    cell_.y += 6;

                    SetTile(cell_, BlueDoorTile);
                }

                if(room.doorBottom == "BlueDoor"){
                    Vector3Int cell_ = cell;
                    cell_.x += 3;
                    cell_.y += 0;

                    SetTile(cell_, BlueDoorTile);
                }
            }
        }
    }

    public void LoadLevel(){
        rooms = new GameObject[width, height];

        Rooms roomsJson = JsonUtility.FromJson<Rooms>(level.text);

        foreach(Room room in roomsJson.rooms){
            rooms[room.x, room.y] = Instantiate(roomPrefab);
            rooms[room.x, room.y].transform.parent = gameObject.transform;
            rooms[room.x, room.y].GetComponent<RoomBehaviour>().initiate(room);
        }
    }

    public GameObject GetNeighbor(int x, int y, int offsetX, int offsetY)
    {
        int newX = x + offsetX;
        int newY = y + offsetY;

        // Check if the new indices are within the bounds of the array
        if (newX >= 0 && newX < rooms.GetLength(0) && newY >= 0 && newY < rooms.GetLength(1))
        {
            return rooms[newX, newY];
        }

        // Return null or handle as necessary if out of bounds
        return null;
    }

    public void MovePlayer(int x, int y){
        Vector3Int cell = GridtoTileCell(new Vector3Int(x,y,0));

        cell.x += 3;
        cell.y += 3;

        Vector3 pos = tileGrid.CellToWorld(cell);

        pos.x += tileGrid.cellSize.x*grid.transform.localScale.x /2;
        pos.y += tileGrid.cellSize.y*grid.transform.localScale.x /2;

        player.transform.position = pos;

        GameObject fx = Instantiate(smokeFX);
        fx.GetComponent<Animator>().Play("SmokeFX1", 0);
        fx.transform.localPosition = pos;

        player.x = x;
        player.y = y;
    }

    public void MoveKey(int x, int y, string color){
        Vector3Int cell = GridtoTileCell(new Vector3Int(x,y,0));

        cell.x += 2;
        cell.y += 4;

        Vector3 pos = tileGrid.CellToWorld(cell);

        pos.x += tileGrid.cellSize.x*grid.transform.localScale.x /2;
        pos.y += tileGrid.cellSize.y*grid.transform.localScale.x /2;

        if (color == "RedDoor"){
            redKey.transform.position = pos;

            redKey.x = x;
            redKey.y = y;
        } else if (color == "GreenDoor"){
            greenKey.transform.position = pos;

            greenKey.x = x;
            greenKey.y = y;
        } else if (color == "BlueDoor"){
            blueKey.transform.position = pos;

            blueKey.x = x;
            blueKey.y = y;
        }
    }

    public void PlayerEnter(string direction){
        int x = player.x;
        int y = player.y;

        Room room = rooms[x, y].GetComponent<RoomBehaviour>().room;

        if(direction == "right"){
            if(room.doorRight == "Wall" || 
               room.doorRight == "Door" || 
               room.doorRight == "RedDoor" || 
               room.doorRight == "BlueDoor" || 
               room.doorRight == "GreenDoor"
               ){
                return;
            }

            x += 1;
        }else if(direction == "left"){
            if(room.doorLeft == "Wall" || 
               room.doorLeft == "Door" || 
               room.doorLeft == "RedDoor" || 
               room.doorLeft == "BlueDoor" || 
               room.doorLeft == "GreenDoor" 
               ){
                return;
            }

            x -= 1;
        }else if(direction == "top"){
            if(room.doorTop == "Wall" || 
               room.doorTop == "Door" || 
               room.doorTop == "RedDoor" || 
               room.doorTop == "BlueDoor" || 
               room.doorTop == "GreenDoor"
            ){
                return;
            }

            y += 1;
        }else if(direction == "bottom"){
            if(room.doorBottom == "Wall" || 
               room.doorBottom == "Door" || 
               room.doorBottom == "RedDoor" || 
               room.doorBottom == "BlueDoor" || 
               room.doorBottom == "GreenDoor"
            ){
                return;
            }

            y -= 1;
        }else{
            return;
        }

        MovePlayer(x, y);
        rooms[x,y].GetComponent<RoomBehaviour>().Enter();
    }

    public void SetTile(Vector3Int cell, Tile tile){
        if(tile == null){
            tile = floorTile;
        }

        tilemap.SetTile(cell, tile);
    }

    public Vector3Int GridtoTileCell(Vector3Int cell){
        return tileGrid.WorldToCell(grid.CellToWorld(cell));
    }

    public void ShowKey(Key key){
        redKeyUI.SetActive(false);
        greenKeyUI.SetActive(false);
        blueKeyUI.SetActive(false);

        string keyName = key.keyName;

        if(keyName == "RedDoor"){
            redKeyUI.SetActive(true);
        }else if(keyName == "GreenDoor"){
            greenKeyUI.SetActive(true);
        }else if(keyName == "BlueKey"){
            blueKeyUI.SetActive(true);
        }
    }
}

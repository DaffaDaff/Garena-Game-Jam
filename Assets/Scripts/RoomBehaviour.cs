using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomBehaviour : MonoBehaviour
{
    public Room room;
    public Room originalRoom;

    public GameObject manaOrb;

    void Start()
    {
        originalRoom = room;
    }

    void Update()
    {
        
    }

    public void spawnMana(float random, GameObject prefab){
        if (0.54 < random && random < 0.57){
            Vector3Int cell = GridManager.instance.GridtoTileCell(new Vector3Int(room.x, room.y, 0));
            room.mana = true;
            cell.x += 4;
            cell.y += 4;

            Vector3 pos = GridManager.instance.tileGrid.CellToWorld(cell);

            pos.x += GridManager.instance.tileGrid.cellSize.x*GridManager.instance.grid.transform.localScale.x /2;
            pos.y += GridManager.instance.tileGrid.cellSize.y*GridManager.instance.grid.transform.localScale.x /2;

            manaOrb = Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    public void initiate(Room room){
        this.room = room;
        originalRoom = room;
    }

    public void UnlockDoor(string direction, string key){
        Vector3Int cell = GridManager.instance.GridtoTileCell(new Vector3Int(room.x, room.y, 0));

        if(direction == "right"){
            if(room.doorRight == key){
                cell.x += 6;
                cell.y += 3;
                room.doorRight = "None";
                GridManager.instance.SetTile(cell, null);
                GridManager.instance.GetNeighbor(room.x, room.y, 1, 0).GetComponent<RoomBehaviour>().UnlockDoor("left", key);
            }
        }else if(direction == "left"){
            if(room.doorLeft == key){
                cell.x += 0;
                cell.y += 3;
                room.doorLeft = "None";
                GridManager.instance.SetTile(cell, null);
                GridManager.instance.GetNeighbor(room.x, room.y, -1, 0).GetComponent<RoomBehaviour>().UnlockDoor("right", key);
            }
        }else if(direction == "top"){
            if(room.doorTop == key){
                cell.x += 3;
                cell.y += 6;
                room.doorTop = "None";
                GridManager.instance.SetTile(cell, null);
                GridManager.instance.GetNeighbor(room.x, room.y, 0, 1).GetComponent<RoomBehaviour>().UnlockDoor("bottom", key);
            }
        }else if(direction == "bottom"){
            if(room.doorBottom == key){
                cell.x += 3;
                cell.y += 0;
                room.doorBottom = "None";
                GridManager.instance.SetTile(cell, null);
                GridManager.instance.GetNeighbor(room.x, room.y, 0, -1).GetComponent<RoomBehaviour>().UnlockDoor("top", key);
            }
        }
    }

    void updateTile(Vector3Int cell, string Type){
        if (Type == "None"){
            GridManager.instance.SetTile(cell, null);
        } else if (Type == "Wall"){
            return;
        } else if (Type == "Door"){
            GridManager.instance.SetTile(cell, GridManager.instance.normalDoorTile);
        } else if (Type == "RedDoor"){
            GridManager.instance.SetTile(cell, GridManager.instance.RedDoorTile);
        } else if (Type == "GreenDoor"){
            GridManager.instance.SetTile(cell, GridManager.instance.GreenDoorTile);
        } else if (Type == "BlueDoor"){
            GridManager.instance.SetTile(cell, GridManager.instance.BlueDoorTile);
        }
    }

    public void LockDoor(string direction)
    {
        Vector3Int cell = GridManager.instance.GridtoTileCell(new Vector3Int(room.x, room.y, 0));

        if (direction == "right"){
            cell.x += 6;
            cell.y += 3;
            updateTile(cell, originalRoom.doorRight);
        } else if (direction == "left"){
            cell.x += 0;
            cell.y += 3;
            updateTile(cell, originalRoom.doorLeft);            
        } else if (direction == "top"){
            cell.x += 3;
            cell.y += 6;
            updateTile(cell, originalRoom.doorTop);
        } else if (direction == "bottom"){
            cell.x += 3;
            cell.y += 0;
            updateTile(cell, originalRoom.doorBottom);
        }

    }

    public void LockRoom(){
        Vector3Int cell = GridManager.instance.GridtoTileCell(new Vector3Int(room.x, room.y, 0));

        LockDoor("bottom");
        GameObject neighbour1 = GridManager.instance.GetNeighbor(room.x, room.y, 0, -1);
        if (neighbour1){
            neighbour1.GetComponent<RoomBehaviour>().LockDoor("top");
        }

        LockDoor("top");
        GameObject neighbour2 = GridManager.instance.GetNeighbor(room.x, room.y, 0, 1);
        if (neighbour2){
            neighbour2.GetComponent<RoomBehaviour>().LockDoor("bottom");
        }

        LockDoor("right");
        GameObject neighbour3 = GridManager.instance.GetNeighbor(room.x, room.y, 1, 0);
        if (neighbour3){
            neighbour3.GetComponent<RoomBehaviour>().LockDoor("left");
        }

        LockDoor("left");
        GameObject neighbour4 = GridManager.instance.GetNeighbor(room.x, room.y, -1, 0);
        if (neighbour4){
            neighbour4.GetComponent<RoomBehaviour>().LockDoor("right");
        }
    }

    public void Enter(){
        if(room.item == "Exit"){
            
        }
        if (room.mana){
            room.mana = false;
            GridManager.instance.player.manaValue += 70;
            Destroy(manaOrb);
        }
    }
}

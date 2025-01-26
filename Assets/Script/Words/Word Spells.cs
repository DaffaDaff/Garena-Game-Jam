using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WordSpells : MonoBehaviour
{
    public Dictionary<string, Action> WordSpell;
    // Start is called before the first frame update
    void Start()
    {
        WordSpell = new Dictionary<string, Action> {
            {"TeleUp ", TeleUp},
            {"TeleDown ", TeleDown},
            {"TeleLeft ", TeleLeft},
            {"TeleRight ", TeleRight},
            {"Door is Open ", OpenDoor},
            {"Left Door is Open ", OpenLeftDoor},
            {"Right Door is Open ", OpenRightDoor},
            {"Top Door is Open ", OpenTopDoor},
            {"Bottom Door is Open ", OpenBottomDoor},
            {"Myself has Key ", PickUpKey}
        };
    }

    // ================================ player movement
    public void TeleUp()
    {
        GridManager.instance.PlayerEnter("top");
        GridManager.instance.player.manaValue -= 5;
    }

    public void TeleDown()
    {
        GridManager.instance.PlayerEnter("bottom");
        GridManager.instance.player.manaValue -= 5;
    }

    public void TeleRight()
    {
        GridManager.instance.PlayerEnter("right");
        GridManager.instance.player.manaValue -= 5;
    }

    public void TeleLeft()
    {
        GridManager.instance.PlayerEnter("left");
        GridManager.instance.player.manaValue -= 5;
    }

    // ============================================== open door
    public void OpenTopDoor()
    {
        RoomBehaviour room = GridManager.instance.GetPlayerRoom();
        string key = GridManager.instance.player.key.keyName;

        room.UnlockDoor("top", "Door");
        room.UnlockDoor("top", key);
        Debug.Log("Try open top door");
        GridManager.instance.player.manaValue -= 10;
        GridManager.instance.player.AddScore(1);
    }

    public void OpenBottomDoor()
    {
        var room = GridManager.instance.GetPlayerRoom();
        string key = GridManager.instance.player.key.keyName;

        room.UnlockDoor("bottom", "Door");
        room.UnlockDoor("bottom", key);
        Debug.Log("Try open bottom door");
        GridManager.instance.player.manaValue -= 10;
        GridManager.instance.player.AddScore(1);
    }

    public void OpenLeftDoor()
    {
        var room = GridManager.instance.GetPlayerRoom();
        string key = GridManager.instance.player.key.keyName;

        room.UnlockDoor("left", "Door");
        room.UnlockDoor("left", key);
        Debug.Log("Try open left door");
        GridManager.instance.player.manaValue -= 10;
        GridManager.instance.player.AddScore(1);
    }

    public void OpenRightDoor()
    {
        var room = GridManager.instance.GetPlayerRoom();
        string key = GridManager.instance.player.key.keyName;

        room.UnlockDoor("right", "Door");
        room.UnlockDoor("right", key);
        Debug.Log("Try open right door");
        GridManager.instance.player.manaValue -= 10;
        GridManager.instance.player.AddScore(1);
    }

    public void OpenDoor()
    {
        Debug.Log("Door is Opened");
        OpenTopDoor();
        OpenBottomDoor();
        OpenLeftDoor();
        OpenRightDoor();
        GridManager.instance.player.manaValue += 25;
        GridManager.instance.player.AddScore(-3);
    }

    // ====================================== pick up key

    public void PickUpKey(){
        Player player = GridManager.instance.player;
        RoomBehaviour playerRoom = GridManager.instance.GetPlayerRoom(); 

        if (player.key) {
            if (playerRoom.room.item == "RedKey")
            {
                GridManager.instance.redKey.gameObject.SetActive(false);
                GridManager.instance.MoveKey(player.x, player.y, player.key.keyName);
                player.key.gameObject.SetActive(true);
                playerRoom.room.item = player.key.gameObject.name;
                player.key = GridManager.instance.redKey;
            } else if (playerRoom.room.item == "GreenKey")
            {
                GridManager.instance.greenKey.gameObject.SetActive(false);
                GridManager.instance.MoveKey(player.x, player.y, player.key.keyName);
                player.key.gameObject.SetActive(true);
                playerRoom.room.item = player.key.gameObject.name;
                player.key = GridManager.instance.greenKey;
            } else if (playerRoom.room.item == "BlueKey")
            {
                GridManager.instance.blueKey.gameObject.SetActive(false);
                GridManager.instance.MoveKey(player.x, player.y, player.key.keyName);
                player.key.gameObject.SetActive(true);
                playerRoom.room.item = player.key.gameObject.name;
                player.key = GridManager.instance.blueKey;
            }
        } else {
            if (playerRoom.room.item == "RedKey")
            {
                GridManager.instance.redKey.gameObject.SetActive(false);
                player.key = GridManager.instance.redKey;
                playerRoom.room.item = "None";
            } else if (playerRoom.room.item == "GreenKey")
            {
                GridManager.instance.greenKey.gameObject.SetActive(false);
                player.key = GridManager.instance.greenKey;
                playerRoom.room.item = "None";
            } else if (playerRoom.room.item == "BlueKey")
            {
                GridManager.instance.blueKey.gameObject.SetActive(false);
                player.key = GridManager.instance.blueKey;
                playerRoom.room.item = "None";
            }
        }
        
        GridManager.instance.ShowKey(player.key);
        GridManager.instance.player.manaValue -= 5;
    }
}

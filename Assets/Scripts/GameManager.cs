using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject manaObject;
    public GameObject GameOverUI;
    
    // Start is called before the first frame update
    void Awake()
    {
        if ( Instance == null ){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void Cycle()
    {
        GameObject[,] rooms = GridManager.instance.rooms;

        for (int row = 0; row < rooms.GetLength(0); row++) // Iterate rows
        {
            for (int col = 0; col < rooms.GetLength(1); col++) // Iterate columns
            {
                if(rooms[row, col] == null) continue;
                
                float randomOffset = Random.Range(0f, 1f);
                rooms[row,col].GetComponent<RoomBehaviour>().spawnMana(randomOffset, manaObject);
                if (0.89 < randomOffset && randomOffset < 0.913){
                    rooms[row, col].GetComponent<RoomBehaviour>().LockRoom();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}

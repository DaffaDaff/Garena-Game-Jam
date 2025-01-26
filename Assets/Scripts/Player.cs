using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int x;
    public int y;

    public string item;
    public int manaValue = 100;
    public TMP_Text manaText;
    public GameObject GameOverUI;

    public Key key;

    private int score = 0;
    public TMP_Text scoreText;


    void Start()
    {

    }

    void Update()
    {
        if (manaValue <= 0){
            GameOver();
        }
        manaText.text = manaValue.ToString();
    }

    void GameOver()
    {
        // TODO : Implement GameOver
        HelpManager.Instance.updateState(3);
        GameOverUI.SetActive(true);
    }

    public void AddScore(int val){
        score += val;
        scoreText.text = "Score : " + score.ToString();
    }
}

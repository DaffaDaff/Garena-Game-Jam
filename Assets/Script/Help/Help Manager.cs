using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpManager : MonoBehaviour
{
    public static HelpManager Instance;
    public TMP_Text helpText1;
    public TMP_Text helpText2;
    public float cycleTime;
    public int state;
    public int gameGuideState = 0;
    
    private float timer;
    private bool first = true;

    void Awake() {
        if ( Instance == null ){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        } 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update references to TMP_Text components when a new scene is loaded
        if (scene.name == "LevelTest")
        {
            helpText2 = GameObject.FindWithTag("ChantedText").GetComponent<TMP_Text>();
            Debug.Log(helpText2);
        }
        if (scene.name == "UI Scene")
        {
            helpText1 = GameObject.FindWithTag("ChantedText").GetComponent<TMP_Text>();
            Debug.Log(helpText1);
        }
    }

    void Start()
    {
        if (helpText1 != null){
            helpText1.text = "Guide : \nUse > to start chanting \nUse < to go back";
        }
    }

    void Update(){
        timer += Time.deltaTime;

        if (first){
            if (timer > cycleTime/4){
                timer = 0;
                first = !first;
            }
        } else {
            if (timer > cycleTime){
                timer = 0;
                first = !first;
            }
        }

        if (state == 0){
            MainMenu();
        } else if (state == 1){
            Game();
        } else if (state == 2){
            Tutor();
        } else if (state == 3){
            GameOver();
        }
    }

    public void updateState(int newState){
        state = newState;
    }

    void MainMenu(){
        if (first){
            if (helpText1 != null){
                helpText1.text = "Guide : \nUse > to start chanting \nUse < to go back";
            }
        } else {
            if (helpText1 != null){
                helpText1.text = "Guide : \n> > < < > < < > : Play \n> < > < < > > : Tutorial \n> < < > < < < : Exit ";
            }
        }
    }

    void Tutor(){
        if (helpText1 != null){
            helpText1.text = "Guide : \n> > > < < > < : Menu \n\n> > > > : Next \n> > < < : Previous";
        }
    }

    void Game(){
        //TODO : Update it with sequence known
        List<string> GameGuide = new List<string> {
            "\nnavigate, \n> > > < < > < : Menu \n> < > < > < : Cancel Casting",
            "\nmovement, \n> < > < : Down \n> < < > : Up \n> < > > : Right \n> < < < : Left",
            // "\ncolor, \n> > > < < < < : Red \n> < < > > < < : Green \n> < < < < > > : Blue",
            "\ndirection, \n> < < < < > :Left \n> > > < > < < : Right \n> > < > > : Top \n> > < > < < > > : Bottom",
            "\nsubject, \n> > > < < : Myself \n> > < > < > > > : Door",
            "\nto be, \n> > < > : is \n> > < > < > : has",
            "\nadjective, \n> < > > < < > : Open \n> < < < > < > < < : Close",
            "\nnoun, \n> > < < < > : Key \n> < > < > > : Star"
        };
        if (helpText2 != null){
            helpText2.text = $"Guide : {GameGuide[gameGuideState]} \n\n> > > > : Next \n> > < < : Previous ";
        }
    }

    public void NextGameGuide(){
        gameGuideState = Mathf.Clamp(gameGuideState + 1, 0, 6);
    }

    public void PreviousGameGuide(){
        gameGuideState = Mathf.Clamp(gameGuideState - 1, 0, 6);
    }

    void GameOver(){
        if (helpText1 != null){
            helpText1.text = "Guide : \n> > > < < > < : Menu";
        }
    }
}

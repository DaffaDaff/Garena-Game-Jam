using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    // Dictionary to store sequences and their corresponding words
    public TMP_Text wordsText;
    public NavigationManager manager;

    public float TimeCast;
    public float timer;
    public float TimeWord;
    public WordSpells SpellBooks;

    private Dictionary<string, string> sequenceMap;
    private Dictionary<string, string> sequenceNavigation;
    private string words = "";




    void Start()
    {
        // Initialize the sequence map with predefined sequences
        sequenceMap = new Dictionary<string, string>
        {
            { "> > < > < > > > ", "Door" },
            { "> > > < < ", "Myself" },
            { "> < > > < < > ", "Open" },
            { "> > < > ", "is" },
            { "> > < < < > ", "Key" },
            { "> > > < < < < ", "Red" },
            { "> < < > > < < ", "Green" },
            { "> < < < < > > ", "Blue" },
            { "> > < > < > ", "has" },
            { "> < < < > < > < < ", "Close" },
            { "> < > < > > ", "Star" },
            { "> < < < < > ", "Left" },
            { "> > > < > < < ", "Right" },
            { "> > < > > ", "Top" },
            { "> > < > < < > > ", "Bottom" },
            { "> < < < ", "TeleLeft" },
            { "> < > > ", "TeleRight" },
            { "> < < > ", "TeleUp" },
            { "> < > < ", "TeleDown" },
        };

        sequenceNavigation = new Dictionary<string, string>
        {
            { "> > > < < > < ", "Menu" },
            { "> > < < > < < > ", "Level" },
            { "> < > < < > > ", "Tutor" },
            { "> < < > < < < ", "Exit" },
            { "> < > > > ", "1" },
            { "> > < > > ", "2" },
            { "> > > < > ", "3" },
            { "> > > > < ", "4" },
            { "> > > > ", "Next" },
            { "> > < < ", "Previous" },
            { "> < > < > < ", "Cancel"}
        };

        wordsText.text = "";
    }

    void Update(){
        if (SpellBooks){    
            timer += Time.deltaTime;
            if(timer > TimeCast && SpellBooks.WordSpell.ContainsKey(words)){

                AudioManager.Instance.PlaySound(UnityEngine.Random.value < 0.5f ? "Spell1" : "Spell2");
                timer = 0;
                SpellBooks.WordSpell[words].Invoke();
                words = "";
                DisplayWord();
                GameManager.Instance.Cycle();
                GridManager.instance.player.AddScore(1);
            }

            if(timer > TimeWord){
                timer = 0;
                words = "";
                DisplayWord();
            }
        }

    }

    public void InputSequence(string sequence){
        if (sequenceMap.ContainsKey(sequence)){
            words += sequenceMap[sequence] + " "; 
        } else if (sequenceNavigation.ContainsKey(sequence)) {
            // Navigate based on the Word
            if (sequenceNavigation[sequence] == "Menu"){
                NavigationManager.Instance.NavigateToMenu();
            } else if (sequenceNavigation[sequence] == "Level"){
                NavigationManager.Instance.NavigateToGame();
            } else if (sequenceNavigation[sequence] == "Tutor"){
                NavigationManager.Instance.NavigateToTutor();
            } else if (sequenceNavigation[sequence] == "Exit"){
                // TODO: Exit The Game
            } else if (sequenceNavigation[sequence] == "Next"){
                NavigationManager.Instance.NavigationNext();
            } else if (sequenceNavigation[sequence] == "Previous"){
                NavigationManager.Instance.NavigationPrevious();
            } else if (sequenceNavigation[sequence] == "Cancel"){
                words = "";
            }
        } else {
            Debug.Log("Nothing Happens");
        }
        timer = 0;
        DisplayWord();
    }

    void Cast(){

    }

    // Method to display the corresponding word for a given sequence
    public void DisplayWord()
    {
        // TODO : Update to give more effect
        if (wordsText != null){
            wordsText.text = words;
            wordsText.GetComponent<TypingEffect>().fullText = words;
            wordsText.GetComponent<TypingEffect>().StartTyping();
        }
    }
}

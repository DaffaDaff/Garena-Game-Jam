using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SequenceInput : MonoBehaviour
{
    public TMP_Text displayText;  // Reference to the Text UI element
    public float timeLimit;
    public float timeCast;

    public InputManager inputManager;
    public WordManager wordManager;
    private string currentString = "";  // The string being managed
    private float timer;

    void Start()
    {
        // Initialize the display text
        if (displayText != null)
        {
            displayText.text = "Start Chanting";
        }
    }

    public void Reset()
    {
        // Initialize the display text
        if (displayText != null)
        {
            displayText.text = "Start Chanting";
        }
    }

    public void Chant()
    {
        // Initialize the display text
        if (displayText != null)
        {
            displayText.text = "Cast:";
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeCast && NavigationManager.Instance.currentNavigation.SequenceInput.Contains(currentString)){
            timer = 0;
            wordManager.InputSequence(currentString);
            currentString = "";
            Reset();
            inputManager.state = 0;
        }

        else if (timer > timeLimit){
            timer = 0;
            currentString = "";
            Reset();
            inputManager.state = 0;
        }
    }

    // Method to update the string and display it
    public void UpdateString(string newInput)
    {
        if (!string.IsNullOrEmpty(newInput))
        {
            currentString += newInput + " ";  // Append input to the current string
            DisplayString();  // Update the display
        }
        timer = 0;
    }

    // Method to display the updated string
    private void DisplayString()
    {
        if (displayText != null)
        {
            displayText.text = "Cast: " + currentString;
        }
    }
}

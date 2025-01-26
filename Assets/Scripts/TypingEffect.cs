using System.Collections;
using UnityEngine;
using TMPro; // Use this if using TextMeshPro

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Assign in the Inspector if using TextMeshPro
    // public Text textComponent; // Use this for the default Text component
    public float typingSpeed = 0.05f;
    public string fullText;

    private string currentText = "";

    void Start()
    {
        
    }

    public void StartTyping(){
        currentText = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}

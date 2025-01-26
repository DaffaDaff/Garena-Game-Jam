using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    public AudioSource audioSource;
    public List<AudioClip> soundClips; // Assign sounds in the Inspector
    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();


    void Awake()
    {
        if ( Instance == null ){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        foreach (var clip in soundClips)
        {
            soundDictionary[clip.name] = clip;
        }
    }

    void Start()
    {

    }

    public void PlaySound(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            audioSource.PlayOneShot(soundDictionary[soundName]);
        }
        else
        {
            Debug.LogWarning($"Sound {soundName} not found!");
        }
    }
}

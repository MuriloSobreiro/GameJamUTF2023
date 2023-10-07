using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMaster : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] audios;
    int mi = 0;
    public string lugar = "Menu";

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if ((SceneManager.GetActiveScene().name.StartsWith("Menu")|| lugar =="Menu") && !audioSource.isPlaying && mi <1)
        {
            lugar = "Menu";
            audioSource.clip = audios[mi];
            audioSource.Play();
            mi = 1;
        }
        if (lugar == "Menu" && SceneManager.GetActiveScene().name.StartsWith("Fase"))
            lugar = "Fase";
            audioSource.clip = audios[2];
        if(lugar == "GameOver")
        {
            audioSource.loop = false;
            audioSource.clip = audios[3];
            audioSource.Play();
        }
    }
}

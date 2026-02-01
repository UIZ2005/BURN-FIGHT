using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sonidoletra : MonoBehaviour
{
    public TMP_InputField inputField; // Asigna el InputField en el Inspector
    public AudioSource audioSource; // Asigna el AudioSource en el Inspector
    public AudioClip typingSound; // Asigna el sonido de escritura en el Inspector

    private string lastText = ""; // Para detectar nuevas letras

    void Start()
    {
        if (inputField != null)
            inputField.onValueChanged.AddListener(PlayTypingSound);
    }

    void PlayTypingSound(string newText)
    {
        // Solo reproduce sonido si se agregó una nueva letra
        if (newText.Length > lastText.Length && audioSource != null && typingSound != null)
        {
            audioSource.PlayOneShot(typingSound);
        }

        lastText = newText;
    }
}

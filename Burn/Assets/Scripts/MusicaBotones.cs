using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaBotones : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource music;
    public AudioClip clickAudio;
    public AudioClip cambioAudio;

    void Start()
    {
        music = GetComponent<AudioSource>();
    }

    public void clockAudioOn()
    {
        music.PlayOneShot(clickAudio);
    }

    public void SwitchAudioOn()
    {
        music.PlayOneShot(cambioAudio);
    }
}

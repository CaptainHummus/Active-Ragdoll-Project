using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource ambience;
    [SerializeField] private AudioMixerGroup master;
    [SerializeField] private AudioMixer mixer;
    private bool isMasterMuted;
    private bool isMusicMuted;
    private float musicDefaultVolume;


    private void Start()
    {
        mixer.GetFloat("MusicVolume", out musicDefaultVolume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            music.DOPitch(music.pitch += 0.25f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            music.DOPitch(music.pitch -= 0.25f, 1f);
        }

        music.pitch = Mathf.Clamp(music.pitch, -3, +3);

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.M))
        {
            if (!isMusicMuted)
            {
                mixer.DOSetFloat("MusicVolume", -80f, 3f);
                isMusicMuted = true;
            }
            else
            {
                mixer.DOSetFloat("MusicVolume", musicDefaultVolume, 3f);
                isMusicMuted = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isMasterMuted)
            {
                mixer.DOSetFloat("MasterVolume", -80f, 3f);
                isMasterMuted = true;
            }
            else
            {
                mixer.DOSetFloat("MasterVolume", 0f, 3f);
                isMasterMuted = false;
            }
        }

    }
}

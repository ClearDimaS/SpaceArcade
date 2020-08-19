using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.volume = DataController.GetValue<int>(Constants.VOLUME) / 100.0f;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void ChangeVolume(float volume, bool isMusicOn) 
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }

        if (!isMusicOn)
        {
            Sound s = Array.Find(sounds, sound => sound.name == Constants.SND_BCKGND_MUSIC);

            s.source.volume = 0;
        }
    }

    void Start()
    {
        Play("Theme");
    }

    public void Play(string name) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) 
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
        // FindObjectOfType<AudioManager>().Play("audio_name");
    }
}

using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;
        }
        Play("BackgroundMusic");
    }

    public void Play(string name)
    {
        Sound s = null;
        foreach (Sound track in sounds)
        {
            if(track.name == name) s = track;
        }
        s.source.Play();
    }
        public void Stop(string name)
    {
        Sound s = null;
        foreach (Sound track in sounds)
        {
            if(track.name == name) s = track;
        }
        s.source.Stop();
    }
}

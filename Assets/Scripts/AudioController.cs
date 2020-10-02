using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;
        [HideInInspector]
        public AudioSource source;
    }

    public List<Sound> soundList;

    public static AudioController Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in soundList)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    private void Start()
    {
        LoopSound("background");        
    }


    public void PlaySound(string name)
    {
        Sound s = soundList.Find(sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();
        }        
    }

    public void LoopSound(string name)
    {
        Sound s = soundList.Find(sound => sound.name == name);
        if (s != null)
        {
            s.source.loop = true;
            s.source.Play();
        }
    }
}


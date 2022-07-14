using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [System.Serializable] struct NamedSounds
    {
        public string name;
        public AudioClip audioClip;
    }

    [SerializeField] private NamedSounds[] _namedSounds;

    private Dictionary<string, AudioClip> _soundsDictionary;

    void Start()
    {
        if (Instance == null)
        {
            _soundsDictionary = new Dictionary<string, AudioClip>();
            foreach (var sound in _namedSounds)
            {
                _soundsDictionary.Add(sound.name, sound.audioClip);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    public void PlaySFX(AudioClip audioClip, float volume = 0.8f)
    {
        _sfxSource.PlayOneShot(audioClip, volume);
    }

    public void PlaySFX(string name, float volume = 0.8f)
    {
        if (_soundsDictionary.ContainsKey(name))
            _sfxSource.PlayOneShot(_soundsDictionary[name], volume);
        else
            Debug.Log("There is no sound with such name");
    }

    public void PlayMusic()
    {
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

}

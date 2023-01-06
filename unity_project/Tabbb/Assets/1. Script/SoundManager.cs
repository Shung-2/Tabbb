using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get { return instance; }
    }

    [SerializeField] private List<AudioClip> SFXAudioClipList;
    [SerializeField] private AudioSource SFXAudioSource;
    [SerializeField] private float soundDelay;
    [SerializeField] private Dictionary<string, float> dictionaryCurrentPlaySound = new Dictionary<string, float>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySFX(string audio, float _delay = 0f)
    {
        if (!dictionaryCurrentPlaySound.ContainsKey(audio))
        {
            StartCoroutine(PlayDelayedSFX(SFXAudioClipList.Find(clip => clip.name == audio), _delay));
            dictionaryCurrentPlaySound.Add(audio, soundDelay);
        }
    }

    IEnumerator PlayDelayedSFX(AudioClip _clip, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        SFXAudioSource.PlayOneShot(_clip);
    }
}
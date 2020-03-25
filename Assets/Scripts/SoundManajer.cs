using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManajer : Singleton<SoundManajer>
{

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();


    // Start is called before the first frame update
    void Start()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio") as AudioClip[];
        
        foreach(AudioClip clip in clips)
        {
            audioClips.Add(clip.name, clip);
        }
    }

    // Update is called once per frame

    public void PlaySFX(string name)
    {
        sfxSource.PlayOneShot(audioClips[name]);
    }
}

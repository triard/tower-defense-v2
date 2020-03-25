using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBack : Singleton<SoundBack>
{
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private Slider musicSlider;
    // Start is called before the first frame update
    void Start()
    {
        LoadVolume();
        musicSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVolume()
    {


        musicSource.volume = musicSlider.value;

        PlayerPrefs.SetFloat("Music", musicSlider.value);
    }

    public void LoadVolume()
    {

        musicSource.volume = PlayerPrefs.GetFloat("Music", 0.5f);

        musicSlider.value = musicSource.volume;
    }
}

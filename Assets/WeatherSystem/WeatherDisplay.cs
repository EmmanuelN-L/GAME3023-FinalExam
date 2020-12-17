using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherDisplay : MonoBehaviour
{
    public WeatherState weatherState;

    public Text WeatherNameText;

    //public ParticleSystem WeatherParticleSys;

    
    // Start is called before the first frame update
    void Start()
    {
        WeatherNameText.text = weatherState.WeatherName;
        //WeatherParticleSys = weatherState.WeatherEffects;
       // gameObject.AddComponent<ParticleSystem>();
        var ps = GetComponent<ParticleSystem>();
        var As = GetComponent<AudioSource>();

        As.clip = weatherState.SoundClip;
        As.Play();
        
    }

    public void WeatherChange(WeatherState ws)
    {
        var As = GetComponent<AudioSource>(); 
        As.Stop();
        weatherState = ws;
        WeatherNameText.text = weatherState.WeatherName;

        
        As.clip = weatherState.SoundClip;
        As.Play();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;

public class WeatherDisplay : MonoBehaviour
{
    public WeatherState weatherState;

    public Text WeatherNameText;

    public UnityEngine.Experimental.Rendering.Universal.Light2D Sun;

    //public ParticleSystem WeatherParticleSys;


    // Start is called before the first frame update
    void Start()
    {
        Sun = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        WeatherNameText.text = weatherState.WeatherName;
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
        Sun.intensity = ws.lightIntensity;
        Sun.color = ws.lightColor;
        As.clip = weatherState.SoundClip;
        As.Play();
    }


}

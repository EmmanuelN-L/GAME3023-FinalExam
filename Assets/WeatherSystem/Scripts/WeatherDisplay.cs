﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;

public class WeatherDisplay : MonoBehaviour
{
    public WeatherState weatherState;

    public Text WeatherNameText;

    private UnityEngine.Experimental.Rendering.Universal.Light2D Sun;

    bool lightningEnabled;
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
        //Sun.intensity = weatherState.lightIntensity;
        StartCoroutine(ColorChanger(weatherState.lightColor.x, weatherState.lightColor.y, weatherState.lightColor.z, 5.0f));
        
        As.clip = weatherState.SoundClip;
        As.Play();
        if (weatherState.WeatherName == ("ThunderStorm"))
        {
            //Debug.Log("Lightning is enabled");
            lightningEnabled = true;
            StartCoroutine(LightningFlash());
        }
        else
            lightningEnabled = false;
    }

    IEnumerator ColorChanger(float R, float G, float B,  float TransitionTime)
    {
        var oldColor = Sun.color;
        var time = 0.0f;
        Color newColor = new Color
        {
            r = R/255,
            g = G/255,
            b = B/255,
            a = 1
        };
        while (time < 1.0f)
        {
            //var changeColor = 
            Sun.color = Color.Lerp(oldColor, newColor, time);
            time = time + Time.deltaTime / TransitionTime;
            yield return null;
        }
    }
    IEnumerator LightningFlash()
    {
        Sun.intensity = 3.0f;
        yield return new WaitForSeconds(0.3f);
        Sun.intensity = 1.0f;
        yield return new WaitForSeconds(0.2f);
        Sun.intensity = 2.0f;
        yield return new WaitForSeconds(0.2f);
        Sun.intensity = 1.0f;
        yield return new WaitForSeconds(3.0f);
        if (lightningEnabled)
            StartCoroutine(LightningFlash());
        else
            lightningEnabled = false;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicWeather : MonoBehaviour
{
    public WeatherState RainState;
    public WeatherState SunnyState;
    public WeatherState ThunderstormState;
    public WeatherState OvercastState;
    public ParticleSystem rain;
    ParticleSystem.EmissionModule emissionModule;

    public float RainEmissionRate;
    float timePassed;
    GameObject WeatherDisplay;
    //WeatherDisplay wd;
    bool weatherChange = false;
    
    public Text Timetext;
    
    void Start()
    {
        WeatherDisplay = GameObject.FindGameObjectWithTag("WeatherDisplay");
        emissionModule = rain.emission;
    }

    void Update()
    {
        timePassed += Time.smoothDeltaTime;
        Timetext.text ="Time Passed: " +  timePassed.ToString();
        //Debug.Log(eRate);

        if(timePassed >= 5 && !weatherChange)
        {
            
            Rainy();
            weatherChange = true;
        }
    }
    public void Sunny()
    {
        WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(SunnyState);
    }
    public void Overcast()
    {
        StartCoroutine(RainEmission(emissionModule, 100, 0, 5.0f));
        rain.Stop();
        WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(OvercastState);
    }
    public void Thunderstorm()
    {
        WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(ThunderstormState);
    }
    public void Rainy()
    {
        rain.Play();
        StartCoroutine(RainEmission(emissionModule, 0, 100, 5.0f));

        WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(RainState);
    }

    IEnumerator RainEmission( ParticleSystem.EmissionModule em, float minEmission, float maxEmission, float TransitionTime)
    {
        //var start = em.rateOverTime;
        //var end = start + Mathf.Lerp(start, 100f, time);
        var time = 0.0f;

        while(time < 1.0f)
        {
            em.rateOverTime = Mathf.Lerp(minEmission, maxEmission, time);
            time = time + Time.deltaTime / TransitionTime;
            yield return null;
        }
    }

}

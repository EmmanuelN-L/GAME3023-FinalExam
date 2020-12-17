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

    float timePassed;
    GameObject WDisplay;
    //WeatherDisplay wd;
    bool weatherChange = false;
    
    public Text Timetext;
    public enum WeatherStates
    {
        WeatherChoice,
        Sunny,
        Overcast,
        Thunderstorm,
        Rainy
    }
    

    void Start()
    {
        WDisplay = GameObject.FindGameObjectWithTag("WeatherDisplay");
    }

    void Update()
    {
        timePassed += Time.smoothDeltaTime;
        Timetext.text ="Time Passed: " +  timePassed.ToString();
        if(timePassed >= 5 && !weatherChange)
        {
            Rainy();
            weatherChange = true;
        }
    }
    void Sunny()
    {

    }
    void Overcast()
    {

    }
    void Thunderstorm()
    {

    }
    void Rainy()
    {
        
        rain.Play();
        WDisplay.GetComponent<WeatherDisplay>().WeatherChange(RainState);
        //wd.WeatherChange(RainState);
    }
}

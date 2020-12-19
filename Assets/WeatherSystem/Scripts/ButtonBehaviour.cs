using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    GameObject dynamicWeather;
    private void Start()
    {
        dynamicWeather = GameObject.FindGameObjectWithTag("WeatherController");
    }
    public void SunnyButton()
    {
        dynamicWeather.GetComponent<DynamicWeather>().Sunny();
    }

    public void OvercastButton()
    {
        dynamicWeather.GetComponent<DynamicWeather>().Overcast();
    }

    public void RainButton()
    {
        dynamicWeather.GetComponent<DynamicWeather>().Rainy();
    }

    public void ThunderstormButton()
    {
        dynamicWeather.GetComponent<DynamicWeather>().Thunderstorm();
    }
    public void turnOffWeatherChanger()
    {
        dynamicWeather.GetComponent<DynamicWeather>().weatherPicker = false;
        
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DynamicWeather : MonoBehaviour
{
    public WeatherState currentState;
    public WeatherState RainState;
    public WeatherState SunnyState;
    public WeatherState ThunderstormState;
    public WeatherState OvercastState;
    public ParticleSystem rain;
    public ParticleSystem Cloud;
    public ParticleSystem Cloud2;
    public GameObject player;
    ParticleSystem.EmissionModule emissionModule;

    public float RainEmissionRate;
    float timePassed;
    GameObject WeatherDisplay;

    int randNum;
    public float repeaterTime = 15.0f;
    public Text Timetext;
    public bool weatherPicker = true;

    
    
    void Start()
    {
        currentState = SunnyState;
        WeatherDisplay = GameObject.FindGameObjectWithTag("WeatherDisplay");
        emissionModule = rain.emission;
        StartCoroutine(WeatherPickRepeater(repeaterTime));
    }

    void Update()
    {
        if(weatherPicker)
        {
            timePassed += Time.smoothDeltaTime;
            Timetext.text ="Time Passed: " +  timePassed.ToString();
        }
        else
            Timetext.text = "Weather Picker off";

    }
    //Selecting the weather state
    public void WeatherPicker()
    {   
            //If in sunny state
            if (currentState == SunnyState)
            {
                randNum = Random.Range(0, 100);
                //Sunny state
                if (randNum <= 40)
                {
                    //stay in sunny state
                    Debug.Log("Staying in sunny state");
                }
                else //Go to overcast state
                {                 
                    Debug.Log("Going to overcast state");
                    Overcast();
                }
            }       //If in overcast state
            else if (currentState == OvercastState)
            {
                randNum = Random.Range(0, 100);
                if (randNum <= 33)
                {
                    //stay in overcast state
                    Debug.Log("Staying in overcast state");
                }
                else if (randNum > 33 && randNum <= 66)
                {
                    //Go to sunny state
                    Debug.Log("Going to sunny state");
                    Sunny();
                }
                else
                {
                    //Go to Rain state
                    Debug.Log("Going to rain state");
                    Rainy();
                }
            }       // If current state is rain state
            else if (currentState == RainState)
            {
                randNum = Random.Range(0, 100);
                if (randNum <= 33)
                {
                    //stay in overcast state
                    Debug.Log("Staying in rain state");
                }
                else if (randNum > 33 && randNum <= 66)
                {
                    //Go to overcast state
                    Debug.Log("Going to overcast state");
                    Overcast();
                }
                else
                {
                    //Go to thunderstorm state                  
                    Debug.Log("Going to thunderstorm state");
                    Thunderstorm();
                }
            }        //If in thunderstorm state
            else if (currentState == ThunderstormState)
            {
                randNum = Random.Range(0, 100);

                if (randNum <= 25)
                {
                    //Stay in thunderstorm state
                    Debug.Log("Staying in thunderstorm state");
                }
                else //Go to rain state
                {
                    Debug.Log("Going to rain state");
                    Rainy();
                }
            }
            timePassed = 0;
            //StartCoroutine(RainEmission(emissionModule, 100, 250, 3.0f));

    }

    public void Sunny()
    {
        if (currentState == OvercastState)
        {
            Cloud.Stop();
            Cloud2.Stop();
            WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(SunnyState);
        }
        else
            Debug.LogError("Can't change from that state");
    }
    public void Overcast()
    {
        Debug.Log("Current state: " + currentState);
        if (currentState == SunnyState)
        {
            Cloud.Play();
            Cloud2.Play();
            currentState = OvercastState;
            WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(OvercastState);
        }
        else if(currentState == RainState)
        {
            currentState = OvercastState;
            StartCoroutine(RainEmission(emissionModule, 100, 0, 3.0f));
            WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(OvercastState);
        }
        else
            Debug.LogError("Can't change from that state");
    }
    public void Thunderstorm()
    {
        if (currentState == RainState)
        {
            currentState = ThunderstormState;
            StartCoroutine(RainEmission(emissionModule, 100, 250, 3.0f));
            WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(ThunderstormState);
        }
        else
            Debug.LogError("Can't change from that state");
        
    }
    public void Rainy()
    {
        if (currentState == OvercastState)
        {
            currentState = RainState;
            if (!(rain.isPlaying))
            {
                rain.Play();
            }
            StartCoroutine(RainEmission(emissionModule, 0, 100, 5.0f));
            WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(RainState);
        }
        else if (currentState == ThunderstormState)
        {
            currentState = RainState;
            StartCoroutine(RainEmission(emissionModule, 250, 100, 3.0f));
            WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(RainState);
        }
        else
            Debug.LogError("Can't change from that state");
    }
    IEnumerator WeatherPickRepeater(float repeatingTime)
    {
        //float time = 0.0f;
        //int numTimes = 0;

        while(weatherPicker)
        {            
            yield return new WaitForSeconds(repeatingTime);
            //Debug.Log("Called: " + numTimes);
            WeatherPicker();
            //numTimes += 1;
            //time = 0;
        }
        
    }
    IEnumerator RainEmission( ParticleSystem.EmissionModule emission, float minEmission, float maxEmission, float TransitionTime)
    {
        //var start = em.rateOverTime;
        //var end = start + Mathf.Lerp(start, 100f, time);
        float time = 0.0f;

        while(time < 1.0f)
        {
            emission.rateOverTime = Mathf.Lerp(minEmission, maxEmission, time);
            time = time + Time.deltaTime / TransitionTime;
            yield return null;
        }
    }


}

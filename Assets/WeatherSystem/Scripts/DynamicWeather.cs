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
    
    public Text Timetext;
    
    void Start()
    {
        currentState = SunnyState;
        WeatherDisplay = GameObject.FindGameObjectWithTag("WeatherDisplay");
        emissionModule = rain.emission;
    }

    void Update()
    {
        timePassed += Time.smoothDeltaTime;
        Timetext.text ="Time Passed: " +  timePassed.ToString();
        if (timePassed >= 15)
        {
            WeatherPicker();
            timePassed = 0;
        }
        //Debug.Log(eRate);        
    }

    void WeatherPicker()
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
            }    
            //If in overcast state
            if(currentState == OvercastState)
            {
                randNum = Random.Range(0, 100);
                if (randNum <=33)
                {
                    //stay in overcast state
                    Debug.Log("Staying in overcast state");
                }
                else if (randNum >33 && randNum<=66)
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
            }
            // If current state is rain state
            if (currentState == RainState)
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
            }

            //If in thunderstorm state
            if (currentState == ThunderstormState)
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
        
    }

    public void Sunny()
    {

        Cloud.Stop();
        Cloud2.Stop();
        currentState = SunnyState;
        WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(SunnyState);
        
    }
    public void Overcast()
    {
        Cloud.Play();
        Cloud2.Play();
        currentState = OvercastState;
        StartCoroutine(RainEmission(emissionModule, 100, 0, 3.0f));
        WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(OvercastState);
    }
    public void Thunderstorm()
    {
        currentState = ThunderstormState;
        StartCoroutine(RainEmission(emissionModule, 100, 250, 3.0f));
        WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(ThunderstormState);
    }
    public void Rainy()
    {
        //float cloudSpeed = 1.0f;        
        //Cloud.transform.Translate(Vector3.Normalize(player.transform.position - transform.position) * cloudSpeed);
        currentState = RainState;
        rain.Play();
        StartCoroutine(RainEmission(emissionModule, 0, 100, 5.0f));
        WeatherDisplay.GetComponent<WeatherDisplay>().WeatherChange(RainState);
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

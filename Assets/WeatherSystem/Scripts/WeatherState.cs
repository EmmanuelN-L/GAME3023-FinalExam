using UnityEngine;

[CreateAssetMenu(fileName = "New Weather State", menuName = "WeatherSystem")]
public class WeatherState : ScriptableObject
{

    public string WeatherName;
    public string description;
  //  public ParticleSystem WeatherEffects;
    public AudioClip SoundClip;
   // public GameObject go;
    public Vector3 lightColor;
    public float lightIntensity;
}

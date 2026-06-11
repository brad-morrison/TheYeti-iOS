using UnityEngine;

public class Weather : TheYeti
{
    public enum WeatherType
    {
        clear,
        rain,
        snow,
        blizzard,
        storm
    };

    public WeatherType weather;

    public GameObject snow, rain, blizzard, storm;

    void Start()
    {
        RollForWeather();
    }

    public void RollForWeather()
    {
        int weatherTypesCount = System.Enum.GetValues(typeof(WeatherType)).Length;
        int roll = Random.Range(1, weatherTypesCount);
        SetWeather((WeatherType)roll);
    }

    public void SetWeather(WeatherType _weather)
    {
        weather = _weather;

        switch (weather)
        {
            case WeatherType.clear:
                Clear();
                break;
            case WeatherType.rain:
                Rain();
                break;
            case WeatherType.snow:
                Snow();
                break;
            case WeatherType.blizzard:
                Blizzard();
                break;
            case WeatherType.storm:
                Storm();
                break;
        }
    }

    public void Snow()
    {
        Instantiate(snow);
    }

    public void Blizzard()
    {
        Instantiate(blizzard);
    }

    public void Clear()
    {
    }

    public void Rain()
    {
        Instantiate(rain);
    }

    public void Storm()
    {
        Debug.Log("calling storm");
        Instantiate(blizzard);
        GM.gameManager.sky.StormSky();
    }
}

using System.Collections;
using System.Collections.Generic;
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
        //SetWeather(WeatherType.storm);
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
        if (weather == WeatherType.clear) Clear();
        if (weather == WeatherType.blizzard) Blizzard();
        if (weather == WeatherType.snow) Snow();
        if (weather == WeatherType.rain) Rain();
        if (weather == WeatherType.storm) Storm();

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

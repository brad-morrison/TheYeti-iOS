using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sky : TheYeti
{
    public GameObject[] backgrounds;
    public Color frenzyModeColour, stormSkyColour, GoldModeColour;
    public float skyColourFadeTime;

    private void Awake()
    {
        GameObject sky = Instantiate(RandomBackground());
        sky.transform.parent = gameObject.transform;
    }

    public GameObject RandomBackground()
    {
        if (backgrounds.Length > 0)
            return backgrounds[Random.Range(0, backgrounds.Length)];
        else
            return null;
    }

    public void FrenzyModeSky(bool isOn)
    {
        Color target = isOn ? frenzyModeColour : Color.white;

        if (!isOn && GM.gameManager.weather.weather == Weather.WeatherType.storm)
            target = stormSkyColour;

        StopAllCoroutines();

        foreach (SpriteRenderer sprite in transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>())
        {
            StartCoroutine(ChangeColour(sprite, target, skyColourFadeTime));
        }
    }

    public void GoldModeSky(bool isOn)
    {
        Color target = isOn ? GoldModeColour : Color.white;

        if (!isOn && GM.gameManager.weather.weather == Weather.WeatherType.storm)
            target = stormSkyColour;

        StopAllCoroutines();

        foreach (SpriteRenderer sprite in transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>())
        {
            StartCoroutine(ChangeColour(sprite, target, skyColourFadeTime));
        }
    }

    public void StormSky()
    {
        StopAllCoroutines();
        Debug.Log("setting storm colours");
        foreach (SpriteRenderer sprite in transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = stormSkyColour;
            Debug.Log("setting " + sprite + " colour to " + stormSkyColour);
        }
    }

    public IEnumerator ChangeColour(SpriteRenderer sprite, Color targetColour, float duration)
    {
        float time = 0;
        Color startColour = sprite.color;

        while (time < duration)
        {
            sprite.color = Color.Lerp(startColour, targetColour, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        
    }
}

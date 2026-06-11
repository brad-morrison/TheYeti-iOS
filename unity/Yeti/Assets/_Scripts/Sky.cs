using System.Collections;
using UnityEngine;

public class Sky : TheYeti
{
    public GameObject[] backgrounds;
    public Color frenzyModeColour, stormSkyColour, GoldModeColour;
    public float skyColourFadeTime;

    private void Awake()
    {
        GameObject background = RandomBackground();
        if (background == null)
            return;

        GameObject sky = Instantiate(background);
        sky.transform.parent = transform;
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
        FadeSkyTo(isOn ? frenzyModeColour : DefaultSkyColour());
    }

    public void GoldModeSky(bool isOn)
    {
        FadeSkyTo(isOn ? GoldModeColour : DefaultSkyColour());
    }

    public void StormSky()
    {
        StopAllCoroutines();
        Debug.Log("setting storm colours");
        foreach (SpriteRenderer sprite in SkySprites())
        {
            sprite.color = stormSkyColour;
            Debug.Log("setting " + sprite + " colour to " + stormSkyColour);
        }
    }

    private Color DefaultSkyColour()
    {
        if (GM.gameManager.weather.weather == Weather.WeatherType.storm)
            return stormSkyColour;

        return Color.white;
    }

    private void FadeSkyTo(Color target)
    {
        StopAllCoroutines();

        foreach (SpriteRenderer sprite in SkySprites())
        {
            StartCoroutine(ChangeColour(sprite, target, skyColourFadeTime));
        }
    }

    private SpriteRenderer[] SkySprites()
    {
        if (transform.childCount == 0)
            return new SpriteRenderer[0];

        return transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>();
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

        sprite.color = targetColour;
    }
}

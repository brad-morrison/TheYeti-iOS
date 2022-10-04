using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Costumes : MonoBehaviour
{
    public List<Costume> costumesList = new List<Costume>();
    
    // sprite arrays for costumes
    public Sprite[]     original = new Sprite[6], 
                        sailor = new Sprite[6], 
                        angryYeti = new Sprite[6], 
                        drFrog = new Sprite[6], 
                        yetiGirl = new Sprite[6], 
                        bigFoot = new Sprite[6], 
                        sillyWizard = new Sprite[6], 
                        captainFuzz = new Sprite[6],
                        ghostYeti = new Sprite[6], 
                        rayYeti = new Sprite[6], 
                        yetimon = new Sprite[6], 
                        yetiWonka = new Sprite[6], 
                        devilYeti = new Sprite[6], 
                        zombieYeti = new Sprite[6], 
                        yetiFloss = new Sprite[6], 
                        rainbowYeti = new Sprite[6],
                        steamboatYeti = new Sprite[6];

    // Start is called before the first frame update
    void Awake()
    {
        
        costumesList.Add(new Costume(original, "Yeti", 0, 0, true));
        costumesList.Add(new Costume(sailor, "Sailor", 80, 500, true));
        costumesList.Add(new Costume(angryYeti, "Angry Yeti", 180, 800, true));
        costumesList.Add(new Costume(drFrog, "Dr Frog", 240, 1000, true));
        costumesList.Add(new Costume(yetiGirl, "Yeti Girl", 260, 2000, true));
        costumesList.Add(new Costume(bigFoot, "Big Foot", 290, 2500, true));
        costumesList.Add(new Costume(sillyWizard, "Silly Wizard", 320, 3000, true));
        costumesList.Add(new Costume(captainFuzz, "Captain Fuzz", 400, 3500, true));
        costumesList.Add(new Costume(ghostYeti, "Ghost Yeti", 440, 4000, true));
        costumesList.Add(new Costume(rayYeti, "Ray Yeti", 480, 4500, true));
        costumesList.Add(new Costume(yetimon, "Yetimon", 550, 5300, true));
        costumesList.Add(new Costume(yetiWonka, "Yeti Wonka", 600, 6000, true));
        costumesList.Add(new Costume(devilYeti, "Devil Yeti", 666, 6500, true));
        costumesList.Add(new Costume(zombieYeti, "Zombie Yeti", 700, 7000, true));
        costumesList.Add(new Costume(yetiFloss, "Yeti Floss", 730, 7500, true));
        costumesList.Add(new Costume(rainbowYeti, "Rainbow Yeti", 760, 8000, true));
        costumesList.Add(new Costume(steamboatYeti, "Steamboat Yeti", 800, 9001, true));
        
    }

}

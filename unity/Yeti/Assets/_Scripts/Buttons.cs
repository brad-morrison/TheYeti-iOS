using UnityEngine;
using UnityEngine.SceneManagement;
using SA.iOS.StoreKit;

public class Buttons : TheYeti
{
    // listen to events
    private void OnEnable()
    {
        Actions.onButtonPressed += ButtonPress;
    }

    private void OnDisable()
    {
        Actions.onButtonPressed -= ButtonPress;
    }

    public void ButtonPress(string function) {

        switch(function) {
            case ButtonCommands.Replay:
                SceneManager.LoadScene("Main");
                break;

            case ButtonCommands.Settings:
                GM.mainMenu.ShowSettingsUI(true);
                GM.mainMenu.ShowMainUI(false);
                break;

            case ButtonCommands.CloseSettings:
                GM.mainMenu.ShowSettingsUI(false);
                GM.mainMenu.ShowMainUI(true);
                break;

            case ButtonCommands.Music:
                GM.mainMenu.Music(false);
                GM.audio.Music(false);
                break;

            case ButtonCommands.MusicMute:
                GM.mainMenu.Music(true);
                GM.audio.Music(true);
                break;

            case ButtonCommands.Sfx:
                GM.mainMenu.Sfx(false);
                GM.audio.sfxOn = false;
                break;

            case ButtonCommands.SfxMute:
                GM.mainMenu.Sfx(true);
                GM.audio.sfxOn = true;
                break;

            case ButtonCommands.CostumesNext:
                GM.costumeManager.NextCostume();
                break;
            
            case ButtonCommands.CostumesPrevious:
                GM.costumeManager.PreviousCostume();
                break;

            case ButtonCommands.CostumesSelect:
                if (GM.costumeManager.SetCostume())
                {
                    SceneManager.LoadScene("Menu");
                }
                break;

            case ButtonCommands.Costumes:
                SceneManager.LoadScene("Costumes");
                break;

            case ButtonCommands.Menu:
                SceneManager.LoadScene("Menu");
                break;

            case ButtonCommands.RemoveAds:
                break;

            case ButtonCommands.Restore:
                break;

            case ButtonCommands.Leaderboard:
                GM.leaderboards.ShowLeaderboards();
                break;

            case ButtonCommands.Rate:
                ISN_SKStoreReviewController.RequestReview();
                print("rating pressed");
                UnityEngine.iOS.Device.RequestStoreReview();
                break;

            default:
                Debug.LogWarning("Unknown button command: " + function);
                break;
        }
    }
    
}

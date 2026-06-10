using System;

public static class Actions
{
    public static event Action<string> onButtonPressed;

    public static void ButtonPressed(string function)
    {
        onButtonPressed?.Invoke(function);
    }
}

public static class ButtonCommands
{
    public const string Replay = "replay";
    public const string Settings = "settings";
    public const string CloseSettings = "close_settings";
    public const string Music = "music";
    public const string MusicMute = "music_mute";
    public const string Sfx = "sfx";
    public const string SfxMute = "sfx_mute";
    public const string CostumesNext = "costumes_next";
    public const string CostumesPrevious = "costumes_prev";
    public const string CostumesSelect = "costumes_select";
    public const string Costumes = "costumes";
    public const string Menu = "Menu";
    public const string RemoveAds = "remove_ads";
    public const string Restore = "restore";
    public const string Leaderboard = "leaderboard";
    public const string Rate = "rate";
}

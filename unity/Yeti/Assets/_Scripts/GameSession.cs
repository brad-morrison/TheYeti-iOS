public class GameSession
{
    public int Score { get; private set; }
    public int HighScore { get; private set; }
    public int RoundKills { get; private set; }
    public bool HasNewHighScore { get; private set; }

    public GameSession(int highScore)
    {
        Reset(highScore);
    }

    public void Reset(int highScore)
    {
        Score = 0;
        HighScore = highScore;
        RoundKills = 0;
        HasNewHighScore = false;
    }

    public void RegisterKill()
    {
        RoundKills++;
    }

    public bool AddScore(int amount)
    {
        Score += amount;

        if (Score <= HighScore)
            return false;

        HighScore = Score;
        HasNewHighScore = true;
        return true;
    }

    public int TotalKillsAfterRound(int storedKills)
    {
        return storedKills + RoundKills;
    }

    public bool ShouldSaveHighScore(int storedHighScore)
    {
        return Score > storedHighScore;
    }

    public void OverrideHighScore(int value)
    {
        HighScore = value;
    }

}

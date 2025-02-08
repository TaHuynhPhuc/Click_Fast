[System.Serializable]
public class PlayerRank
{
    public string namePlayer;
    public int scorePlayer;

    public PlayerRank(string namePlayer, int scorePlayer)
    {
        this.namePlayer = namePlayer;
        this.scorePlayer = scorePlayer;
    }
}

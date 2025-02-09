using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string username;
    public string password;
    public int score;

    public PlayerData(string username, string password, int score)
    {
        this.username = username;
        this.password = password;
        this.score = score;
    }
}
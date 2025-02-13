using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }
    public FirebaseRestClient firebaseClient;

    public TextMeshProUGUI textTenDangNhap;
    public TextMeshProUGUI textMatKhau;
    public TextMeshProUGUI textNotification;

    public List<PlayerData> playerData = new List<PlayerData>();
    private string userName;
    public int bestScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            firebaseClient = gameObject.GetComponent<FirebaseRestClient>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Login()
    {
        userName = textTenDangNhap.text.Trim();
        string password = textMatKhau.text.Trim();
        if (userName.Replace("\u200B", "") == "" || password.Replace("\u200B", "") == "")
        {
            textNotification.text = "Không được để trống tên đăng nhập và mật khẩu";
        }
        else
        {
            firebaseClient.CheckAccountExists(userName, (exists) =>
            {
                if (exists)
                {
                    firebaseClient.CheckPassword(userName, password, (isCorrect) =>
                    {
                        if (isCorrect)
                        {
                            SceneManager.LoadScene("Start");
                        }
                        else
                        {
                            textNotification.text = "Sai mật khẩu";
                        }
                    });
                }
                else
                {
                    SavePlayerData(textTenDangNhap.text, textMatKhau.text, 0);
                    SceneManager.LoadScene("Start");
                }
            });
        }
    }

    public void LoadTop30Players()
    {
        firebaseClient.GetAllPlayers((players) =>
        {
            var top30Players = players
                .OrderByDescending(player => player.score)
                .Take(30)
                .ToList();

            Debug.Log("===== Top 30 Players =====");
            for (int i = 0; i < top30Players.Count; i++)
            {
                Debug.Log($"{i + 1}. Username: {top30Players[i].username}, Score: {top30Players[i].score}");
                playerData.Add(top30Players[i]);
            }
        });
    }

    public int GetBestScore()
    {
        GetPlayerScore(userName);
        return bestScore;
    }

    public void UpdatePlayerScore(int newScore)
    {
        firebaseClient.UpdatePlayerScore(userName, newScore);
    }

    #region FirebaseFunciton
    private void SavePlayerData(string userName, string password, int score)
    {
        firebaseClient.SavePlayerData(userName, password, score);
    }

    private void GetPlayerScore(string userName)
    {
        firebaseClient.GetPlayerScore(userName);
    }
    #endregion
}
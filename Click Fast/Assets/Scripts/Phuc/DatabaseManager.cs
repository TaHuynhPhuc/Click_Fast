using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }
    private FirebaseRestClient firebaseClient;

    public TextMeshProUGUI textTenDangNhap;
    public TextMeshProUGUI textMatKhau;

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
        string userName = textTenDangNhap.text;
        string password = textMatKhau.text;

        Debug.Log(textTenDangNhap.text);
        Debug.Log(textMatKhau.text);
        firebaseClient.CheckAccountExists(userName, (exists) =>
        {
            if (exists)
            {
                Debug.Log("Tài khoản đã tồn tại.");
                SceneManager.LoadScene("Start");
            }
            else
            {
                Debug.Log("Tài khoản đã được tạo.");
                SavePlayerData(textTenDangNhap.text, textMatKhau.text, 0);
                SceneManager.LoadScene("Start");
            }
        });
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

    private void UpdatePlayerScore(string userName, int newScore)
    {
        firebaseClient.UpdatePlayerScore(userName, newScore);
    }
    #endregion
}
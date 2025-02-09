using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FirebaseRestClient : MonoBehaviour
{
    private string databaseURL = "https://clickfast-2adbd-default-rtdb.asia-southeast1.firebasedatabase.app/";

    // Lưu thông tin người chơi (username, password, score)
    public void SavePlayerData(string username, string password, int score)
    {
        string jsonData = JsonUtility.ToJson(new PlayerData(username, password, score));
        string url = $"{databaseURL}players/{username}.json";

        StartCoroutine(PostData(url, jsonData));
    }

    // Đọc điểm số người chơi
    public void GetPlayerScore(string username)
    {
        string url = $"{databaseURL}players/{username}/score.json";
        StartCoroutine(GetData(url));
    }

    // Cập nhật điểm số người chơi
    public void UpdatePlayerScore(string username, int newScore)
    {
        string url = $"{databaseURL}players/{username}/score.json";
        string jsonData = newScore.ToString();

        StartCoroutine(PatchData(url, jsonData));
    }

    public void CheckAccountExists(string username, System.Action<bool> callback)
{
    string url = $"{databaseURL}players/{username}.json";
    StartCoroutine(CheckAccountCoroutine(url, callback));
}

private IEnumerator CheckAccountCoroutine(string url, System.Action<bool> callback)
{
    UnityWebRequest request = UnityWebRequest.Get(url);

    yield return request.SendWebRequest();

    if (request.result == UnityWebRequest.Result.Success)
    {
        if (!string.IsNullOrEmpty(request.downloadHandler.text) && request.downloadHandler.text != "null")
        {
            Debug.Log("Tài khoản đã tồn tại.");
            callback?.Invoke(true);
        }
        else
        {
            Debug.Log("Tài khoản chưa tồn tại.");
            callback?.Invoke(false);
        }
    }
    else
    {
        Debug.LogError("Lỗi kiểm tra tài khoản: " + request.error);
        callback?.Invoke(false);
    }
}

    IEnumerator PostData(string url, string jsonData)
    {
        UnityWebRequest request = UnityWebRequest.Put(url, jsonData);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data saved successfully!");
        }
        else
        {
            Debug.LogError("Failed to save data: " + request.error);
        }
    }

    IEnumerator GetData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Player score: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Failed to get data: " + request.error);
        }
    }

    IEnumerator PatchData(string url, string jsonData)
    {
        UnityWebRequest request = UnityWebRequest.Put(url, jsonData);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score updated successfully!");
        }
        else
        {
            Debug.LogError("Failed to update score: " + request.error);
        }
    }
}

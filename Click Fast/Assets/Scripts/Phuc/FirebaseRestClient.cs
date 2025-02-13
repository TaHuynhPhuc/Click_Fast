using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using System;

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

    public void GetAllPlayers(System.Action<List<PlayerData>> callback)
    {
        string url = $"{databaseURL}players.json";
        StartCoroutine(GetAllPlayersData(url, callback));
    }
    public void CheckPassword(string username, string passwordToCheck, Action<bool> callback)
    {
        string url = $"{databaseURL}players/{username}.json";

        StartCoroutine(CheckPasswordCoroutine(url, passwordToCheck, callback));
    }

    private IEnumerator CheckPasswordCoroutine(string url, string passwordToCheck, Action<bool> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;

            if (!string.IsNullOrEmpty(jsonData) && jsonData != "null")
            {
                var playerData = JSON.Parse(jsonData);
                string storedPassword = playerData["password"];

                bool isPasswordCorrect = storedPassword == passwordToCheck;
                callback(isPasswordCorrect);
            }
            else
            {
                Debug.LogWarning("Player not found.");
                callback(false);
            }
        }
        else
        {
            Debug.LogError("Request failed: " + request.error);
            callback(false);
        }
    }

    private IEnumerator GetAllPlayersData(string url, System.Action<List<PlayerData>> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        List<PlayerData> playersList = new List<PlayerData>();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;
            if (!string.IsNullOrEmpty(jsonData) && jsonData != "null")
            {
                // Parse JSON using SimpleJSON
                var jsonObject = JSON.Parse(jsonData);

                foreach (var playerEntry in jsonObject)
                {
                    string username = playerEntry.Key;
                    var playerData = playerEntry.Value;

                    string password = playerData["password"];
                    int score = playerData["score"].AsInt;

                    playersList.Add(new PlayerData(username, password, score));
                }
            }
            else
            {
                Debug.Log("No player data found.");
            }
        }
        else
        {
            Debug.LogError("Failed to get player list: " + request.error);
        }

        callback?.Invoke(playersList);
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
            DatabaseManager.Instance.bestScore = int.Parse(request.downloadHandler.text);
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
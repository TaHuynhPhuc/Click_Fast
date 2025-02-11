using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class RankManager1 : MonoBehaviour
{
    public GameObject rankEntryPrefab;  // Prefab chứa TextMeshProUGUI
    public Transform rankContainer;     // Vị trí hiển thị bảng xếp hạng


    public List<PlayerData> listPlayerRank = new List<PlayerData>();

    void Start()
    {
        // PlayerPrefs.DeleteAll(); // ⚠️ Bật dòng này nếu muốn reset bảng xếp hạng

        LoadRanking();

        if (listPlayerRank.Count == 0)
        {
          
            SaveRanking();
        }

        UpdateUI();
    }

    // Thêm người chơi mới vào bảng xếp hạng
    public void AddPlayer(string name, int score)
    {
        //listPlayerRank.Add(new PlayerRank(name, score));
        Debug.Log($"📌 Đã thêm {name} với {score} điểm vào danh sách.");
        SortRank();
        SaveRanking();
        UpdateUI();
    }

    // Sắp xếp bảng xếp hạng theo điểm số giảm dần
    private void SortRank()
    {
        listPlayerRank = listPlayerRank.OrderByDescending(p => p.score).ToList();
    }

    // Cập nhật UI bảng xếp hạng
    private void UpdateUI()
    {
        if (rankContainer == null)
        {
            Debug.LogError("⚠ rankContainer chưa được gán trong Inspector!");
            return;
        }

        if (rankEntryPrefab == null)
        {
            Debug.LogError("⚠ rankEntryPrefab chưa được gán trong Inspector!");
            return;
        }

        // Xóa danh sách cũ
        foreach (Transform child in rankContainer)
        {
            Destroy(child.gameObject);
        }

        // Hiển thị danh sách mới
        for (int i = 0; i < listPlayerRank.Count; i++)
        {
            var player = listPlayerRank[i];  // Lấy người chơi thứ i trong danh sách
            GameObject newEntry = Instantiate(rankEntryPrefab, rankContainer);

            TextMeshProUGUI textComponent1 = newEntry.transform.Find("STT").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI textComponent2 = newEntry.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI textComponent3 = newEntry.transform.Find("Score").GetComponent<TextMeshProUGUI>();

            // Lấy component TextMeshProUGUI từ con của Prefab
            // List<TextMeshProUGUI> textComponent = new List<TextMeshProUGUI>();

            if (textComponent1 == null)
            {
                Debug.LogError("⚠ rankEntryPrefab không có TextMeshProUGUI Component!");
                continue;
            }

            // Format đẹp hơn + tô màu cho điểm số
            textComponent1.text = $"<b>#{i + 1}</b>";
            textComponent2.text = player.username;
            textComponent3.text = player.score.ToString();
            //    textComponent.fontSize = 38; // Tăng kích thước chữ
            //    textComponent.alignment = TextAlignmentOptions.Center; // Căn giữa text



            // Thêm khoảng cách giữa các hàng (dùng Layout Element)
            LayoutElement layout = newEntry.GetComponent<LayoutElement>();
            if (layout == null)
            {
                layout = newEntry.AddComponent<LayoutElement>();
            }
            layout.minHeight = 50; // Tăng chiều cao mỗi hàng
        }

        Debug.Log("✅ UI Cập nhật thành công!");
    }

    // Lưu bảng xếp hạng vào PlayerPrefs
    private void SaveRanking()
    {
        for (int i = 0; i < listPlayerRank.Count; i++)
        {
            PlayerPrefs.SetString($"PlayerName_{i}", listPlayerRank[i].username);
            PlayerPrefs.SetInt($"PlayerScore_{i}", listPlayerRank[i].score);
        }
        PlayerPrefs.SetInt("PlayerCount", listPlayerRank.Count);
    }

    // Load bảng xếp hạng từ PlayerPrefs
    private void LoadRanking()
    {
        //listPlayerRank.Clear();
        /*listPlayerRank.Add(new PlayerRank("tri",100));
        listPlayerRank.Add(new PlayerRank("thuan", 1400));
        listPlayerRank.Add(new PlayerRank("phuc", -100));*/
        listPlayerRank = DatabaseManager.Instance.playerData;
    }
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
}

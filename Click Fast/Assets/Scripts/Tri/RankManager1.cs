using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class RankManager1 : MonoBehaviour
{
    public GameObject rankEntryPrefab;  // Prefab chứa TextMeshProUGUI
    public Transform rankContainer;     // Vị trí hiển thị bảng xếp hạng

    private List<PlayerRank> listPlayerRank = new List<PlayerRank>();

    void Start()
    {
        // PlayerPrefs.DeleteAll(); // ⚠️ Bật dòng này nếu muốn reset bảng xếp hạng

        LoadRanking();

        if (listPlayerRank.Count == 0)
        {
            AddPlayer("Tri", 150);
            AddPlayer("Nam", 200);
            AddPlayer("Linh", 120);
            SaveRanking();
        }

        UpdateUI();
    }

    // Thêm người chơi mới vào bảng xếp hạng
    public void AddPlayer(string name, int score)
    {
        listPlayerRank.Add(new PlayerRank(name, score));
        Debug.Log($"📌 Đã thêm {name} với {score} điểm vào danh sách.");
        SortRank();
        SaveRanking();
        UpdateUI();
    }

    // Sắp xếp bảng xếp hạng theo điểm số giảm dần
    private void SortRank()
    {
        listPlayerRank = listPlayerRank.OrderByDescending(p => p.scorePlayer).ToList();
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

            // Lấy component TextMeshProUGUI từ con của Prefab
            TextMeshProUGUI textComponent = newEntry.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent == null)
            {
                Debug.LogError("⚠ rankEntryPrefab không có TextMeshProUGUI Component!");
                continue;
            }

            // Format đẹp hơn + tô màu cho điểm số
            textComponent.text = $"<b>#{i + 1}</b> {player.namePlayer}: <color=black>{player.scorePlayer} điểm</color>";
            textComponent.fontSize = 38; // Tăng kích thước chữ
            textComponent.alignment = TextAlignmentOptions.Center; // Căn giữa text

            


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
            PlayerPrefs.SetString($"PlayerName_{i}", listPlayerRank[i].namePlayer);
            PlayerPrefs.SetInt($"PlayerScore_{i}", listPlayerRank[i].scorePlayer);
        }
        PlayerPrefs.SetInt("PlayerCount", listPlayerRank.Count);
    }

    // Load bảng xếp hạng từ PlayerPrefs
    private void LoadRanking()
    {
        listPlayerRank.Clear();
        int playerCount = PlayerPrefs.GetInt("PlayerCount", 0);

        for (int i = 0; i < playerCount; i++)
        {
            string name = PlayerPrefs.GetString($"PlayerName_{i}", "");
            int score = PlayerPrefs.GetInt($"PlayerScore_{i}", 0);
            if (!string.IsNullOrEmpty(name))
            {
                listPlayerRank.Add(new PlayerRank(name, score));
            }
        }
    }
}

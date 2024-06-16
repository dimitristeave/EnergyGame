using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerLeaderboard : MonoBehaviour
{
    public List<TextMeshProUGUI> playerNameTexts; // Assign these in the inspector
    public List<TextMeshProUGUI> playerScoreTexts; // Assign these in the inspector
    private GameManagerDb gameManagerDb;

    void Start()
    {
        gameManagerDb = FindObjectOfType<GameManagerDb>();
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        PlayerDatabase db = gameManagerDb.LoadPlayerDatabase();
        Dictionary<string, PlayerInfo> players = db.ToDictionary();

        // Sort players by score in descending order
        var sortedPlayers = players.OrderByDescending(p => p.Value.score_total).Take(6).ToList();

        // Update the TextMeshProUGUI components
        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            playerNameTexts[i].text = sortedPlayers[i].Key;
            playerScoreTexts[i].text = sortedPlayers[i].Value.score_total.ToString();
        }

        // Clear remaining TextMeshProUGUI components if there are less than 6 players
        for (int i = sortedPlayers.Count; i < 6; i++)
        {
            playerNameTexts[i].text = "";
            playerScoreTexts[i].text = "";
        }
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ScoreBoard : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public float templateHeight = 4f;
    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        foreach (var entry in GetRandomEntries())
        {
            var rankString = entry.Position switch
            {
                1 => "1st",
                2 => "2nd",
                3 => "3rd",
                _ => entry.Position + "th"
            };
            
            var entryTransform = Instantiate(entryTemplate, entryContainer);
            var entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * (entry.Position - 1));
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("position").GetComponent<TextMeshProUGUI>().text = rankString;
            entryTransform.Find("player").GetComponent<TextMeshProUGUI>().text = entry.Player;
            entryTransform.Find("score").GetComponent<TextMeshProUGUI>().text = entry.Score.ToString();
        }
    }

    private List<ScoreEntry> GetRandomEntries()
    {
        var entryList = new List<ScoreEntry>();
        for (var i = 0; i < 10; i++)
        {
            var rank = i + 1;
            var player = "Player" + rank;
            var score = Random.Range(0, 100);
            entryList.Add(new ScoreEntry(rank, player, score));
        }

        return entryList;
    }

    private class ScoreEntry
    {
        public readonly int Score;
        public readonly int Position;
        public readonly string Player;

        public ScoreEntry(int position, string player, int score)
        {
            this.Position = position;
            this.Player = player;
            this.Score = score;
        }
    }
}

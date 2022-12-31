using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreBoard : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public float templateHeight = 4f;
    private List<ScoreEntry> _entryList;
    private List<Transform> _entryTransformList;
    private void Awake()
    {
        _entryList = GetRandomEntries();
        Save();
        Load();
        Draw();
    }

    private void Save()
    {
        var scores = new Scores { ScoreEntries = _entryList };
        var jsonScores = JsonUtility.ToJson(scores);
        PlayerPrefs.SetString("MarsScores", jsonScores);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        var jsonScores = PlayerPrefs.GetString("MarsScores");
        var scores = JsonUtility.FromJson<Scores>(jsonScores);
        _entryList = scores.ScoreEntries;
    }

    private void Draw()
    {
        entryTemplate.gameObject.SetActive(false);
        _entryTransformList = new List<Transform>();
        _entryList.Sort();
        for (int i = 0; i < 10 && i < _entryList.Count; i++)
        {
            _entryTransformList.Add(CreateScoreEntryTransform(i, _entryList[i]));
        }
    }

    private Transform CreateScoreEntryTransform(int position, ScoreEntry entry)
    {
        var rankString = position switch
        {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            _ => position + "th"
        };
            
        var entryTransform = Instantiate(entryTemplate, entryContainer);
        var entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * (position - 1));
        entryTransform.gameObject.SetActive(true);

        entryTransform.Find("position").GetComponent<TextMeshProUGUI>().text = rankString;
        entryTransform.Find("player").GetComponent<TextMeshProUGUI>().text = entry.player;
        entryTransform.Find("score").GetComponent<TextMeshProUGUI>().text = entry.score.ToString();

        return entryTransform;
    }

    private static List<ScoreEntry> GetRandomEntries()
    {
        var entryList = new List<ScoreEntry>();
        for (var i = 0; i < 10; i++)
        {
            var rank = i + 1;
            var player = "Player" + rank;
            var score = Random.Range(0, 100);
            entryList.Add(new ScoreEntry(player, score));
        }

        return entryList;
    }

    [Serializable]
    private class ScoreEntry : IComparable
    {
        public int score;
        public string player;

        public ScoreEntry(string player, int score)
        {
            this.player = player;
            this.score = score;
        }

        public int CompareTo(object obj)
        {
            return obj == null ? 1 : ((ScoreEntry)obj).score.CompareTo(this.score);
        }
    }
    
    private class Scores
    {
        public List<ScoreEntry> ScoreEntries;
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class TabletQuestions : MonoBehaviour
{
    public Transform questionTemplate;
    private List<QuestionEntry> _questionEntry;
    private List<Transform> _entryTransformList;
    private int _currentQuestion;
    private int score;
    private void Awake()
    {
        string nickname = PhotonNetwork.NickName;
        Debug.Log("Nume: "+ nickname);
        _questionEntry = ComputeQuestions();
        _currentQuestion = -1;
        score = 0;
    }


    private void Draw(int index)
    {
        questionTemplate.Find("Question").GetComponent<TextMeshProUGUI>().text = _questionEntry[index].question;
    }

    private void DrawFinal()
    {
        questionTemplate.Find("Question").GetComponent<TextMeshProUGUI>().text = "Congrats, " + PhotonNetwork.NickName + "!\n Your score is: " + score; 
    }

    public void OnClickTrue ()
    {
        _currentQuestion += 1;
        if (_currentQuestion < _questionEntry.Count)
        {
            Draw(_currentQuestion);
            if (_questionEntry[_currentQuestion].answer == true)
                score += 1;
        }
        else
        {
            DrawFinal();
        }
    }

    public void OnClickFalse()
    {
        _currentQuestion += 1;
        if (_currentQuestion < _questionEntry.Count) 
        {
            Draw(_currentQuestion);
            if (_questionEntry[_currentQuestion].answer == false)
                score += 1;
        }
        else
        {
            DrawFinal();
        }
        
    }

    private static List<QuestionEntry> ComputeQuestions()
    {
        var questionsList = new List<QuestionEntry>();
        questionsList.Add(new QuestionEntry("Phobos and Deimos are the 2 moons of Mars planet.", true));
        questionsList.Add(new QuestionEntry("Because the Mars is further away from the sun, its poles can reach temperatures of -140 Celsius degrees.", true));
        questionsList.Add(new QuestionEntry("Mars is the fifth planet from the sun in our Solar System.", false));
        questionsList.Add(new QuestionEntry("Mars is also known as the Blue Planet.", false));
        questionsList.Add(new QuestionEntry("You could jump around three times higher on Mars than you can on Earth.", true));
        questionsList.Add(new QuestionEntry("The home for the highest mountain in our solar system, Olympus Mons, is Mars.", true));

        return questionsList;
    }

    [Serializable]
    private class QuestionEntry
    {
        public string question;
        public bool answer;

        public QuestionEntry(string question, bool answer)
        {
            this.question = question;
            this.answer = answer;
        }
    }
}

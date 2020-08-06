using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    private static Text _text;

    private void Awake()
    {
        _text = GameObject.Find("HighScore").GetComponent<Text>();
        Load();
    }

    private class ScoreSaveObject 
    {
        public int Score;
    }
    
    public static void Save(int currentScore)
    {
        var so = new ScoreSaveObject
        {
            Score = PlayerStats.Score
        };
        if (File.Exists(Application.dataPath + "/highScore.txt"))
        {
            if (currentScore > JsonUtility.FromJson<ScoreSaveObject>(File.ReadAllText(Application.dataPath + "/highScore.txt")).Score)
            {
                File.WriteAllText(Application.dataPath + "/highScore.txt", JsonUtility.ToJson(so));
            }
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/highScore.txt", JsonUtility.ToJson(so));
        }
    }

    private static void Load()
    {
        if (File.Exists(Application.dataPath + "/highScore.txt"))
        {
            var so = JsonUtility.FromJson<ScoreSaveObject>(File.ReadAllText(Application.dataPath + "/highScore.txt"));

            _text.text = "HIGHSCORE " + so.Score;
        }
        else
        {
            _text.text = "HIGHSCORE 0";
        }
    }
}

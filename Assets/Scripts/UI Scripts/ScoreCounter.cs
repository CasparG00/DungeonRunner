using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Text scoreCounter;

    private void Update() => scoreCounter.text = "SCORE " + PlayerStats.Score;
}
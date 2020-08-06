using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int time;
    private Text _timerText;
    [SerializeField] private int panicTime;
    [SerializeField] private int fontSize;

    [Space] 
    [SerializeField] private GameObject player;

    [Space]
    
    public Camera mainCam;

    [SerializeField] private int camSize;

    private void Awake()
    {
        _timerText = GetComponent<Text>();
        StartCoroutine(TimerCounter());
    }

    private void Update()
    {
        _timerText.color = time <= panicTime ? Color.red : Color.white;
        _timerText.fontSize = time <= panicTime ? fontSize + (panicTime-time + 1) * 5 : fontSize;
        mainCam.orthographicSize = time <= panicTime ? camSize - (panicTime-time + 1) * 5 : camSize;
    }

    private IEnumerator TimerCounter()
    {
        while (time > 0 && PlayerStats.Health > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            _timerText.text = time.ToString();
        }
    }
}

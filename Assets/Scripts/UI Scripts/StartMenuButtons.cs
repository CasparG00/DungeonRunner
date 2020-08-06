using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuButtons : MonoBehaviour
{
    [SerializeField] private Text[] buttons;

    private int _selectedButton;

    private void Awake() => _selectedButton = 0;

    private void Update()
    {
        if (_selectedButton < buttons.Length - 1)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _selectedButton++;
            }
        } if (_selectedButton > 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _selectedButton--;
            }
        }

        foreach (var i in buttons)
        {
            i.color = Color.white;
        }

        buttons[0].text = File.Exists(Application.dataPath + "/save.txt") ? "CONTINUE" : "NO SAVE PRESENT";
        
        buttons[_selectedButton].color = Color.red;

        if (!Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.Return)) return;
        switch (_selectedButton)
        {
            case 0:
                if (File.Exists(Application.dataPath + "/save.txt"))
                {
                    Continue();
                }
                break;
            case 1:
                NewGame();
                break;
            case 2:
                Quit();
                break;
        }
    }

    private static void Continue()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        SaveGame.Load();
        PlayerStats.Floor++;
    }

    private static void NewGame()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        File.Delete(Application.dataPath + "/save.txt");
        PlayerStats.Score = 0;
        SaveGame.Load();
    }

    private static void Quit() => Application.Quit();
}
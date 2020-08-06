using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndExit : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.Backspace)) return;
        if (PlayerStats.Health > 0) SaveGame.Save();
        else
        {
            File.Delete(Application.dataPath + "/save.txt");
            SaveScore.Save(PlayerStats.Score);
        }
        SceneManager.LoadScene(0);
    }
}

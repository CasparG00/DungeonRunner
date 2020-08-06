using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int Health = 3;
    public static int Gold = 0;
    public static int Score = 0;
    public static int Floor = 1;
    
    public GameObject healthBar;
    public Sprite[] healthSprites;
    public GameObject deathScreen;
    public Timer timer;
    
    private Image _image;

    private void Start()
    {
        _image = healthBar.GetComponent<Image>();
        Health = Mathf.Clamp(Health, 0, 3);
    }

    private void Update()
    {
        switch (Health)
        {
            case 3:
                _image.sprite = healthSprites[Health];
                break;
            
            case 2:
                _image.sprite = healthSprites[Health];
                break;
                
            case 1:
                _image.sprite = healthSprites[Health];
                break;
            
            case 0:
                _image.sprite = healthSprites[Health];
                deathScreen.SetActive(true);
                Destroy(gameObject);
                break;
        }
        if (timer.time <= 0 && Health > 0) Health = 0;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public GameObject eventMenu;
    public GameObject interact;
    public GameObject optionsWrapper;
    public Text engageText;
    public Text leaveText;
    public GameObject descriptionBox;
    public Text descriptionText;
    public GameObject itemSprite;

    private bool _collided;
    private bool _firstSelected;
    private bool _eventExists;
    public Vector3 lastMove; //Inverse
    private PlayerMovement _pm;

    private GameObject _currEvent;
    private LootSpawner _currLs;
    private EncounterSpawner _currEs;
    private const float PlayerLevel = 50;
    private const float TimeForEncounterLevel = 1.5f;

    private Coroutine _lastRoutine;

    private void Start()
    {
        optionsWrapper.SetActive(false);
        _pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        eventMenu.SetActive(_collided);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && _collided)
        {
            _eventExists = _currEvent != null;

            //Player event interaction
            switch (interact.activeSelf)
            {
                case false:

                    //Pick up Loot
                    if (_firstSelected && _eventExists)
                    {
                        switch (_currEvent.name)
                        {
                            case "LootSpawner":
                            {
                                switch (_currLs.loot.name)
                                {
                                    case "cursed artifact":
                                        PlayerStats.Health--;
                                        PlayerStats.Score -= 100;
                                        break;
                                    case "coin":
                                        PlayerStats.Gold += _currLs.goldAmount;
                                        PlayerStats.Score += _currLs.goldAmount;
                                        break;
                                }

                                Destroy(_currEvent);
                                break;
                            }
                            case "EntryRoomMaster":
                                SaveGame.Save();
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                                PlayerStats.Floor++;
                                break;
                            case "EncounterSpawner":
                                if (PlayerLevel < _currEs.killChance)
                                {
                                    PlayerStats.Health--;
                                    PlayerStats.Score -= 100;
                                    _pm.Movement(lastMove);
                                }
                                else
                                {
                                    PlayerStats.Gold += _currEs.goldAmount;
                                    PlayerStats.Score += _currEs.goldAmount;
                                    Destroy(_currEvent);
                                }

                                break;
                        }
                    }
                    else
                    {
                        if (_eventExists)
                        {
                            if (_currEvent.name == "EncounterSpawner")
                            {
                                _pm.Movement(lastMove);
                            }
                        }
                    }

                    _pm.isInteracting = false;
                    optionsWrapper.SetActive(false);
                    break;
                //If player interacts with event
                case true:
                    optionsWrapper.SetActive(true);
                    _pm.isInteracting = true;
                    break;
            }

            interact.SetActive(false);
        }

        if (_collided && _currEvent.name == "EncounterSpawner")
        {
            interact.SetActive(false);
            optionsWrapper.SetActive(true);
            _pm.isInteracting = true;
        }

        if (_pm.isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) _firstSelected = true;
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) _firstSelected = false;
        }

        engageText.color = _firstSelected ? Color.red : Color.white;
        leaveText.color = _firstSelected ? Color.white : Color.red;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var tf = other.transform;
        if (!tf.CompareTag("Event") && tf.name != "EntryRoomMaster") return;
        _collided = true;

        _currEvent = other.gameObject;
        switch (tf.name)
        {
            case "LootSpawner":
            {
                _currLs = _currEvent.GetComponent<LootSpawner>();
                engageText.text = "LOOT";
                leaveText.text = "LEAVE";
                var loot = other.transform.GetComponent<LootSpawner>().loot;
                descriptionText.text = loot.name.ToUpper();
                itemSprite.GetComponent<Image>().sprite = loot;
                itemSprite.SetActive(true);
                descriptionBox.SetActive(true);
                break;
            }
            case "EncounterSpawner":
                _currEs = _currEvent.GetComponent<EncounterSpawner>();
                engageText.text = "ATTACK";
                leaveText.text = "RUN";
                _lastRoutine = StartCoroutine(EncounterWaitTime());
                itemSprite.SetActive(false);
                descriptionText.text = "SKELETON";
                break;
            case "EntryRoomMaster":
                engageText.text = "EXIT";
                leaveText.text = "STAY";
                descriptionBox.SetActive(false);
                itemSprite.SetActive(false);
                break;
        }

        interact.SetActive(true);
        optionsWrapper.SetActive(false);
        _firstSelected = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.CompareTag("Event") && other.transform.name != "EntryRoomMaster") return;
        _collided = false;
        _pm.isInteracting = false;
        if (_lastRoutine == null) return;
        StopCoroutine(_lastRoutine);
    }

    private IEnumerator EncounterWaitTime()
    {
        descriptionBox.SetActive(true);
        yield return new WaitForSeconds(TimeForEncounterLevel);
        descriptionText.text = PlayerLevel < _currEs.killChance ? "TOO STRONG" : "WEAK";
    }
}
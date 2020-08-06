using UnityEngine;

public class EncounterSpawner : MonoBehaviour
{
    public int killChance;
    public int goldAmount;

    private const int MinKillChance = 0;
    private const int MaxKillChance = 70;

    private void Start()
    {
        if (Random.value < 0.7f) Destroy(gameObject);
        killChance = Random.Range(MinKillChance, MaxKillChance);
        goldAmount = Random.Range(10, 50);
    }
}

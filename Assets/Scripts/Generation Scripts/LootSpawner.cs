using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    private LootManager _lm;
    public Sprite loot;
    public int goldAmount;

    private void Start()
    {
        _lm = GameObject.Find("LootManager").GetComponent<LootManager>();

        loot = _lm.loot[Random.Range(0, _lm.loot.Length)];
        if (loot.name == "coin")
        {
            goldAmount = Random.Range(10, 100);
        }
    }
}

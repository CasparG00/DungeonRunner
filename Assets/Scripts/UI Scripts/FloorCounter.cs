using UnityEngine;
using UnityEngine.UI;

public class FloorCounter : MonoBehaviour
{
    [SerializeField] private Text floorCounter;

    private void Update() => floorCounter.text = "FLOOR " + PlayerStats.Floor;
}

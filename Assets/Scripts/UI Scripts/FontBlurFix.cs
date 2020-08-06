using UnityEngine;

public class FontBlurFix : MonoBehaviour
{
    public Font[] fonts;

    private void Start()
    {
        foreach (var font in fonts)
        {
            font.material.mainTexture.filterMode = FilterMode.Point;
        }
    }
}

using UnityEngine;

public class Brick : MonoBehaviour
{
    private int rowIndex = 0;

    // Called by the spawner
    public void SetRowIndex(int index)
    {
        rowIndex = index;
    }

    private void Start()
    {
        ApplyRainbowColorByRow();
    }

    private void ApplyRainbowColorByRow()
    {
        float hue = (rowIndex % 7) / 7f;  // Loop through 7 rainbow hues
        Color rowColor = Color.HSVToRGB(hue, 1f, 1f);

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = rowColor;
        }
    }
}

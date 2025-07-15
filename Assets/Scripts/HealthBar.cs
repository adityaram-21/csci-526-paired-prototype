using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public RectTransform fillBar; // Assign the Fill image's RectTransform
    public Image fillImage;       // The Fill Image (for color change)

    public float maxWidth; // Adjust based on your bar width in Canvas

    public void SetHealth(float current, float max)
    {
        float percent = Mathf.Clamp01(current / max);
        fillBar.sizeDelta = new Vector2(maxWidth * percent, fillBar.sizeDelta.y); // Resize the fill bar
        fillImage.color = Color.Lerp(Color.red, Color.green, percent); // Linear interpolation for color change
    }
}
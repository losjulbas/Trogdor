using UnityEngine;

public class OpacityController : MonoBehaviour {
    public float opacity = 1f; // Range from 0 (fully transparent) to 1 (fully opaque)

    void Update() {
        // Update the opacity of the child sprites every frame
        SetOpacity(opacity);
    }

    public void SetOpacity(float newOpacity) {
        // Clamp the opacity value
        newOpacity = Mathf.Clamp(newOpacity, 0f, 1f);
        opacity = newOpacity;

        // Get all SpriteRenderer components in the child objects
        SpriteRenderer[] childSprites = GetComponentsInChildren<SpriteRenderer>();

        // Iterate through each SpriteRenderer and set its color with the new opacity
        foreach (SpriteRenderer sprite in childSprites) {
            Color color = sprite.color;
            color = new Color(0, 0, 0);
            color.a = opacity; // Set the alpha to the new opacity value
            sprite.color = color; // Apply the new color
        }
    }
}

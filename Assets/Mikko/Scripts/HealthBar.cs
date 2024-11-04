using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public GameObject scalingBar;
    public SpriteRenderer scalableBar;
    public SpriteRenderer bgBar;


    void Awake() {
        HideTheBar();
    }

    public void RevealTheBar() {
        var scalableBarColor = scalableBar.color;
        scalableBarColor.a = 1f;
        scalableBar.color = scalableBarColor;

        var bgBarColor = bgBar.color;
        bgBarColor.a = 0.6f;
        bgBar.color = bgBarColor;
    }

    public void UpdateBar(float prosentti ) {
        var barScale = scalingBar.transform.localScale;
        barScale.x = prosentti;
        scalingBar.transform.localScale = barScale;
    }

    public void HideTheBar() {
        var scalableBarColor = scalableBar.color;
        scalableBarColor.a = 0f;
        scalableBar.color = scalableBarColor;

        var bgBarColor = bgBar.color;
        bgBarColor.a = 0f;
        bgBar.color = bgBarColor;
    }

}

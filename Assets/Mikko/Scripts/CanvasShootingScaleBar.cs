using UnityEngine;
using UnityEngine.UI;

public class CanvasShootingScaleBar : MonoBehaviour
{
    DragonShooter dragonShooterScript;
    float barSize;
    public GameObject squareLiikkuva;
    Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() {
        dragonShooterScript = FindFirstObjectByType<DragonShooter>();
        barSize = dragonShooterScript.workingShootingDuration / dragonShooterScript.maxShootingDuration;

        image = squareLiikkuva.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        //barSize = (dragonShooterScript.maxShootingDuration - dragonShooterScript.workingShootingDuration) / dragonShooterScript.maxShootingDuration;
        barSize = dragonShooterScript.workingShootingDuration / dragonShooterScript.maxShootingDuration;

        var barScale = transform.localScale;
        barScale.x = barSize;
        transform.localScale = barScale;
        /*
        if (dragonMovementScript.accelerationBlock) {
            var color = image.color;
            color.a = 0.2f;
            image.color = color;
        }
        else {
            var color = image.color;
            color.a = 1;
            image.color = color;
        }*/


    }
}

using UnityEngine;
using UnityEngine.UI;

public class CanvasDragonHealth : MonoBehaviour
{
    ScuffedDragon scuffedDragonScript;
    float barSize;
    int dragonHealthMax;
    public GameObject squareLiikkuva;
    Image image;
    void Awake() {
        scuffedDragonScript = FindFirstObjectByType<ScuffedDragon>();
        dragonHealthMax = scuffedDragonScript.hitpoints;
        //barSize = dragonMovementScript.workingAccelerationStamina / (float)dragonMovementScript.maxAccelerationStamina;
        barSize = scuffedDragonScript.hitpoints / (float)dragonHealthMax;

        image = squareLiikkuva.GetComponent<Image>();
    }

    void Update() {
        barSize = scuffedDragonScript.hitpoints / (float)dragonHealthMax;
        var barScale = transform.localScale;
        barScale.x = barSize;
        transform.localScale = barScale;

       


    }

}

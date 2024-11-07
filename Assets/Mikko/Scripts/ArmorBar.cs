using UnityEngine;

public class ArmorBar : MonoBehaviour
{

    public ScuffedDragon scuffedDragonScripti;
    //public GameObject armorPowerUpSprite;
    public GameObject scalingBar;
    


    public float prosentti;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        scuffedDragonScripti = GetComponent<ScuffedDragon>();
        //mittari.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

            CalculateArmorBar();

    }

    void CalculateArmorBar(){
        prosentti = (scuffedDragonScripti.powerupDuration-scuffedDragonScripti.timer) / scuffedDragonScripti.powerupDuration;
        var barScale = scalingBar.transform.localScale;
        barScale.x = prosentti;
        scalingBar.transform.localScale = barScale;
        //mittari.SetActive(true);



    }


    



}

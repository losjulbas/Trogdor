using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class DragonShooter : MonoBehaviour
{
    public GameObject shootingObject;
    public GameObject fireBallPrefab;
    public KeyCode shootingKey = KeyCode.Space;

    public float fireBallLife; 
    
    public float fireBallSetSpeed;
    public float fireBallCurrentSpeed;
    
    
    public Vector2 fireballSizeMinMax;
    public float maxShootingDuration; // tämä on kuinka paljon staminaa (eli ampuma-aikaa) lohikäärmeellä on
    public float workingShootingDuration;

    public float shootingFrequenzy; // tämä kuinka tiiviisti tulipalloja tulee, x sekunnin välein
    float workingShootingFrequenzy;
    
    DragonMovement dragonMovement;
  

void Start(){
    workingShootingFrequenzy = shootingFrequenzy;
    workingShootingDuration = maxShootingDuration;
    
    dragonMovement = GetComponent<DragonMovement>();
  
}

   
    void Update()
    {
        fireBallCurrentSpeed = fireBallSetSpeed+dragonMovement.workingSpeed*1.3f;

        if (Input.GetKey(shootingKey)){
            
            workingShootingFrequenzy -= Time.deltaTime;
            if (workingShootingFrequenzy<0 && workingShootingDuration>0){
                workingShootingDuration -= Time.deltaTime;
                workingShootingFrequenzy = shootingFrequenzy;
                GameObject fireBall = Instantiate<GameObject>(fireBallPrefab);
                fireBall.transform.position = shootingObject.transform.position;
                fireBall.transform.rotation = shootingObject.transform.rotation;


            }
            
        } 
        
        if (!Input.GetKey(shootingKey) && workingShootingDuration <maxShootingDuration){
            workingShootingDuration += Time.deltaTime;
        }
        
    }
}

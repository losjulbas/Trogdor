using UnityEngine;

public class FireBall : MonoBehaviour
{
   float fireBallSpeed;
   float fireBallLife;
   float fireBallMinSize;
   float fireBallMaxSize;
   Vector3 localScale;

   public AnimationCurve fireBallScaleCurve;
   public AnimationCurve fireBallSpeedCurve;
   public AnimationCurve fireBallOpacityCurve;

   float scaleTime;
   float randomFireBallLife;
   float randomGreen;
   

    // Wiggle variables
    public float wiggleIntensity = 0.1f; // Adjust this to control wiggle strength
    private float wiggleOffsetX;
    private float wiggleOffsetY;


    SpriteRenderer spriteRenderer;

    
   

    void Awake()
    {
        randomFireBallLife = Random.Range(0.3f,1f);

        fireBallLife = FindFirstObjectByType<DragonShooter>().fireBallLife*randomFireBallLife;
        fireBallSpeed = FindFirstObjectByType<DragonShooter>().fireBallCurrentSpeed;
        fireBallMinSize=FindFirstObjectByType<DragonShooter>().fireballSizeMinMax.x;
        fireBallMaxSize=FindFirstObjectByType<DragonShooter>().fireballSizeMinMax.y*randomFireBallLife;
        
        localScale = new Vector3(fireBallMinSize,fireBallMinSize,fireBallMinSize);
        transform.localScale = localScale;
        
        Destroy (gameObject, fireBallLife);

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        randomGreen = Random.Range(0.4f,0.6f);

        

        
    }

    void Update()
    {
        scaleTime+= Time.deltaTime/fireBallLife;
        float curveValueScale = fireBallScaleCurve.Evaluate(scaleTime)*fireBallMaxSize;
        transform.localScale = new Vector3 (curveValueScale,curveValueScale,curveValueScale);
        
        
        float curveValueOpacity = fireBallOpacityCurve.Evaluate(scaleTime);

        var colour = spriteRenderer.color;
        colour.a = curveValueOpacity;
        colour.g = randomGreen;
        spriteRenderer.color = colour;


        
        float curveValueSpeed = fireBallSpeedCurve.Evaluate(scaleTime)*fireBallSpeed;
        transform.position += transform.right * curveValueSpeed * Time.deltaTime;

        // Adding random wiggle effect
        float wiggleX = (Mathf.PerlinNoise(wiggleOffsetX, Time.time * 5f) - 0.5f) * wiggleIntensity;
        float wiggleY = (Mathf.PerlinNoise(wiggleOffsetY, Time.time * 5f) - 0.5f) * wiggleIntensity;


        transform.position += new Vector3(wiggleX, wiggleY, 0);
    
         

    }
}

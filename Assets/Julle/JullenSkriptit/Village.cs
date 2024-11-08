using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Village : MonoBehaviour, IDamageable
{

    public float tickTime = 2f;
    float timer = 0f;
    public GameObject arrowPrefab;
    [SerializeField] float sightDistance;
    ScuffedDragon scuffedDragon;
    public int hitpoints = 2;
    int maxHitpoints; //Mikko
    public Sprite undestroyedVillage;
    public Sprite destroyedVillage;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    ScoreManager scoreManager;
    public GameObject smokeEffectPrefab;
    public GameObject soulEffectPrefab;
    private bool isDestroyed = false;

    public HealthBar healthBar; // Mikko
    SimpleAudioSource audioSource;

    void Start() {
        healthBar.UpdateBar(hitpoints / (float)maxHitpoints); //mikko
    }

    void Awake()
    {
        maxHitpoints = hitpoints; //Mikko
        spriteRenderer = GetComponent<SpriteRenderer>();
        scuffedDragon = FindAnyObjectByType<ScuffedDragon>();
        boxCollider = GetComponent<BoxCollider2D>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        audioSource = FindAnyObjectByType<SimpleAudioSource>();

    }

    void Update()
    {
        IsPlayerVisible();
    }

    void ShootArrow()
    {

        timer += Time.deltaTime;

        while (timer > tickTime)
        {
            //audioSource.PlaySound("ArrowTower");
            var newArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            timer -= tickTime; // “spend” one tickTime, don’t go to zero
        }
    }

    // Check radius on scene only for debug purposis
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, sightDistance);
    }
    void IsPlayerVisible()
    {

        // Check if scuffedDragon is null before accessing it
        if (scuffedDragon == null)
        {
            return;
        }

        // Get the dragon's collider
        PolygonCollider2D dragonCollider = scuffedDragon.GetComponent<PolygonCollider2D>();

        // Check if any part of the dragon's collider overlaps with the sight radius
        if (Physics2D.OverlapCircle(transform.position, sightDistance, LayerMask.GetMask("Dragon")) == dragonCollider)
        {
            ShootArrow();
        }



        //Vector2 pos = scuffedDragon.transform.position;
        //if (Vector2.Distance(transform.position, pos) > sightDistance)
        //{
        //    return;
        //}
        //else
        //{
        //    ShootArrow();
        //}
    }

    void HandleDestruction()
    {

        if (isDestroyed) return;

        isDestroyed = true;
        StartCoroutine(DestructionSequence());
    }

    private IEnumerator DestructionSequence()
    {
        GameObject smokeEffect = Instantiate(smokeEffectPrefab, transform.position, Quaternion.identity);
        Destroy(smokeEffect, 2f);  // Destroy the effect after 2 seconds
        yield return new WaitForSeconds(0.3f);  // Wait for smoke effect to finish
        audioSource.PlaySound("DestroyedVillage");
        spriteRenderer.sprite = destroyedVillage;

        GameObject soulEffect = Instantiate(soulEffectPrefab);
        soulEffect.transform.position = transform.position;
        Destroy(soulEffect, 4f); 
        yield return new WaitForSeconds(0.1f);  

        //// Disable this script
        this.enabled = false;
    }



    public void TakeDamage(int amount)
    {
        if (isDestroyed) return;
        if (hitpoints > 0)
        {
            hitpoints -= amount;
            scoreManager.AddScore(100);
            healthBar.RevealTheBar(); // Mikko
            healthBar.UpdateBar(hitpoints / (float)maxHitpoints);// Mikko
        }
        if (hitpoints <= 0)
        {
            healthBar.HideTheBar(); // Mikko
            HandleDestruction();
        }
    }
}

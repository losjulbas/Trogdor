using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SheepFarm : MonoBehaviour, IDamageable
{
    public int hitpoints = 2;
    int maxHitpoints; // Mikko
    public Sprite undestroyedSheepfarm;
    public Sprite destroyedSheepfarm;
    SpriteRenderer spriteRenderer;
    ScoreManager scoreManager;
    PolygonCollider2D polygonCollider;
    public GameObject smokeEffectPrefab;
    public GameObject soulEffectPrefab;
    public bool isVillage = false;
    public bool isSheepfarm = false;
    public bool isCastle = false;
    public float powerupChance = 0.9f;
    public GameObject powerupToSpawn;
    SimpleAudioSource audioSource;
    GameManager gameManager;

    private bool isDestroyed = false;

    public HealthBar healthBar; //Mikko


    void Start() {
        healthBar.UpdateBar(hitpoints/(float)maxHitpoints); //mikko
    }


    void Awake()
    {
        maxHitpoints = hitpoints; //Mikko
        spriteRenderer = GetComponent<SpriteRenderer>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        audioSource = FindAnyObjectByType<SimpleAudioSource>();
        gameManager = FindAnyObjectByType<GameManager>();
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

        spriteRenderer.sprite = destroyedSheepfarm;

        // Check if this instance can spawn a power-up and spawn it if the chance succeeds
        if (isVillage)
        {
            audioSource.PlaySound("DestroyedVillage");
            Debug.Log("PLayed destroyed village sound");
            Debug.Log("Checking for power-up spawn...");
            if (Random.value < powerupChance)
            {
                Debug.Log("Power-up spawning!");
                var Armorpowerup = Instantiate(powerupToSpawn, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Power-up not spawned due to random chance.");
            }
        }

        if (isSheepfarm == true)
        {
            audioSource.PlaySound("Sheep");

            if (powerupToSpawn != null)
            {
                Debug.Log("instantiating health powerup!");
                var healthPowerup = Instantiate(powerupToSpawn, transform.position, Quaternion.identity);
                healthPowerup.transform.position = transform.position;
            }

        }

        if (isCastle == true)
        {
            gameManager.GameWon();
            audioSource.PlaySound("Trumpets");
        }

        GameObject soulEffect = Instantiate(soulEffectPrefab);
        soulEffect.transform.position = transform.position;
        Destroy(soulEffect, 4f);
        yield return new WaitForSeconds(0.1f);


        // Disable this script and collider
        polygonCollider.enabled = false;
        this.enabled = false;
    }


    public void TakeDamage(int amount)
    {
        if (isDestroyed) return;
        if (hitpoints > 0)
        {
            hitpoints -= amount;
            scoreManager.AddScore(50);
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

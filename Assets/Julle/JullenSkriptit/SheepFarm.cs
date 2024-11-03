using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SheepFarm : MonoBehaviour, IDamageable
{
    [SerializeField] private int hitpoints = 2;
    public Sprite undestroyedSheepfarm;
    public Sprite destroyedSheepfarm;
    SpriteRenderer spriteRenderer;
    ScoreManager scoreManager;
    PolygonCollider2D polygonCollider;
    public GameObject smokeEffectPrefab;
    public GameObject soulEffectPrefab;
    public bool canSpawnPowerup = false;
    public float powerupChance = 0.9f;
    public GameObject powerupToSpawn;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void HandleDestruction()
    {
        StartCoroutine(DestructionSequence());
    }

    private IEnumerator DestructionSequence()
    {
        GameObject smokeEffect = Instantiate(smokeEffectPrefab, transform.position, Quaternion.identity);
        Destroy(smokeEffect, 2f);  // Destroy the effect after 2 seconds
        yield return new WaitForSeconds(0.3f);  // Wait for smoke effect to finish

        spriteRenderer.sprite = destroyedSheepfarm;

        GameObject soulEffect = Instantiate(soulEffectPrefab);
        soulEffect.transform.position = transform.position;
        Destroy(soulEffect, 4f);
        yield return new WaitForSeconds(0.1f);

        // Check if this instance can spawn a power-up and spawn it if the chance succeeds
        if (canSpawnPowerup && Random.value < powerupChance)
        {
            var powerup = Instantiate(powerupToSpawn);
            powerup.transform.position = transform.position;
        }

        // Disable this script and collider
        polygonCollider.enabled = false;
        this.enabled = false;
    }

    public void TakeDamage(int amount)
    {
        if (hitpoints > 0)
        {
            hitpoints -= amount;
            scoreManager.AddScore(50);
        }
        if (hitpoints <= 0)
        {
            HandleDestruction();
        }
    }
}

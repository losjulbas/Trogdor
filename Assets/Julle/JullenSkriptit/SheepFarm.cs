using UnityEngine;
using UnityEngine.SceneManagement;

public class SheepFarm : MonoBehaviour, IDamageable
{
    public int hitpoints = 2;
    int maxHitpoints; // Mikko
    public Sprite undestroyedSheepfarm;
    public Sprite destroyedSheepfarm;
    SpriteRenderer spriteRenderer;
    ScoreManager scoreManager;

    public HealthBar healthBar; //Mikko


    void Start() {
        healthBar.UpdateBar(hitpoints/(float)maxHitpoints); //mikko
    }


    void Awake()
    {
        maxHitpoints = hitpoints; //Mikko
        spriteRenderer = GetComponent<SpriteRenderer>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    void HandleDestruction()
    {
        spriteRenderer.sprite = destroyedSheepfarm;
        this.enabled = false;

    }


    public void TakeDamage(int amount)
    {
        
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

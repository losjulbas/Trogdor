using UnityEngine;
using UnityEngine.SceneManagement;

public class SheepFarm : MonoBehaviour, IDamageable
{
    [SerializeField] private int hitpoints = 2;
    public Sprite undestroyedSheepfarm;
    public Sprite destroyedSheepfarm;
    SpriteRenderer spriteRenderer;
    ScoreManager scoreManager;

    void Awake()
    {
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
        }
        if (hitpoints <= 0)
        {
            HandleDestruction();
        }
    }
}

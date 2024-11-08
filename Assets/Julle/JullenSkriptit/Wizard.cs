using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Wizard : MonoBehaviour, IDamageable
{

    public float tickTime = 2f;
    float timer = 0f;
    [SerializeField] float sightDistance;
    ScuffedDragon scuffedDragon;

    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider;

    ScoreManager scoreManager;
    public GameObject smokeEffectPrefab;
    public GameObject soulEffectPrefab;
    public GameObject spellPrefab;

    [SerializeField] private int hitpoints = 2;
    int maxHitpoints; // Mikko

    public Sprite undestroyedWizard;
    public Sprite destroyedWizard;

    public HealthBar healthBar; //Mikko
    SimpleAudioSource audioSource;

    void Start() {
        healthBar.UpdateBar(hitpoints / (float)maxHitpoints); //mikko
    }

    void Awake()
    {
        maxHitpoints = hitpoints; //Mikko
        scuffedDragon = FindAnyObjectByType<ScuffedDragon>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = FindAnyObjectByType<SimpleAudioSource>();

    }

    void Update()
    {
        IsPlayerVisible();
    }

    void CastSpell()
    {

        timer += Time.deltaTime;

        while (timer > tickTime)
        { // might need more than one tick!
            //audioSource.PlaySound("WizardCasting");
            var newSpell = Instantiate(spellPrefab, transform.position, Quaternion.identity);

            timer -= tickTime; // �spend� one tickTime, don�t go to zero
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
        Collider2D dragonCollider = scuffedDragon.GetComponent<Collider2D>();

        // Check if any part of the dragon's collider overlaps with the sight radius
        if (Physics2D.OverlapCircle(transform.position, sightDistance, LayerMask.GetMask("Dragon")) == dragonCollider)
        {
            TurnTowardsPlayer();
            CastSpell();
        }
    }

    void TurnTowardsPlayer()
    {
        Vector3 pos = scuffedDragon.transform.position;

        //calculate direction to shoot the arrow
        var direction = (pos - transform.position).normalized;
        var targetRot = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = targetRot;
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
        audioSource.PlaySound("DestroyedVillage");
        spriteRenderer.sprite = destroyedWizard;

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

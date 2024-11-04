using UnityEngine;
using UnityEngine.Audio;

public class ScuffedDragon : MonoBehaviour
{

    [SerializeField] private int hitpoints = 5;
    [SerializeField] private int armorHitpoints;
    //public GameObject fireballPrefab;
    GameManager gameManager;
    //public float speed;
    public float powerupDuration = 2f;
    float timer = 0f;
    PowerupType currentPowerup = PowerupType.None;
    public GameObject armorPowerupSprite; // Reference to the armor sprite
    SimpleAudioSource audioSource;
    public GameObject hitmarkEffect;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        armorPowerupSprite.SetActive(false);
        audioSource = FindAnyObjectByType<SimpleAudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentPowerup == PowerupType.Armor && armorHitpoints > 0)
        {
            armorHitpoints--;
        }
        else if (hitpoints > 0)
        {
            
            hitpoints--;
        }
        else if (hitpoints <= 0)
        {
            HandleDestruction();
        }

        // Instantiate hitmarkEffect at the contact point
        if (collision.contacts.Length > 0)  // Check if there are any contact points
        {
            Vector2 contactPoint = collision.contacts[0].point;  // Get the first contact point
            GameObject soulEffect = Instantiate(hitmarkEffect, contactPoint, Quaternion.identity);
            Destroy(soulEffect, 2f);  // Destroy the effect after a delay if needed
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) // TÄTÄ EI TARVITA
        //{
        //    print("Fireball spawned!");
        //    var newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        //    Vector2 direction = transform.up;  // Use the direction the dragon is facing
        //    newFireball.GetComponent<Fireball>().Initialize(direction);
        //}

        timer += Time.deltaTime;
        if (currentPowerup != PowerupType.None && timer > powerupDuration)
        {
            EndPowerup(currentPowerup);
            currentPowerup = PowerupType.None;
        }
    }



    public void PowerupActivated(PowerupType whichType)
    {
        timer = 0f;
        EndPowerup(currentPowerup);
        StartPowerup(whichType);
        currentPowerup = whichType;
    }
    void StartPowerup(PowerupType powerup)
    {
        if (powerup == PowerupType.Armor)
        {
            print("Armor activated!");
            armorHitpoints += 10;
            armorPowerupSprite.SetActive(true);
        }
    }
    void EndPowerup(PowerupType powerup)
    {
        print("Armor deactivated!");
        armorHitpoints = 0;
        armorPowerupSprite.SetActive(false);
    }

    void HandleDestruction()
    {
        audioSource.PlaySound("DeadDragon");
        gameManager.GameLost();
        Destroy(gameObject);
    }
}

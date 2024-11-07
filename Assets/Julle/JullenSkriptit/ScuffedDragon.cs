using UnityEngine;
using UnityEngine.Audio;

public class ScuffedDragon : MonoBehaviour
{

    public int hitpoints = 5;
    [SerializeField] private int armorHitpoints;
    //public GameObject fireballPrefab;
    GameManager gameManager;
    //public float speed;
    public float powerupDuration = 2f;
    public float timer = 0f;
    PowerupType currentPowerup = PowerupType.None;
    public GameObject armorPowerupSprite; // Reference to the armor sprite
    SimpleAudioSource audioSource;
    public GameObject hitmarkEffect;

    private bool isCollidingWithWall = false;


    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        armorPowerupSprite.SetActive(false);
        audioSource = FindAnyObjectByType<SimpleAudioSource>();
        //boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Powerup"))
        {
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            return;
        }

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

        // Instantiate hitmarkEffect at the position of the dragon
        Vector2 effectPosition = (Vector2)transform.position; // Use the dragon's position
        GameObject soulEffect = Instantiate(hitmarkEffect, effectPosition, Quaternion.identity);
        Destroy(soulEffect, 2f);  // Destroy the effect after a delay if needed
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
    //    {
    //        FindObjectOfType<DragonMovement>().SetCollisionState(false);
    //    }
    //}

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) // T�T� EI TARVITA
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

        if (currentPowerup == PowerupType.Armor && armorHitpoints <= 0)
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
        if (powerup == PowerupType.Health)
        {
            print("Hitpoints increased!");
            hitpoints += 10;
        }
    }
    void EndPowerup(PowerupType powerup)
    {
        if (powerup == PowerupType.Armor)
        {
            print("Armor deactivated!");
            armorHitpoints = 0;
            armorPowerupSprite.SetActive(false);
        }
    }

    void HandleDestruction()
    {
        gameManager.GameLost();
        Destroy(gameObject);
    }
}

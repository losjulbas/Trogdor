using UnityEngine;

public class ScuffedDragon : MonoBehaviour
{

    [SerializeField] private int hitpoints = 2;
    public GameObject fireballPrefab;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitpoints > 0)
        {
            hitpoints--;
        }
        if (hitpoints <= 0)
        {
            HandleDestruction();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Fireball spawned!");
            var newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            Vector2 direction = transform.up;  // Use the direction the dragon is facing
            newFireball.GetComponent<Fireball>().Initialize(direction);
        }
    }
    void HandleDestruction()
    {
        gameManager.GameLost();
        Destroy(gameObject);
    }
}

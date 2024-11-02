using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Village : MonoBehaviour, IDamageable
{

    public float tickTime = 2f;
    float timer = 0f;
    public GameObject arrowPrefab;
    [SerializeField] float sightDistance;
    ScuffedDragon scuffedDragon;
    [SerializeField] private int hitpoints = 2;
    public Sprite undestroyedVillage;
    public Sprite destroyedVillage;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    ScoreManager scoreManager;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        scuffedDragon = FindAnyObjectByType<ScuffedDragon>();
        boxCollider = GetComponent<BoxCollider2D>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    void Update()
    {
        IsPlayerVisible();
    }

    void ShootArrow()
    {

        timer += Time.deltaTime;

        while (timer > tickTime)
        { // might need more than one tick!
            print("Arrow spawned!");
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
        Collider2D dragonCollider = scuffedDragon.GetComponent<Collider2D>();

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
        spriteRenderer.sprite = destroyedVillage;
        this.enabled = false; //disable this script

    }

    public void TakeDamage(int amount)
    {
        if (hitpoints > 0)
        {
            hitpoints -= amount;
            scoreManager.AddScore(100);
        }
        if (hitpoints <= 0)
        {
            HandleDestruction();
        }
    }
}

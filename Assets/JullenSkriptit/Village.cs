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

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        scuffedDragon = FindAnyObjectByType<ScuffedDragon>();
        boxCollider = GetComponent<BoxCollider2D>();
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

            //float x = transform.position.x;
            //float y = transform.position.y;
            //newArrow.transform.position = new Vector3(x, y, 0);
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
        Vector2 pos = scuffedDragon.transform.position;

        // This checks if the player is within the detection radius
        if (Vector2.Distance(transform.position, pos) > sightDistance)
        {
            return;
        }
        else
        {
            ShootArrow();
        }
    
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (hitpoints > 0)
    //    {
    //        hitpoints--;
    //    }
    //    if (hitpoints <= 0)
    //    {
    //        HandleDestruction();
    //    }
    //}

    void HandleDestruction()
    {
        spriteRenderer.sprite = destroyedVillage;
        this.enabled = false;

    }

    public void TakeDamage(int amount)
    {
        if (hitpoints > 0)
        {
            hitpoints -= amount;
        }
        if (hitpoints <= 0)
        {
            HandleDestruction();
        }
    }
}

using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour
{

    public float tickTime = 2f;
    float timer = 0f;
    public GameObject spellPrefab;
    [SerializeField] float sightDistance;
    ScuffedDragon scuffedDragon;

    BoxCollider2D boxCollider;
    ScoreManager scoreManager;

    void Awake()
    {
        scuffedDragon = FindAnyObjectByType<ScuffedDragon>();
        boxCollider = GetComponent<BoxCollider2D>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
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
            print("Spell casted!");
            var newSpell = Instantiate(spellPrefab, transform.position, Quaternion.identity);

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
}

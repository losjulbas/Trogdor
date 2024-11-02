using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Village : MonoBehaviour
{

    public float tickTime = 2f;
    float timer = 0f;
    public GameObject arrowPrefab;
    [SerializeField] float sightDistance;
    ScuffedDragon scuffedDragon;

    void Awake()
    {
        scuffedDragon = FindAnyObjectByType<ScuffedDragon>();
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
}

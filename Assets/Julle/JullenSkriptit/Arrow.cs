using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    //public Vector2 _direction;
    public float speed;
    ScuffedDragon scuffedDragon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scuffedDragon = FindAnyObjectByType<ScuffedDragon>();
        ShootArrow();
    }

    void Update()
    {
        
    }

    void ShootArrow()
    {
        //Vector2 velocity = _direction.normalized * speed;
        //rb.linearVelocity = velocity;  // Ensure proper velocity setup

        Vector3 pos = scuffedDragon.transform.position;

        //calculate direction to shoot the arrow
        var direction = (pos - transform.position).normalized * speed;
        //rb.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        rb.SetRotation(Quaternion.LookRotation(Vector3.forward, direction));
        rb.linearVelocity = direction;
        Destroy(gameObject, 10f);

    }


    private void OnTriggerEnter2D(Collider2D collision) {

        Destroy(gameObject);
    }
}

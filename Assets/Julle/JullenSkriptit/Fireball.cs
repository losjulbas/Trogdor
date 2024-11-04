using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rb;
    //public float speed;
    //private Vector2 direction;  // suunta johon dragon katsoo


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //public void Initialize(Vector2 shootDirection)
    //{
    //    //calculate direction to shoot the arrow
    //    Vector2 direction = shootDirection.normalized * speed;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    rb.SetRotation(angle);  // Rotate the fireball to match direction
    //    rb.linearVelocity = direction;
    //    Destroy(gameObject, 10f);
    //}


    private void OnTriggerEnter2D(Collider2D other)
    {
        var damgeable = other.GetComponent<IDamageable>();
        if (damgeable != null)
        {
            damgeable.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}

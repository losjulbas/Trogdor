using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    ScuffedDragon scuffedDragon;
    public float homingTimer;
    public float homingDuration = 1f;
    public float turningSpeed = 300;
    public float turningSpeedDecay = 20;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scuffedDragon = FindAnyObjectByType<ScuffedDragon>();
    }

    private void Start()
    {
        homingTimer = homingDuration;
        // Initial forward velocity
    }

    private void Update()
    {
        if (homingTimer > 0)
        {
            Vector3 pos = scuffedDragon.transform.position;

            //calculate direction to shoot the arrow
            var direction = (pos - transform.position).normalized * speed;


            var targetRot = Quaternion.LookRotation(Vector3.forward, direction);
            var rot = Quaternion.RotateTowards(transform.rotation, targetRot, turningSpeed * Time.deltaTime);
            
            turningSpeed = Mathf.Max(0, turningSpeed - turningSpeedDecay * Time.deltaTime); // Reduce turning aggresiviness
            rb.SetRotation(rot); // Spell rotates towards player
            rb.linearVelocity = direction;

            homingTimer -= Time.deltaTime; // Reduce homing effect
        }

        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
    }
}

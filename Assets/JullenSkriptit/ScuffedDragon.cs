using UnityEngine;

public class ScuffedDragon : MonoBehaviour
{

    [SerializeField] private int hitpoints = 2;

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

    void HandleDestruction()
    {
        Destroy(gameObject);
    }
}

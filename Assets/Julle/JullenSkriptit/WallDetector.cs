using UnityEngine;

public class WallDetector : MonoBehaviour
{
    public DragonMovement dragonMovement; // Reference to main movement script

    private void Awake()
    {
        dragonMovement = GetComponentInParent<DragonMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            dragonMovement.isCollidingWithWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            dragonMovement.isCollidingWithWall = false;
        }
    }
}

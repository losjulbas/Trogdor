using UnityEngine;
public enum PowerupType { None, Armor };

public class Powerup : MonoBehaviour
{
    public PowerupType whichType;

    void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameManager>().PowerupActivated(whichType);
        Destroy(gameObject);
    }
}

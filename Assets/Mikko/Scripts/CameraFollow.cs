using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public GameObject dragonToFollow;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dragonToFollow != null) { 
        transform.position = dragonToFollow.transform.position;
        }
    }
}

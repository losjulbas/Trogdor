using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public KeyCode leftTurn = KeyCode.LeftArrow;
    public KeyCode rightTurn = KeyCode.RightArrow;
    public KeyCode shooting = KeyCode.Space;

    public GameObject fireBall;


    public Transform[] bodyBones; 
    public Transform[] neckBones;
    public float neckSpeed;


    void Update() {
        MoveSnake();
        UpdateBones();
    }

    void MoveSnake() {
        Vector3 velocity = transform.up * speed;
        transform.position += velocity * Time.deltaTime;

        if (Input.GetKey(leftTurn)) {
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

            foreach (Transform t in bodyBones) {
                //t.Rotate(Vector3.up, neckSpeed * Time.deltaTime);
                Quaternion neckRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + Time.deltaTime * neckSpeed);
                t.rotation = neckRotation;
            }

        }
        if (Input.GetKey(rightTurn)) {
            transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
        }
    }


    void UpdateBones() {
        for (int i = 0; i < bodyBones.Length; i++) {
            
            //float angleOffset = i * 5f; 
            //Quaternion boneRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angleOffset);
            //bones[i].rotation = boneRotation;
        }

    }
}

using Unity.Mathematics;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float workingSpeed;
    public float rotateSpeed;
    public KeyCode leftTurn = KeyCode.LeftArrow;
    public KeyCode rightTurn = KeyCode.RightArrow;
    public KeyCode accelerate = KeyCode.UpArrow;
    float accelerationTime=2f;

    
    public AnimationCurve smoothTurningCurve;
    public AnimationCurve smoothStoppingCurve;
    float timeTurningLeft;
    float timeTurningRight;
    public float timeAccelerating;
    


    public Transform[] bodyBones; 
    public Transform[] neckBones;
    public float neckSpeed;


    void Awake(){
        workingSpeed = speed;
    }

    void Update() {
        MoveSnake();
        UpdateBones();

        if (Input.GetKey(accelerate)){
            timeAccelerating += Time.deltaTime/accelerationTime;
            float curveAccelerate = smoothTurningCurve.Evaluate(timeAccelerating)*(maxSpeed-workingSpeed);
            workingSpeed+= curveAccelerate;
        } 
        
        
        else if (workingSpeed>speed){
            workingSpeed -= Time.deltaTime;
            //float curveAccelerate = smoothTurningCurve.Evaluate(timeAccelerating)*(maxSpeed-workingSpeed);
            //workingSpeed -= timeAccelerating;
        } else {
            timeAccelerating = 0;
        }
    }

    

    void MoveSnake() {
        Vector3 velocity = transform.up * workingSpeed;
        transform.position += velocity * Time.deltaTime;

        if (Input.GetKey(leftTurn)) {
            timeTurningLeft += Time.deltaTime;
            float curveRotateLeft = smoothTurningCurve.Evaluate(timeTurningLeft)*rotateSpeed;
            transform.Rotate(Vector3.forward, curveRotateLeft * Time.deltaTime);
            
            TurnNeckLeft();

        } else if (timeTurningLeft>0){
            timeTurningLeft -= Time.deltaTime*2f; //maaginen luku puolittaa liikkumisesta palautumisajan
            float curveRotateLeft = smoothStoppingCurve.Evaluate(timeTurningLeft)*rotateSpeed;
            transform.Rotate(Vector3.forward, curveRotateLeft * Time.deltaTime);
        } else{
            timeTurningLeft=0;
        }



        if (Input.GetKey(rightTurn)) {
            timeTurningRight += Time.deltaTime;
            float curveRotateRight = smoothTurningCurve.Evaluate(timeTurningRight)*rotateSpeed;
            transform.Rotate(Vector3.forward, -curveRotateRight * Time.deltaTime);

            TurnNeckRight();
        } else if (timeTurningRight>0){
            timeTurningRight-= Time.deltaTime*2;
            float curveRotateRight = smoothStoppingCurve.Evaluate(timeTurningRight)*rotateSpeed;
            transform.Rotate(Vector3.forward, -curveRotateRight * Time.deltaTime);
        } else {
            timeTurningRight=0;
        }
    }

    void TurnNeckLeft(){
            
            
        for (int i = 0; i < neckBones.Length; i++)
        {
            
            neckBones[i].localRotation *= Quaternion.Euler(0, 0, neckSpeed * Time.deltaTime);
            /*    
            // Calculate an incremental rotation to the left
            Quaternion targetRotation = Quaternion.Euler(0, 0, neckBones[i].eulerAngles.z + neckSpeed * Time.deltaTime*(i+1));

            // Smoothly rotate the bone towards the target rotation
            neckBones[i].rotation = Quaternion.Lerp(neckBones[i].rotation, targetRotation, Time.deltaTime * neckSpeed*(i+1));
            */
        }
            
            
            
            // vanha
            /*for (int i =0; i<neckBones.Length; i++){
                
                Quaternion neckRotation = Quaternion.Euler(0,0,neckBones[i].eulerAngles.z);
                neckRotation.z += Time.deltaTime*neckSpeed;
                neckBones[i].rotation = neckRotation;

            }*/
            

    }

    void TurnNeckRight(){
        //TODO
    }


    void UpdateBones() {
        for (int i = 0; i < bodyBones.Length; i++) {
            
            //float angleOffset = i * 5f; 
            //Quaternion boneRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angleOffset);
            //bones[i].rotation = boneRotation;


                        /* //keskeneräinen luiden pyöritys
            foreach (Transform t in bodyBones) {
                //t.Rotate(Vector3.up, neckSpeed * Time.deltaTime);
                Quaternion neckRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + Time.deltaTime * neckSpeed);
                t.rotation = neckRotation;
            }*/
        }

    }
}

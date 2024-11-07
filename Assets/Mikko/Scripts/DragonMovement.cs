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
    public float maxAccelerationStamina; // sekuntteina
    public float workingAccelerationStamina = 0;
    bool hasAccelerationStamina = true;
    public bool accelerationBlock = false;


    public Transform[] tailBones; 
    public Transform[] neckBones;
    public float neckSpeed;
    public float neckMaxRotation;
    float neckRotation = 0;
    bool neckTurningLeft = false;
    bool neckTurningRight = false;

    public float tailSpeed;
    public float tailMaxRotation;
    float tailRotation = 0;

    public float tailWaveSpeed = 1;
    public float tailSegmentDifference = 1f;
    public float tailSegmentReduction = 0.5f;

    public bool isCollidingWithWall = false; //Jullen muutos mapin reunoja varten


    void Awake(){
        workingSpeed = speed;
    }

    void Update() {
        Moving();
        UpdateBones();
        Turning();

        if (Input.GetKey(accelerate) && workingAccelerationStamina < maxAccelerationStamina && !accelerationBlock) {
            workingAccelerationStamina += Time.deltaTime;
            hasAccelerationStamina = true;
        }else if (workingAccelerationStamina>0) {
            hasAccelerationStamina = false;
            accelerationBlock = true;
            workingAccelerationStamina -= Time.deltaTime;
        }
        else {
            workingAccelerationStamina = 0;
            accelerationBlock = false;
            hasAccelerationStamina = true;
        }




        if (Input.GetKey(accelerate) && hasAccelerationStamina){
            timeAccelerating += Time.deltaTime/accelerationTime;
            timeAccelerating = Mathf.Clamp(timeAccelerating, 0, 1);
            float curveAccelerate = smoothTurningCurve.Evaluate(timeAccelerating)*(maxSpeed-workingSpeed);
            workingSpeed+= curveAccelerate;

        } 
        else if (workingSpeed>speed){
            workingSpeed -= Time.deltaTime;
        } else {
            timeAccelerating = 0;
        }

    }

    

    void Moving() {

        if (isCollidingWithWall == false) //jullen lisäys
        {
            Vector3 velocity = transform.up * workingSpeed;
            transform.position += velocity * Time.deltaTime;
        }

        HandleRotation(); //jullen funktio, siirretty kaikki extra mikä täällä oli uuden funktion alle, jotta seinät toimii

    }
    void HandleRotation()
    {
        if (Input.GetKey(leftTurn))
        {
            timeTurningLeft += Time.deltaTime;
            timeTurningLeft = Mathf.Clamp(timeTurningLeft, 0, 1);
            float curveRotateLeft = smoothTurningCurve.Evaluate(timeTurningLeft) * rotateSpeed;
            transform.Rotate(Vector3.forward, curveRotateLeft * Time.deltaTime);

            neckTurningLeft = true;

        }
        else if (timeTurningLeft > 0)
        {
            timeTurningLeft -= Time.deltaTime * 2f; //maaginen luku puolittaa liikkumisesta palautumisajan
            float curveRotateLeft = smoothStoppingCurve.Evaluate(timeTurningLeft) * rotateSpeed;
            transform.Rotate(Vector3.forward, curveRotateLeft * Time.deltaTime);
            neckTurningLeft = false;

        }
        else
        {
            timeTurningLeft = 0;
            neckTurningLeft = false;
        }



        if (Input.GetKey(rightTurn))
        {
            timeTurningRight += Time.deltaTime;
            timeTurningRight = Mathf.Clamp(timeTurningRight, 0, 1);
            float curveRotateRight = smoothTurningCurve.Evaluate(timeTurningRight) * rotateSpeed;
            transform.Rotate(Vector3.forward, -curveRotateRight * Time.deltaTime);

            neckTurningRight = true;

        }
        else if (timeTurningRight > 0)
        {
            timeTurningRight -= Time.deltaTime * 2f; // Return to neutral position
            float curveRotateRight = smoothStoppingCurve.Evaluate(timeTurningRight) * rotateSpeed;
            transform.Rotate(Vector3.forward, -curveRotateRight * Time.deltaTime);
            neckTurningRight = false;

        }
        else
        {
            timeTurningRight = 0;
            neckTurningRight = false;
        }
    }

    void Turning(){

        // neck rotation

        if (neckTurningLeft && neckRotation < neckMaxRotation) { 
            neckRotation += Time.deltaTime * neckSpeed;
        } else if (neckRotation > 0) {
            neckRotation -= Time.deltaTime * neckSpeed*3;
        }

        
        if (neckTurningRight && neckRotation > -neckMaxRotation) {
            neckRotation -= Time.deltaTime * neckSpeed;
        }
        else if (neckRotation < 0) {
            neckRotation += Time.deltaTime * neckSpeed * 3;
        }
        
        neckBones[0].localRotation = Quaternion.AngleAxis(neckRotation, Vector3.forward);
        neckBones[1].localRotation = Quaternion.AngleAxis(neckRotation * 2, Vector3.forward);
        neckBones[2].localRotation = Quaternion.AngleAxis(neckRotation * 3, Vector3.forward);


        // tail rotation

        if (neckTurningLeft && tailRotation > -tailMaxRotation) {
            tailRotation -= Time.deltaTime * tailSpeed;

        }
        else if (tailRotation < 0) {
            tailRotation += Time.deltaTime * tailSpeed * 3;
        }

        if (neckTurningRight && tailRotation < tailMaxRotation) {
            tailRotation += Time.deltaTime * tailSpeed;
        }
        else if (tailRotation > 0) {
            tailRotation -= Time.deltaTime * tailSpeed * 3;
        }

        var rot1 = Mathf.Sin((Time.time- tailSegmentDifference) * tailWaveSpeed) * (tailSegmentReduction * 1);
        var rot2 = Mathf.Sin((Time.time - tailSegmentDifference*2) * tailWaveSpeed) * (tailSegmentReduction * 2);
        var rot3 = Mathf.Sin((Time.time - tailSegmentDifference*3) * tailWaveSpeed) * (tailSegmentReduction * 3);
        var rot4 = Mathf.Sin((Time.time - tailSegmentDifference*4) * tailWaveSpeed) * (tailSegmentReduction * 4);
        

        tailBones[0].localRotation = Quaternion.AngleAxis(tailRotation * 1 + 180+rot1, Vector3.forward);
        tailBones[1].localRotation = Quaternion.AngleAxis(tailRotation * 2 + rot2, Vector3.forward);
        tailBones[2].localRotation = Quaternion.AngleAxis(tailRotation * 3 + rot3, Vector3.forward);
        tailBones[3].localRotation = Quaternion.AngleAxis(tailRotation * 4 + rot4, Vector3.forward);







    }

    void TurnNeckRight(){
        //TODO
    }


    void UpdateBones() {
        for (int i = 0; i < tailBones.Length; i++) {
            
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

    public void SetCollisionState(bool state) //jullen muutos
    {
        isCollidingWithWall = state;
    }
}

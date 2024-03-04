using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];
    public float motorTorque = 50;
    public float steeringMax = 1000;

    // Start is called before the first frame update
    void Start()
    {
        FixedUpdate();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (Input.GetKey(KeyCode.W)){
            for (int i = 0;i < 4; i++){
                wheels[i].motorTorque = motorTorque;
            }
        }
        else{
            for (int i = 0;i < 4; i++){
                wheels[i].motorTorque = 0;
            }
        }

        if (Input.GetKey(KeyCode.S)){
            for(int i = 0; i < 4;i++){
                wheels[i].motorTorque = -motorTorque/2;
            }
        }

        if(Input.GetAxis("Horizontal") != 0){
            for(int i = 0; i < 2; i++){
                wheels[i].steerAngle = Input.GetAxis("Horizontal") * steeringMax;
            }
        }
        /*else{
            for(int i = 0; i < 2;i++){
                wheels[i].steerAngle = 0;
            }
        }*/
    }

    void animateWheels(){
        /*Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for(int i = 0; i < 4, i++){
            wheelColliders.GetWorldPose (out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        } */
    }
}

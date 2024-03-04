using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class IAController : MonoBehaviour
{
    internal enum driver{
        AI,
        keyboard,
        mobile

    }

    [SerializeField] driver driveController;

    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal; 
    [HideInInspector] public bool handbrake;
    [HideInInspector] public bool boosting; 

    public WheelCollider[] wheels = new WheelCollider[4];

    public WayPointsController waypoints;
    public List<Transform> nodes = new List<Transform>();
    public Transform currentWaypoint;
    [Range(0, 10)] public int distanceOffset;
    [Range(0, 10)] public float steerForce;


    private void Awake(){
        waypoints = GameObject.FindGameObjectWithTag("Path").GetComponent<WayPointsController>();
        nodes = waypoints.nodes;
        FixedUpdate();
    }

    private void FixedUpdate(){

        switch (driveController){
            case driver.AI:
                AIDrive();
                break;
            case driver.keyboard:
                break;
            case driver.mobile:
                break;
        }
        CalculateDistanceOfWaypoint();
    }

private void AIDrive(){
    vertical = .3f;
    for (int i = 0;i < 4; i++){
        wheels[i].motorTorque = 200;
    }
    AISteer();
}

private void CalculateDistanceOfWaypoint(){
    UnityEngine.Vector3 position = gameObject.transform.position;
    float distance = Mathf.Infinity;

    for (int i = 0;i < nodes.Count; i++){
        UnityEngine.Vector3 difference = nodes[i].transform.position - position;
        float currentDistance = difference.magnitude;
        if (currentDistance < distance){
            currentWaypoint = nodes[i + distanceOffset];
            distance = currentDistance;
        }   
    }
}

private void AISteer(){
    UnityEngine.Vector3 relative = transform.InverseTransformPoint(currentWaypoint.transform.position);
    relative /= relative.magnitude;

    horizontal = (relative.x / relative.magnitude) * steerForce;

    for(int i = 0; i < 2; i++){
        wheels[i].steerAngle =  horizontal * 100;
    }
}

private void OnDrawGizmos(){
    Gizmos.DrawWireSphere(currentWaypoint.position, 3);
}

}

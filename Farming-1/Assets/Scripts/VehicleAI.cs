using UnityEngine;

public class VehicleAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public float turnSpeed = 5f;
    private int currentWaypointIndex = 0;
    private bool isStopped = false, isDontMove;

    // Wheel Transforms (Assign in Inspector)
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform[] allWheels;
    public float detectionRange = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (isStopped)
        {
            Debug.Log("stop");

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            return;
        }


        DetectObstacles();
        MoveToNextWaypoint();
        RotateFrontWheels();
    }

    void MoveToNextWaypoint()
    {

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);


        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, targetWaypoint.position) < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }


    }

    void DetectObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, detectionRange))
        {
           
            if (hit.collider.CompareTag("Vehicle"))
            {
                Debug.Log("Vehicle detected: " + hit.collider.gameObject.name);
                isStopped = true;
               
            }

            StartCoroutine(ResumeAfterDelay(2f));
        }
    }

    System.Collections.IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isStopped = false;

        Debug.Log("resume");
    }

    void RotateFrontWheels()
    {
        if (waypoints.Length == 0) return;

        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        float steerAngle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);

        frontLeftWheel.localRotation = Quaternion.Euler(0, steerAngle, 0);
        frontRightWheel.localRotation = Quaternion.Euler(0, steerAngle, 0);

        foreach (Transform wheel in allWheels)
        {
            wheel.Rotate(Vector3.right * speed * Time.deltaTime * 20f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.5f, transform.forward * detectionRange);
    }
}

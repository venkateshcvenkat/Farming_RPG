using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider FL_Wheel, FR_Wheel; // Front Left and Right Wheels
    public WheelCollider RL_Wheel, RR_Wheel; // Rear Left and Right Wheels

    public Transform FL_Transform, FR_Transform, RL_Transform, RR_Transform; // Wheel Meshes for visuals

    public float maxTorque = 1000f; // Acceleration Force
    public float maxSteerAngle = 25f; // Steering Angle
    public float brakeForce = 3000f; // Brake Strength
    public float antiRollForce = 5000f; // Anti-Roll Bar Strength

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 1500f; // Increase car weight for stability
        rb.centerOfMass = new Vector3(0, -0.8f, 0); // Lower center of mass
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical"); // Forward/Backward Input
        float steer = Input.GetAxis("Horizontal"); // Left/Right Input
        bool isBraking = Input.GetKey(KeyCode.Space);

        ApplyMotorTorque(move);
        ApplySteering(steer);
        ApplyBrakes(isBraking);
        ApplyAntiRoll();
        UpdateWheelVisuals();
    }

    void ApplyMotorTorque(float move)
    {
        RL_Wheel.motorTorque = move * maxTorque;
        RR_Wheel.motorTorque = move * maxTorque;
    }

    void ApplySteering(float steer)
    {
        float steerAngle = steer * maxSteerAngle;
        FL_Wheel.steerAngle = steerAngle;
        FR_Wheel.steerAngle = steerAngle;
    }

    void ApplyBrakes(bool isBraking)
    {
        float brake = isBraking ? brakeForce : 0;
        FL_Wheel.brakeTorque = brake;
        FR_Wheel.brakeTorque = brake;
        RL_Wheel.brakeTorque = brake;
        RR_Wheel.brakeTorque = brake;
    }

    void ApplyAntiRoll()
    {
        ApplyAntiRollForce(FL_Wheel, FR_Wheel);
        ApplyAntiRollForce(RL_Wheel, RR_Wheel);
    }

    void ApplyAntiRollForce(WheelCollider leftWheel, WheelCollider rightWheel)
    {
        WheelHit leftHit, rightHit;
        bool groundedLeft = leftWheel.GetGroundHit(out leftHit);
        bool groundedRight = rightWheel.GetGroundHit(out rightHit);

        float travelLeft = groundedLeft ? (1.0f - (leftHit.force / leftWheel.suspensionSpring.spring)) : 1.0f;
        float travelRight = groundedRight ? (1.0f - (rightHit.force / rightWheel.suspensionSpring.spring)) : 1.0f;

        float force = (travelLeft - travelRight) * antiRollForce;

        if (groundedLeft)
            rb.AddForceAtPosition(leftWheel.transform.up * -force, leftWheel.transform.position);

        if (groundedRight)
            rb.AddForceAtPosition(rightWheel.transform.up * force, rightWheel.transform.position);
    }

    void UpdateWheelVisuals()
    {
        UpdateWheel(FL_Wheel, FL_Transform);
        UpdateWheel(FR_Wheel, FR_Transform);
        UpdateWheel(RL_Wheel, RL_Transform);
        UpdateWheel(RR_Wheel, RR_Transform);
    }

    void UpdateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}

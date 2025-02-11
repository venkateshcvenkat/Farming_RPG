/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float offsetZ = 5f;
    public float smoothing = 2f;

    Transform playerPos;
    void Start()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
    }


    void Update()
    {
        FollowPlayer();
    }
    void FollowPlayer()
    {
        Vector3 targetPos = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z - offsetZ);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
    }
}
*/

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player or object the camera should follow
    public float distance = 5.0f; // Distance from the target
    public float height = 3.0f; // Height above the target
    public float rotationSpeed = 5.0f; // Speed of camera rotation
    public float smoothSpeed = 0.125f; // Smooth movement speed

    private Vector3 offset;

    void Start()
    {
        // Calculate initial offset based on target position
        offset = new Vector3(0, height, -distance);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (!target) return;

        // Get mouse input for rotation
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed;

        // Rotate around the target horizontally
        offset = Quaternion.AngleAxis(horizontal, Vector3.up) * offset;

        // Clamp vertical rotation
        float desiredHeight = Mathf.Clamp(offset.y + vertical, 1.0f, height);
        offset = new Vector3(offset.x, desiredHeight, offset.z);

        // Calculate desired position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Look at the target
        transform.LookAt(target);
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private float moveSpeed = 4f;
    interactable interactableScript;

    [Header("Moement System")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        interactableScript = GetComponentInChildren<interactable>();
    }


    void Update()
    {
        Move();
        interact();

        //debug purposes only
        if (Input.GetKey(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }
    }
    public void interact()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            interactableScript.Interact();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            interactableScript.ItemInteract();
        }
    }
    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 velocity = dir * Time.deltaTime * moveSpeed;
        if (Input.GetButton("Sprint"))
        {
            moveSpeed = runSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            moveSpeed = walkSpeed;
            animator.SetBool("Running", false);
        }

        if (dir.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            controller.Move(velocity);

        }
        animator.SetFloat("Speed", dir.magnitude);

    }
}*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private float moveSpeed = 4f;
    private interactable interactableScript;

    [Header("Movement System")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public Transform cameraTransform; // Reference to the camera's transform
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        interactableScript = GetComponentInChildren<interactable>();
        
    }

    void Update()
    {
        Move();
        Interact();

        // Debug purposes only
        if (Input.GetKey(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }
    }

    public void Interact()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            interactableScript.Interact();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            interactableScript.ItemInteract();
        }
    }

    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calculate target rotation based on camera direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // Smooth rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Move in the direction of the camera
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir * moveSpeed * Time.deltaTime);
        }

        // Sprint logic
        if (Input.GetButton("Sprint"))
        {
            moveSpeed = runSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            moveSpeed = walkSpeed;
            animator.SetBool("Running", false);
        }

        animator.SetFloat("Speed", direction.magnitude);
    }

   
}

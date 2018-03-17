using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 3f; 
    public float gravity = 10f;
    public Vector3 targetVelocity;

    [Header("MouseControl")]
    public bool lookAtMouse = true;
    public float rotationSpeed = 10f;
    public LayerMask raycastLayer;

    RaycastHit mouseRaycastHit;
    Rigidbody rb;
    bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        PlayerMovement();
        MousePosition();
        CharacterRotation();
    }


    bool GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            return true;
        }
        else return false;
    }
    public Vector3 MousePosition()
    {
        // Get mouse position on Main Camera screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // If Raycast hit something, return hit position
        if (Physics.Raycast(ray, out mouseRaycastHit))
        {
            return mouseRaycastHit.point;
        }

        // If not calculate direction from Character position to mouse position on screen
        else
        {
            Vector3 mousePos = ray.origin + ray.direction * Vector3.Distance(ray.origin, transform.position);
            return mousePos;
        }
    }

    void CharacterRotation()
    {
        // Rotate to mouse position
        if (lookAtMouse)
        {
            // Calculate direction from character to mouse position
            Vector3 direction = (new Vector3(MousePosition().x, transform.position.y, MousePosition().z) - transform.position);

            // Direction to rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction, transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            // Spherical interpolation to desired rotation
            transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }

        //Rotate character to velocity vector
        else
        {
            // Set previous character rotation as new rotation
            Quaternion targetRotation = transform.rotation;
            // Calculate direction from character velocity vector
            Vector3 direction = new Vector3(targetVelocity.x, 0.0f, targetVelocity.z);

            // Direction to rotation
            if (direction != Vector3.zero)
                targetRotation = Quaternion.LookRotation(direction);

            // Spherical interpolation to desired rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }

    void PlayerMovement()
    {
        grounded = GroundCheck();

        if (grounded)
        {
            // Input
            targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Normalize vector, affection of momevemnt speed in seconds
            targetVelocity = targetVelocity.normalized * movementSpeed * Time.deltaTime;

            rb.MovePosition(transform.position + targetVelocity);
            rb.AddForce(new Vector3(0, gravity, 0));
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 3f, Color.blue);
    }

#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum surfaceType { Ground, Wood, Stone, Mud, Water, Mist, Null }

public class PlayerController : MonoBehaviour
{
    public static GameObject playerObject;

    [Header("Surface")]
    public surfaceType currentSurface;

    [Header("Animations")]
    public Animator animator;

    [Header("Movement")]
    public float movementSpeedBonus = 0f;
    public float movementSpeedBonusTimer = 0f;

    [Space]
    [HideInInspector]
    public float jumpTime = 1f;
   [HideInInspector]
    public float dashForce = 10;

    public float dashSpeed = 1f;
    public float dashDistance = 5f;
    public float dashRate = 2f;
    float nextDash;


    float elapsedTime = 0;


    [Space]
    public float movementSpeed = 3f; 
    public float gravity = 10f;
    public Vector3 targetVelocity;
    public LayerMask groundCheckLayer;
    RaycastHit groundHit;

    [Header("MouseControl")]
    public bool lookAtMouse = true;
    public float rotationSpeed = 10f;
    public LayerMask raycastLayer;

    RaycastHit mouseRaycastHit;
    Rigidbody rb;
    bool grounded;
    bool jumping;

    private void Awake()
    {
        PlayerController.playerObject = gameObject;

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        PlayerMovement();
        MousePosition();
        CharacterRotation();
        currentSurface = CheckSurfaceType();

        if (movementSpeedBonusTimer > 0) movementSpeedBonusTimer -= Time.deltaTime;

        else
        {
            movementSpeedBonusTimer = 0;
            movementSpeedBonus = 0;
        }

        //animator update
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("moving", true);
        }

        else animator.SetBool("moving", false);

        animator.SetBool("grounded", GroundCheck());


        //return to menu
        if (Input.GetKeyDown(KeyCode.Escape)) MenuFunctions.instance.LoadScene("scene_mainMenu");
    }

    public void AddBoostSpeed(float speedBonus, float time)
    {
        movementSpeedBonus += speedBonus;
        movementSpeedBonusTimer += time;
    }

    
    surfaceType CheckSurfaceType()
    {
        surfaceType mySurface = surfaceType.Null;

        if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.05f, groundCheckLayer))
        {
            string matName = groundHit.collider.material.name;

            switch (matName)
            {
                case "Ground (Instance)":
                    mySurface = surfaceType.Ground;
                    break;
                case "Wood (Instance)":
                    mySurface = surfaceType.Wood;
                    break;
                case "Stone (Instance)":
                    mySurface = surfaceType.Stone;
                    break;
                case "Mud (Instance)":
                    mySurface = surfaceType.Mud;
                    break;
                case "Water (Instance)":
                    mySurface = surfaceType.Water;
                    break;
                case "Mist (Instance)":
                    mySurface = surfaceType.Mist;
                    break;

                case "Default (Instance)":
                    mySurface = surfaceType.Ground;
                    break;
            }

        }

        else mySurface = surfaceType.Null;

        return mySurface;
    }
    
    bool GroundCheck()
    {
        /*
              if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
            {
                return true;
            }
            else return false;
        */
        Ray myRay = new Ray(transform.position, Vector3.down);

        if (Physics.SphereCast(myRay, 0.5f, 1.05f))
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
        if(!jumping)grounded = GroundCheck();

        if (grounded && elapsedTime == 0)
        {
            // Input
            targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Normalize vector, affection of momevemnt speed in seconds
          if(movementSpeedBonusTimer > 0)  targetVelocity = targetVelocity.normalized * (movementSpeed + movementSpeedBonus) * Time.deltaTime;
          else if(currentSurface == surfaceType.Mist) targetVelocity = targetVelocity.normalized * (movementSpeed * 0.5f) * Time.deltaTime;
            else targetVelocity = targetVelocity.normalized * movementSpeed * Time.deltaTime;

            

            if (Input.GetButtonDown("Jump") && currentSurface != surfaceType.Mist)
            {
                if (Time.time > nextDash)
                {
                    //Dash();
                    float dashDist = dashDistance;
                    RaycastHit hit;

                    if (Physics.Raycast(transform.position, targetVelocity.normalized, out hit, dashDistance))
                    {
                        dashDist = hit.distance;
                        Debug.DrawLine(transform.position, hit.point, Color.blue, 2f);
                    }

                    StartCoroutine("Dash", dashDist);

                    nextDash = Time.time + dashRate;
                }
            }
        }
        

        //targetVelocity.y -= gravity * Time.deltaTime;

        rb.MovePosition(transform.position + targetVelocity);
        rb.AddForce(Vector3.down * gravity, ForceMode.Force);

        //reset game when under map
        if (transform.position.y < -50f) MenuFunctions.instance.RestartCurrentScene();
    }

    IEnumerator Dash(float dashDist)
    {

        /*
        //aimator
        animator.SetBool("jump", true);

        StartCoroutine("SetJumpingFalse", jumpTime);

        jumping = true;
        grounded = false;

            Vector3 dashVector = targetVelocity.normalized * dashForce;
            dashVector.y = 0.5f * dashForce;

            rb.AddForce(dashVector * dashForce);

        nextDash = Time.time + dashRate;
        */
       

        while (elapsedTime < dashSpeed)
        {
            Debug.DrawLine(transform.position, targetVelocity.normalized * dashDist, Color.red);

            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, transform.position + targetVelocity.normalized * dashDist, Time.deltaTime * dashSpeed * 10);

            yield return new WaitForEndOfFrame();
        }

        elapsedTime = 0;
    }

    IEnumerator SetJumpingFalse(float time)
    {
        yield return new WaitForSeconds(time);
        jumping = false;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 3f, Color.blue);
    }

#endif
}

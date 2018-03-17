using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class AI_Firefly : MonoBehaviour
{
    public float movementForce = 700f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Vector3 direction = transform.position - other.transform.position;
            Dash(direction);
        }
    }

    void Dash(Vector3 dir)
    {

        dir.x -= Random.Range(0, 5f);
        dir.x += Random.Range(0, 5f);

        rb.AddForce(dir.normalized * Random.Range(0.5f * movementForce, 1.25f * movementForce), ForceMode.Impulse);
    }
}

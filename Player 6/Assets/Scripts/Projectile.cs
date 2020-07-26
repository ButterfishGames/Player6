using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxRange;
    public float moveSpeed;

    private float dist;
    private Vector3 moveDir;
    private Vector3 prevPos;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        moveDir = transform.forward;

        rb = GetComponent<Rigidbody>();
        rb.velocity = moveDir * moveSpeed;
        prevPos = transform.position;

        dist = 0;
    }

    private void LateUpdate()
    {
        dist += Vector3.Distance(prevPos, transform.position);
        prevPos = transform.position;
        if (dist > maxRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Hit(collision.collider.gameObject);
    }

    private void Hit(GameObject target)
    {
        // Implement code to damage players (and objects?)
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float sprintMult;
    public float mouseSensitivity;
    public float jumpForce;

    public Transform gunArm;

    public GameObject bullet;

    private float moveMod;

    private Vector2 moveVal;
    private Vector2 lookVal;

    private Rigidbody rb;

    // private Weapon[] weapons;
    private Transform bulletSpawn;

    private PlayerInput pIn;

    private bool canJump;

    public void OnMove(InputValue value)
    {
        moveVal = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookVal = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        Jump();
    }

    public void OnFire(InputValue value)
    {
        Shoot();
    }

    public void OnSprintStart(InputValue value)
    {
        moveMod = sprintMult;
    }

    public void OnSprintRelease(InputValue value)
    {
        moveMod = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveMod = 1.0f;
        rb = GetComponent<Rigidbody>();
        pIn = GetComponent<PlayerInput>();
        canJump = true;
        bulletSpawn = gunArm.Find("Gun").Find("BulletSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, lookVal.x * mouseSensitivity, 0);
        gunArm.Rotate(lookVal.y * mouseSensitivity * -1, 0, 0);
        if (gunArm.localRotation.eulerAngles.x > 70 && gunArm.localRotation.eulerAngles.x <= 180)
        {
            gunArm.localRotation = Quaternion.Euler(70, 0, 0);
        }
        else if (gunArm.localRotation.eulerAngles.x < 290 && gunArm.localRotation.eulerAngles.x >= 180)
        {
            gunArm.localRotation = Quaternion.Euler(290, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        Vector3 move = (transform.forward * moveVal.y + transform.right * moveVal.x) * moveSpeed * moveMod;
        move.y = rb.velocity.y;
        rb.velocity = move;
    }

    private void Jump()
    {
        if (canJump)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            canJump = false;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}

public struct Weapon
{
    bool isGun;
    GameObject prefab;
    GameObject projectile;
}

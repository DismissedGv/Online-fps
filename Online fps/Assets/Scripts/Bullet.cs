using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float shootForce;
    private Rigidbody rb;

    void Start()
    {
        //Reference
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Move the fireball forward based on the player facing direction
        rb.velocity = rb.transform.forward * shootForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}

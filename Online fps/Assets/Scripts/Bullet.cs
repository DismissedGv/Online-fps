using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public Shoot parent;
    [SerializeField] private float shootForce;
    private Rigidbody rb;

    void Start()
    {
        //Reference
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3.5f);
    }

    void Update()
    {
        //Move the fireball forward based on the player facing direction
        rb.velocity = rb.transform.forward * shootForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;
        parent.DestroyServerRpc();
    }
}

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
        if (other.CompareTag("Player"))
        { other.GetComponent<PlayerMovement2>().ResetPlayerPositionServerRpc(); }
        if (!IsOwner) return;
        parent.DestroyServerRpc();
    }
}

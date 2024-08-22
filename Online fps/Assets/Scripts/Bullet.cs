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
        if (IsServer && IsSpawned)
        {
            Invoke(nameof(DestroyServerRpc), 3.5f);
        }
    }

    void Update()
    {
        rb.velocity = rb.transform.forward * shootForce;
    }

    [ServerRpc]
    public void DestroyServerRpc()
    {
        NetworkObject.Despawn(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!IsServer || !IsSpawned)
        {
            return;
        }
        if (other.gameObject.CompareTag("Player") && parent.gameObject != other.gameObject)
        {
            other.gameObject.GetComponent<PlayerMovement2>().ResetPlayerPosition();
        }
        DestroyServerRpc();
    }
}
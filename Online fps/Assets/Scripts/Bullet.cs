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
        if(IsServer && IsSpawned)
        { Invoke(nameof(DestroyServerRpc), 3.5f); }
    }

    void Update()
    {
        //Move the fireball forward based on the player facing direction
        rb.velocity = rb.transform.forward * shootForce;
    }

    [ServerRpc]
    public void DestroyServerRpc()
    {
        NetworkObject.Despawn(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!IsServer) { return;}
        if (parent.gameObject == other.gameObject) return;
        if (other.CompareTag("Player"))
        { other.GetComponent<PlayerMovement2>().ResetPlayerPosition(); }
        if(IsSpawned) DestroyServerRpc();
    }
}

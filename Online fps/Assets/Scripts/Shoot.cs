using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Shoot : NetworkBehaviour
{
    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.Mouse0;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootTransform;

    [SerializeField] private List<GameObject> spawnedBullets = new List<GameObject>();

    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(shootKey))
        {
            ShootServerRpc();
        }
    }

    [ServerRpc]
    private void ShootServerRpc()
    {
        GameObject go = Instantiate(bullet, shootTransform.position, shootTransform.rotation);
        spawnedBullets.Add(go);
        go.GetComponent<Bullet>().parent = this;
        go.GetComponent<NetworkObject>().Spawn(); //syncing gameobject to both host and clients
    }

    [ServerRpc]
    public void DestroyServerRpc()
    {
        GameObject toDestroy = spawnedBullets[0];
        toDestroy.GetComponent<NetworkObject>().Despawn();
        spawnedBullets.Remove(toDestroy);
        Destroy(toDestroy);
    }
}

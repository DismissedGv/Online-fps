using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Shoot : NetworkBehaviour
{
    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.Mouse0;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootTransform;

    void Update()
    {
        if (IsLocalPlayer)
        {
            if (Input.GetKeyDown(shootKey))
            {
                ShootServerRpc();
            }
        }
    }

    [ServerRpc]
    private void ShootServerRpc()
    {
        GameObject go = Instantiate(bullet, shootTransform.position, shootTransform.rotation);
        go.GetComponent<Bullet>().parent = this;
        go.GetComponent<NetworkObject>().Spawn();
    }
}
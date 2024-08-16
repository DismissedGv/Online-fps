using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSettings : NetworkBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    public List<Color> colors = new List<Color>();
    void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

   public override void OnNetworkSpawn()
   {
        meshRenderer.material.color = colors[(int)OwnerClientId];
   }
}

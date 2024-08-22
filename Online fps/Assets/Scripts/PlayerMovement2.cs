using Unity.Netcode;
using UnityEngine;

public class PlayerMovement2 : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float positionRange = 5f;

    public override void OnNetworkSpawn()
    {
        UpdatePositionServerRpc();
    }

    void Update()
    {
        if (!IsOwner) return;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdatePositionServerRpc()
    {
        transform.position = new Vector3(Random.Range(positionRange, -positionRange), 0, Random.Range(positionRange, -positionRange));
        transform.rotation = new Quaternion(0, Random.Range(0, 180), 0, 0);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ResetPlayerPositionServerRpc()
    {
        transform.position = new Vector3(0, 0, 0);
    }
}

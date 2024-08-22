using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement2 : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float positionRange = 5f;

    private Vector3 move2;
    private Rigidbody rb;
    private float maxSpeed = 50f;

    private void Start()
    {
        if(!IsServer) return;
        rb = GetComponent<Rigidbody>();
        Vector2 startPos = Random.insideUnitCircle * positionRange;
        transform.position = startPos;

        float startRot = Random.Range(0, 180);
        transform.rotation = Quaternion.Euler(0, startRot, 0);
    }

    void FixedUpdate()
    {
        if(!IsServer) return;
        rb.AddForce(move2 * movementSpeed);
    }

    void Update()
    {
        if (!IsLocalPlayer) return;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        //transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);

        MovePlayerServerRpc(movementDirection);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    [ServerRpc]
    private void MovePlayerServerRpc(Vector3 move)
    {
        move2 = move;
    }

    public void ResetPlayerPosition()
    {
        transform.position = new Vector3(0, 0, 0);
    }
}

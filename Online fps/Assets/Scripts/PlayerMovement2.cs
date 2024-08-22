using Unity.Netcode;
using UnityEngine;

public class PlayerMovement2 : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float positionRange = 5f;

    private Vector3 move;
    private Rigidbody rb;
    private float maxSpeed = 50f;

    private void Start()
    {
        if (IsServer)
        {
            rb = GetComponent<Rigidbody>();
            Vector2 startPos = Random.insideUnitCircle * positionRange;
            transform.position = startPos;

            float startRot = Random.Range(0, 180);
            transform.rotation = Quaternion.Euler(0, startRot, 0);
        }
    }

    void FixedUpdate()
    {
        if (IsServer)
        {
            rb.AddForce(move * movementSpeed);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    void Update()
    {
        if (IsLocalPlayer)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new(horizontalInput, 0, verticalInput);
            MovePlayerServerRpc(movementDirection);
        }

        if (IsServer)
        {
            if (move != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    [ServerRpc]
    private void MovePlayerServerRpc(Vector3 input)
    {
        move = input;
    }

    public void ResetPlayerPosition()
    {
        transform.position = new Vector3(0, 0, 0);
    }
}
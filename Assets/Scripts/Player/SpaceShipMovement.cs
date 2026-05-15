using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShipMovement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float moveSpeed = 30f;

    private Rigidbody rb;
    private float initialY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        // Guardamos la altura inicial para bloquearla y evitar que la nave suba o baje
        initialY = transform.position.y;
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        // Controles de dirección (WASD)
        if (Input.GetKey(KeyCode.A)) movement -= transform.right;
        if (Input.GetKey(KeyCode.D)) movement += transform.right;
        if (Input.GetKey(KeyCode.W)) movement += transform.forward;
        if (Input.GetKey(KeyCode.S)) movement -= transform.forward;

        // Normalizamos el vector para que no corra más al ir en diagonal
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        transform.position += movement * moveSpeed * Time.deltaTime;

        // Forzamos que la altura Y sea siempre la inicial
        Vector3 pos = transform.position;
        pos.y = initialY;
        transform.position = pos;
    }
}
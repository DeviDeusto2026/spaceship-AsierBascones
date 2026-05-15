using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Header("Configuración de la Bala")]
    public float speed = 50f;
    public float lifeTime = 3f;

    [Header("Efectos Visuales")]
    public GameObject bulletHitEffect;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.linearVelocity = transform.forward * speed;

        // Autodestrucción para optimizar memoria
        Destroy(gameObject, lifeTime);
    }

    // Lógica de impacto
    void OnCollisionEnter(Collision collision)
    {
        // Ignoramos el choque si la bala toca nuestra propia nave
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // Si hay un efecto visual asignado, lo creamos en el punto de impacto
        if (bulletHitEffect != null)
        {
            Instantiate(bulletHitEffect, transform.position, transform.rotation);
        }

        // La bala se destruye al tocar cualquier otro objeto
        Destroy(gameObject);
    }
}
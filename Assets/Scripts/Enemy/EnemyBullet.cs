using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Configuración de Bala")]
    public float speed = 30f;
    public float lifeTime = 4f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            // La bala sale disparada hacia el frente local del objeto
            rb.linearVelocity = transform.forward * speed;
        }

        // Se destruye automáticamente tras unos segundos para no llenar la memoria
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // La bala enemiga ignora a otros enemigos para no matarse entre ellos
        if (collision.gameObject.CompareTag("Enemy")) return;

        // Si toca cualquier otra cosa, desaparece
        Destroy(gameObject);
    }
}
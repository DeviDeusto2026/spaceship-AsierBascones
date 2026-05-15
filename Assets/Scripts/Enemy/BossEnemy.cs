using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    [Header("Estadísticas")]
    public int health = 20;
    private int maxHealth;
    public float speed = 5f;
    public Image healthBarFill;

    [Header("Movimiento Sinusoidal")]
    public float frequency = 2f;
    public float magnitude = 5f;
    private Vector3 pos;

    [Header("Ajustes de Ataque")]
    public GameObject bossBulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float nextFireTime;

    [Header("Minions")]
    public GameObject minionPrefab;

    private Transform player;

    void Start()
    {
        maxHealth = health;
        pos = transform.position;

        // Inicializamos el tiempo de disparo para que no dispare al segundo 0.1
        nextFireTime = Time.time + fireRate;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        EnemySpawner spawner = Object.FindFirstObjectByType<EnemySpawner>();
        if (spawner != null) spawner.StopSpawning();
    }

    void Update()
    {
        if (player != null)
        {
            // 1. Orientación constante al jugador
            transform.LookAt(player);

            // 2. Movimiento hacia el jugador con zigzag
            Vector3 zigzag = transform.right * Mathf.Sin(Time.time * frequency) * magnitude;
            transform.position += (transform.forward * speed + zigzag) * Time.deltaTime;

            // 3. Lógica de Disparo (CORREGIDA)
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        if (bossBulletPrefab != null && firePoint != null)
        {
            // Creamos la bala
            Instantiate(bossBulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            // Si falta algo, este mensaje te lo dirá en la consola de Unity
            Debug.LogWarning("¡Atención! Falta asignar el Prefab de la bala o el FirePoint en el Jefe.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (healthBarFill != null) healthBarFill.fillAmount = (float)health / maxHealth;

            if (minionPrefab != null) Instantiate(minionPrefab, transform.position, Quaternion.identity);

            if (health <= 0) Win();
        }
    }

    void Win()
    {
        SceneManager.LoadScene("Win");
        Destroy(gameObject);
    }
}
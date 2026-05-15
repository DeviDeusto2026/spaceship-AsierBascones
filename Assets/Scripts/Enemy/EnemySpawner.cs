using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración Moscas")]
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    public float spawnRangeX = 20f;

    [Header("Configuración Jefe")]
    public GameObject bossPrefab;
    public float timeToSpawnBoss = 20f;
    private bool bossSpawned = false;

    [Header("Disparo de las Moscas (Configuración Global)")]
    public GameObject enemyBulletPrefab;
    public float fireRate = 2f;
    private float nextFireTime;

    void Start()
    {
        // Empezamos a soltar moscas normales repetidamente
        InvokeRepeating("Spawn", spawnRate, spawnRate);
    }

    void Update()
    {
        // Lógica de aparición del Jefe tras el tiempo establecido
        if (!bossSpawned && Time.timeSinceLevelLoad >= timeToSpawnBoss)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        bossSpawned = true;
        // Instanciamos al Boss en la posición actual del Spawner
        Instantiate(bossPrefab, transform.position, Quaternion.identity);

        // El Boss llamará a StopSpawning() desde su propio script
    }

    void Spawn()
    {
        // Generamos una posición aleatoria en el eje X para las moscas
        Vector3 spawnPos = new Vector3(
            Random.Range(-spawnRangeX, spawnRangeX),
            transform.position.y,
            transform.position.z
        );

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    // Función que detiene por completo la creación de enemigos
    public void StopSpawning()
    {
        Debug.Log("¡Jefe detectado! Deteniendo spawner de moscas.");
        CancelInvoke("Spawn");
        this.enabled = false; // Desactivamos el script para ahorrar recursos
    }
}
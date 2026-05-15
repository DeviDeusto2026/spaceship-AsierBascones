using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Estadísticas de Vida")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Interfaz de Usuario")]
    public Image healthBarFill;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Actualizamos el relleno de la barra
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }

        // Comprobamos si el jugador ha muerto
        if (currentHealth <= 0)
        {
            Debug.Log("Jugador derrotado");
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}
using UnityEngine;

public class SpaceShipRotation : MonoBehaviour
{
    [Header("Configuración de Control")]
    public float sensitivity = 200f;
    private float yRotation = 0f;

    void Update()
    {
        // Gestión del cursor: Solo se oculta y bloquea mientras se mantiene el clic derecho
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // Aplicamos la rotación horizontal
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

            // Acumulamos la rotación en el eje Y (giro lateral)
            yRotation += mouseX;

            // Aplicamos la rotación a la nave
            transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}
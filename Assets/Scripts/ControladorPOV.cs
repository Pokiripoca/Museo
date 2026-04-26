using UnityEngine;

public class ControladorPOV : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float sensibilidadMouse = 2f;

    private float rotacionX = 0f;
    public Transform cuerpoJugador; // Arrastra aquí al padre (Jugador)

    void Start()
    {
        // Bloqueamos el mouse para que no se salga de la ventana
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
 
        float mouseX = Input.GetAxisRaw("Mouse X") * sensibilidadMouse;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensibilidadMouse;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f); 
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        if (cuerpoJugador != null)
        {
            cuerpoJugador.Rotate(Vector3.up * mouseX);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movimiento = cuerpoJugador.right * x + cuerpoJugador.forward * z;
        cuerpoJugador.position += movimiento * velocidadMovimiento * Time.deltaTime;
    }
}
using UnityEngine;

public class Camrafija : MonoBehaviour
{
    [Header("Configuración de Cámaras")]
    public GameObject camaraJugador;   
    public GameObject camaraFijaSalon;

    [Header("Interfaz de Ayuda")]
    public GameObject canvasTextoAyuda; 

    [Header("Control del Jugador")]
    public MonoBehaviour scriptMovimiento;

    private bool enModoCamaraFija = false;
    private bool yaSeUso = false;

    private void Start()
    {

        if (canvasTextoAyuda != null) canvasTextoAyuda.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaSeUso)
        {
            yaSeUso = true; 
            ActivarCamaraFija();
        }
    }

    private void Update()
    {
        if (enModoCamaraFija)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("SALIENDO DE CÁMARA: Espacio presionado.");
                RegresarAlJugador();
            }
        }
    }

    void ActivarCamaraFija()
    {
        enModoCamaraFija = true;

        if (scriptMovimiento != null) scriptMovimiento.enabled = false;
        if (camaraJugador != null) camaraJugador.SetActive(false);
        if (camaraFijaSalon != null) camaraFijaSalon.SetActive(true);

        if (canvasTextoAyuda != null) canvasTextoAyuda.SetActive(true);
    }

    void RegresarAlJugador()
    {
        enModoCamaraFija = false;

        if (camaraFijaSalon != null) camaraFijaSalon.SetActive(false);
        if (camaraJugador != null) camaraJugador.SetActive(true);
        if (scriptMovimiento != null) scriptMovimiento.enabled = true;

        if (canvasTextoAyuda != null) canvasTextoAyuda.SetActive(false);

        Destroy(gameObject);
    }
}
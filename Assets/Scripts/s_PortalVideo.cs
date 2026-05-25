using UnityEngine;
using UnityEngine.Video;
using TMPro;
using System.Collections;

public class s_PortalVideo : MonoBehaviour
{
    [Header("UI y Video")]
    public GameObject canvasVideosObject;
    public VideoPlayer videoPlayer;
    public VideoClip videoEsteSalon;
    public TextMeshProUGUI textoIdentificadorCamara;
    public string nombreDeEstaCamara = "CAM_01: INTRODUCCIÓN";

    [Header("Control del Jugador")]
    public MonoBehaviour scriptMovimiento;

    private bool yaSeUso = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaSeUso)
        {
            yaSeUso = true;

            Collider miCollider = GetComponent<Collider>();
            if (miCollider != null)
            {
                miCollider.enabled = false;
            }

            StartCoroutine(ReproducirCinematica());
        }
    }

    IEnumerator ReproducirCinematica()
    {
        if (scriptMovimiento != null) scriptMovimiento.enabled = false;

        if (videoPlayer != null && videoEsteSalon != null)
        {
            videoPlayer.clip = videoEsteSalon;
            if (textoIdentificadorCamara != null) textoIdentificadorCamara.text = nombreDeEstaCamara;

            canvasVideosObject.SetActive(true);
            yield return null; 

            videoPlayer.Play();

            float tiempoEspera = (float)videoEsteSalon.length;
            yield return new WaitForSeconds(tiempoEspera);

            videoPlayer.Stop();
        }

        if (canvasVideosObject != null) canvasVideosObject.SetActive(false);
        if (scriptMovimiento != null) scriptMovimiento.enabled = true;

        Debug.Log("Trigger de video completado con éxito. Destruyendo objeto: " + gameObject.name);
        Destroy(gameObject);
    }
}
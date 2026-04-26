using System.Collections;
using UnityEngine;

public class PelotaControl : MonoBehaviour
{
    private Rigidbody rb;
    public float pushForce = 10f;
    public float torqueForce = 10f;
    public AudioSource audioSource;
    private bool moving = false;
    private bool isPaused = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            moving = true;
            rb.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (isPaused == false)
            {
                Time.timeScale = 0f;
                isPaused = true;
                audioSource.Pause();
            }
            else if (isPaused == true)
            {
                Time.timeScale = 1f;
                isPaused = false;
                audioSource.UnPause();

            }
        }
    }
    IEnumerator TorqueRoutine()
    {
        float timer = 0f;
        while (timer < 2f)
        {
            rb.AddTorque(Vector3.forward * torqueForce);
            timer += Time.deltaTime;
            yield return null;
        }
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(10f);
        timer = 0f;
        while (timer < 10f)
        {
            rb.AddTorque(Vector3.forward * torqueForce);
            timer += Time.deltaTime;
            yield return null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            Debug.Log("Iniciando rotaci�n...");
            StartCoroutine(TorqueRoutine());
        }
    }
}

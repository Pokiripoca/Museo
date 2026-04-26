using UnityEngine;

public class Forces : MonoBehaviour
{
    public float forceAmount = 15f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Vector3.forward es el eje Z global (hacia los barriles)
            // Impulse es un golpe seco
            rb.AddForce(Vector3.forward * forceAmount, ForceMode.Impulse);
            Debug.Log("Impacto m�stico aplicado a: " + rb.linearVelocity);
        }
    }
}
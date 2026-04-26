using System.Collections;
using UnityEngine;

public class PauseSphere : MonoBehaviour
{

    Rigidbody rb;
    public float seconds = 4f;
    private bool Activado = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !Activado)
        {
            Activado = true;
            StartCoroutine(PauseElement(seconds));
            Debug.Log("¡Hechizo de tiempo activado!");
        }

    }
    IEnumerator PauseElement(float seconds)
    {
      ;
        yield return new WaitForSeconds(seconds);
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        Debug.Log("¡Hechizo de tiempo desactivado!");
    }
 
}

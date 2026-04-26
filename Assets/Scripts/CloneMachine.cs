using UnityEngine;
using System.Collections.Generic;

public class CloneMachine : MonoBehaviour
{
    public GameObject myPrefab;
    private List<GameObject> clones = new List<GameObject>();
    private bool encendido = false; 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!encendido)
            {
                GenerarCirculo();
                encendido = true;
            }
            else
            {
                LimpiarMesa();
                encendido = false;
            }
        }
    }

    void GenerarCirculo()
    {
        clones.Clear();

        for (int i = 0; i < 6; i++)
        {
            float angulo = i * (Mathf.PI * 2 / 6);
            float radio = 1.2f; 

            float x = Mathf.Cos(angulo) * radio;
            float z = Mathf.Sin(angulo) * radio;

            Vector3 posFinal = transform.position + new Vector3(x, 0.1f, z);

            GameObject nuevoClon = Instantiate(myPrefab, posFinal, Quaternion.identity);
            clones.Add(nuevoClon);
        }
    }

    void LimpiarMesa()
    {
        foreach (GameObject objeto in clones)
        {
            if (objeto != null)
            {
                Destroy(objeto);
            }
        }
        clones.Clear();
    }
}
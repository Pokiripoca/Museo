using UnityEngine;
using System.Collections.Generic;

public class s_PlanetarySystem : MonoBehaviour
{ [Header("cosos")]
    public GameObject prefabSol;
    public GameObject prefabPlanetas;

    [Header("Parametros")]
    public float distancia = 5f;
    public float rotacion = 50f;
    public float orbita = 50f;
    public int prefabs = 3;

    private Transform centralObject; 
    private List<Transform> orbitingObjects = new List<Transform>();

    void Start()
    {
        CreateCentralPrefab();
        CreateOrbitingPrefabs();
    }

    void Update()
    {
        RotateCentralObject();
        RotateAndOrbitPlanets();
    }

    void CreateCentralPrefab()
    {
        GameObject centerObject;
        if (prefabSol != null)
        {
            centerObject = Instantiate(prefabSol, transform.position, Quaternion.identity);
        }
        else
        {
            centerObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            centerObject.transform.position = transform.position;
        }

        centralObject = centerObject.transform;
        centralObject.localScale = Vector3.one * 2f; 
    }

    void CreateOrbitingPrefabs()
    {
        for (int i = 0; i < prefabs; i++)
        {
            GameObject newPrefab;

            float currentDistance = distancia + (distancia * i);
            Vector3 spawnPosition = transform.position + Vector3.right * currentDistance;

            if (prefabPlanetas != null)
            {
                newPrefab = Instantiate(prefabPlanetas, spawnPosition, Quaternion.identity);
            }
            else
            {
                newPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                newPrefab.transform.position = spawnPosition;
            }

            orbitingObjects.Add(newPrefab.transform);
        }
    }

    void RotateCentralObject()
    {
        if (centralObject == null) return;

        float angleChange = rotacion * Time.deltaTime;
        Quaternion rotationChange = Quaternion.AngleAxis(angleChange, Vector3.up);

        centralObject.rotation = (centralObject.rotation * rotationChange).normalized;
    }

    void RotateAndOrbitPlanets()
    {
        if (centralObject == null) return;

        for (int i = 0; i < orbitingObjects.Count; i++)
        {
            Transform currentObject = orbitingObjects[i];
            if (currentObject == null) continue;

            float angleChange = rotacion * Time.deltaTime;
            Quaternion rotationChange = Quaternion.AngleAxis(angleChange, Vector3.up);
            currentObject.rotation = (currentObject.rotation * rotationChange).normalized;

            float currentOrbitSpeed = orbita / (i + 1);

            currentObject.RotateAround(centralObject.position, Vector3.up, currentOrbitSpeed * Time.deltaTime);
        }
    }
}
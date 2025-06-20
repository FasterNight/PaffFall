using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabYSpawner : MonoBehaviour
{
    [Header("Prefabs à instancier")]
    public List<GameObject> prefabs;

    [Header("Paramètres d'instanciation")]
    public int nombreAInstancier = 5;
    public float intervalleEntreObjets = 2f;
    public float vitesseDeplacement = 2f;

    [Header("Parent pour les obstacles")]
    public Transform parentDesObstacles; 

    private List<GameObject> instances = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < nombreAInstancier; i++)
        {
            if (prefabs.Count == 0 || parentDesObstacles == null) return;

            GameObject prefabChoisi = prefabs[Random.Range(0, prefabs.Count)];

            Vector3 position = transform.position + new Vector3(0, i * intervalleEntreObjets, 0);
            GameObject instance = Instantiate(prefabChoisi, position, Quaternion.identity, parentDesObstacles);

            instances.Add(instance);
        }
    }

    void Update()
    {
        foreach (GameObject obj in instances)
        {
            if (obj != null)
            {
                obj.transform.Translate(Vector3.up * vitesseDeplacement * Time.deltaTime);
            }
        }
    }
}

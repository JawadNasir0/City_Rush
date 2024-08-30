using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public GameObject platformPrefab;
    private Vector3 spawnPos = new Vector3(0, 0,220);
     private Vector3 targetPosition = new Vector3(0, 0, 40);
    private float tolerance = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsApproximately(transform.position, targetPosition, tolerance))
        {
            Instantiate(platformPrefab, spawnPos, platformPrefab.transform.rotation);
        }
        Vector3 targetPosition2 = new Vector3(0,0,-90);

        if (Vector3.Distance(transform.position, targetPosition2) < tolerance)
        {
            Destroy(gameObject);
        }
    }

    bool IsApproximately(Vector3 a, Vector3 b, float tolerance)
    {
        return Vector3.Distance(a, b) < tolerance;
    }
}

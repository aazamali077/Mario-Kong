using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject Barrel;
    [SerializeField] float minTime, maxTime;
    // Start is called before the first frame update
    private void OnEnable()
    {
        InvokeRepeating(nameof(BarrelSpawner), 1, Random.Range(minTime, maxTime));
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void BarrelSpawner()
    {
        Instantiate(Barrel,transform);
    }
}

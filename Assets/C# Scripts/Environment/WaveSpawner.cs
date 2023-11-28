using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    float elapsed = 45f;
    float waveTime = 30f;
    [SerializeField] GameObject[] groups;
    int i = 1;
    [SerializeField] Transform spawnPoint;

    // Update is called once per frame
    void Update()
    {
        elapsed -= Time.deltaTime;
        if(elapsed <= 0){
            elapsed = waveTime;
            if(waveTime <= 10f){
                //Do nothing
            }
            else{
                waveTime -= 1f;
            }
            Instantiate(groups[i % groups.Length], spawnPoint.position, spawnPoint.rotation);
            i++;
        }
    }
}

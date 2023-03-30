using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRespawning : MonoBehaviour
{
    private List<Collider2D> colliderCheckArray;
    [SerializeField] private float radius;

    private float randomSizeNumber;
    private int randomIndexNumber;

    private float checkColliderTimer;

    private int colliderCheckCount;

    [SerializeField] private GameObject[] smallAsteroidArray;
    [SerializeField] private GameObject[] mediumAsteroidArray;
    [SerializeField] private GameObject[] largeAsteroidArray;

    [SerializeField] private float checkColliderResetTime;

    [SerializeField] private LayerMask layerMask;

    void Awake()
    {
        checkColliderTimer = checkColliderResetTime;
    }

    void FixedUpdate()
    {
        checkColliderTimer -= 1 * Time.deltaTime;
        if(checkColliderTimer <= 0){
            colliderCheckArray = new List<Collider2D>(Physics2D.OverlapCircleAll(gameObject.transform.position, radius, layerMask));
            if(colliderCheckArray.Count > 0){
                colliderCheckCount++;
                checkColliderTimer = checkColliderResetTime;
            }
            else{
                checkColliderTimer = checkColliderResetTime;
                colliderCheckCount = 0;
                randomSizeNumber = Random.Range(0, 1);
                randomIndexNumber = Mathf.FloorToInt(Random.Range(0, 7));
                if(randomSizeNumber <= 0.5f){
                    Instantiate(smallAsteroidArray[randomIndexNumber], gameObject.transform.position, Random.rotation);
                }
                else if(randomSizeNumber <= 0.8f && randomSizeNumber >= 0.5f){
                    Instantiate(mediumAsteroidArray[randomIndexNumber], gameObject.transform.position, Random.rotation);
                }
                else if(randomSizeNumber <= 1f && randomSizeNumber >= 0.8){
                    Instantiate(largeAsteroidArray[randomIndexNumber], gameObject.transform.position, Random.rotation);
                }
            }
        }
    }
}


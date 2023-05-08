using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    [SerializeField] private float range;

    private float elapsed;
    [SerializeField] float duration;

    public Collider2D enemy;
    List<Collider2D> colliderArray;
    List<Collider2D> enemies;

    [SerializeField] public LayerMask layermask;

    [SerializeField] WeaponClass[] weapons;

    [SerializeField] bool targetAsteroids = false;

    void Start(){
        colliderArray = new List<Collider2D>();
        enemies = new List<Collider2D>();
    }

    void FixedUpdate()
    {   
        elapsed += Time.deltaTime;

        if(elapsed >= duration) {
            elapsed = 0;
            enemies.Clear();
            colliderArray.Clear();
            colliderArray.AddRange(Physics2D.OverlapCircleAll(this.transform.position, range, layermask));

            if(targetAsteroids){
                foreach(Collider2D collider in colliderArray){
                    if(collider.tag != this.tag){
                        enemies.Add(collider);
                    }
                }
            }
            else{
                foreach(Collider2D collider in colliderArray){
                    if(collider.tag != this.tag){
                        enemies.Add(collider);    
                    }
                }
            }
            
            if(colliderArray.Count == 0){
                enemy = null;
                SetTarget(null);
                //Debug.Log("No enemies Detected");
            }
            if(enemy != null){
                if(Vector2.Distance(this.transform.position, enemy.transform.position) > range){
                    enemy = null;
                    SetTarget(null);
                    //Debug.Log("Out Of Range");    
                }
            }
            else if(enemies.Count > 0){
                SearchRadius(enemies);
            }
        }
    }

    private void SearchRadius(List<Collider2D> enemyList){
        float dist1 = 0;
        float dist2 = 0;
        Collider2D temp = null;
        foreach(Collider2D collider in enemyList){
            if(collider == enemy){
                SetTarget(enemy);
                return;
            }
            else if(enemy == null){
                enemy = collider;
                SetTarget(enemy);
                return;
            }
            dist1 = dist2;
            dist2 = Vector2.Distance(this.transform.position, collider.transform.position);
            if(dist2 > dist1){
                temp = collider;
            }
        }
        SetTarget(temp);
    }

    public void SetTarget(Collider2D temp){
        foreach(WeaponClass weapon in weapons){
            //Debug.Log(weapon);
            //Debug.Log(enemy);
            weapon.SetTarget(temp);
        }
    }

    public void SetTarget(Vector2 position){
        
    }
}

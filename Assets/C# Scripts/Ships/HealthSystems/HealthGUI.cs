using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGUI : MonoBehaviour
{
    [SerializeField] private Transform bar;

    public void SetSize(float sizeNormalized) {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    private float size = 1f;

    private float health;
    private float maxHealth;

    void Start()
    {
        SetSize(size);
    }

    public void UpdateHealth(int health, int maxHealth) {
        this.health = (float)health;
        this.maxHealth = (float)maxHealth;
        if(health > 0){
            size = (float)(this.health / this.maxHealth);
            SetSize(size);
        }
        else {
            size = 0f;
        }    
    }
    
}

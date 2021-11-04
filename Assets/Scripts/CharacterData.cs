using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class CharacterData : MonoBehaviour
{
    public Action OnHealthZero;
    public Action<float, float> OnHealthChanged;
    
    [SerializeField] private float health = 100f;
    [SerializeField] private float maxHealth = 100f;

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            if (health > value) Health = value;
        }
    }
    
    public float Health
    {
        get => health;
        set
        {
            var prev = health;
            health = Mathf.Clamp(value, 0f, maxHealth);
            OnHealthChanged?.Invoke(prev, health);
            if(health <= 0f) OnHealthZero?.Invoke(); 
        }
    }
}

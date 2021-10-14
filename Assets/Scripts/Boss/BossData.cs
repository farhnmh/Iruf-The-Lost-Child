using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class BossData : MonoBehaviour
{
    public Action OnHealthZero;
    public Action<float, float> OnHealthChanged;
    public float health = 100f;

    public float Health
    {
        get => health;
        set
        {
            var prev = health;
            health = Mathf.Clamp(value, 0f, float.MaxValue);
            OnHealthChanged?.Invoke(prev, health);
            if(health <= 0f) OnHealthZero?.Invoke(); 
        }
    }
}

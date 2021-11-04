using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    [Header ("Enemy Health Bar Attribute")]
    [SerializeField] Image healthBarEnemy;
    [SerializeField] BossData bossData;
    [SerializeField] float enemyMaxHealth = 100f;
    [SerializeField] float enemycurrentHealth = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemycurrentHealth = bossData.health;
        healthBarEnemy.fillAmount = enemycurrentHealth / enemyMaxHealth;
    }
}

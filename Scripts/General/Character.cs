using System;
using UnityEngine;

namespace General
{
    public class Character : MonoBehaviour
    {
        [Header("基本属性")]
        public float maxHealth;
        public float currentHealth;

        [Header("受伤无敌")]
        public float invulnerableDuration;//持续时间

        public float invulnerabileCounter;//无敌时间计数器;
        public bool invulnerable;//无敌
        
        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(Attack attacker)
        {
            if(invulnerable)
                return;
            if (currentHealth - attacker.damage > 0) //确实血量够扣
            {
                currentHealth -= attacker.damage;
                TriggerInvulnerable();
            }

            else
            {
                currentHealth = 0;
                //触发死亡
            }
                 
                
        }

        private void Update()
        {
            if (invulnerable)
            {
                invulnerabileCounter -= Time.deltaTime;//表示无敌时间逐渐减少
                if (invulnerabileCounter <= 0)
                {
                    invulnerable = false;   
                }
            }
            
        }

        private void TriggerInvulnerable()//触发无敌
        {
            if (!invulnerable)
            {
                invulnerable = true;
                invulnerabileCounter = invulnerableDuration;
            }
        }
    
    }
}

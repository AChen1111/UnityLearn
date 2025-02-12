using System;
using UnityEngine;

namespace General
{
    public class Attack : MonoBehaviour
    {
        public int damage;
        public float attackRange;
        public float attackRate;

        private void OnTriggerStay2D(Collider2D other)
        {
            other.GetComponent<Character>()?.TakeDamage(this); 
            //?表示目标对象是否有这个方法 如果有就执行 
        }
    }
}

using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {

        bool isDead = false;
        public float health = 100;

        public bool IsDead()
        {
            return isDead;
        }



        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();


        }
    }
}
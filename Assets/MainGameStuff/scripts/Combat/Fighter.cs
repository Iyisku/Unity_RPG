using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Health combatTarget;
        float TimeSinceLastAttack = Mathf.Infinity;



        public float TimeBetweenAttacks;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        public void Update()
        {
            TimeSinceLastAttack += Time.deltaTime;

            if (combatTarget == null) return; 

            if (combatTarget.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<mover>().MoveTo(combatTarget.transform.position);
            }
            else
            {
                GetComponent<mover>().Cancel();
                AttackBehaviour();
            }
        }


        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health TargetToTest = combatTarget.GetComponent<Health>();
            return TargetToTest != null && !TargetToTest.IsDead();
        }  
        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            
            combatTarget = target.GetComponent<Health>();

        }
        private void AttackBehaviour()
        {
            transform.LookAt(combatTarget.transform);
            if (TimeSinceLastAttack > TimeBetweenAttacks)
            {
                TriggerAttack();
                TimeSinceLastAttack = Mathf.Infinity;
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, combatTarget.transform.position) < weaponRange;
        }

      
        public void Cancel()
        {
            combatTarget = null;
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
            GetComponent<mover>().Cancel();
        }
        void Hit()
        {
            if (combatTarget == null) return;

            combatTarget.TakeDamage(weaponDamage);
        }


    }

}

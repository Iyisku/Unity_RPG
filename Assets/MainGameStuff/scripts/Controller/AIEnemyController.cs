using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Controller
{
    public class AIEnemyController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float SuspicionTime = 2f;
        [SerializeField] PatrolPath patrolPath; 
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 2f;


        Fighter fighter;
        GameObject player;
        Health health;
        mover Mover;
       
        Vector3 guardLocation;
        float timeSinceLastSeen = Mathf.Infinity;
        float timeSinceArrivedatWaypoint = Mathf.Infinity;
       
        int currentWaypointIndex = 0;
        void Start(){
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            Mover = GetComponent<mover>();
            guardLocation = transform.position;     
        }
        void Update()
        {
            if (health.IsDead()) { return; }
            if (DistanceToPlayer() && fighter.CanAttack(player))
            {
               
                AttackBehav();
            }
            else if (timeSinceLastSeen < SuspicionTime)
            {
                SuspiciousBehav();
            }

            else
            {
                PatrolBehav();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSeen += Time.deltaTime;
            timeSinceArrivedatWaypoint += Time.deltaTime;
        }

        private void PatrolBehav()
        { 
            Vector3 nextPosition = guardLocation;
            if(patrolPath != null){
                if(AtWaypoint()){
                    timeSinceArrivedatWaypoint = 0;
                    CycleWaypoint();
                }
            nextPosition = GetCurrentWaypoint();
            }
            if(timeSinceArrivedatWaypoint > waypointDwellTime){
               Mover.StartToMove(nextPosition); 
            }
            
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNext(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;

        }

        private void SuspiciousBehav()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehav()
        {
            timeSinceLastSeen = 0;
            fighter.Attack(player);
        }

        private bool DistanceToPlayer()
        {
            
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{   
    const float waypointgizmorad = 0.3f;
   private void OnDrawGizmos() {
        for(int i=0 ; i<transform.childCount; i++)
        {
            int j = GetNext(i);
            Gizmos.DrawSphere(GetWaypoint(i), waypointgizmorad);
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));

        }
    }

    public int GetNext(int i)
    {
        if(i + 1 == transform.childCount){
            return 0;
        }
        return i + 1;
    }

    public Vector3 GetWaypoint(int i)
    {
        return transform.GetChild(i).position;
    }
}

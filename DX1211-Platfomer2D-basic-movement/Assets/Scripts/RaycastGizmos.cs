using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class RaycastGizmos : GameGizmos
{
    // Start is called before the first frame update
   public override void Draw
        (GizmosRenderer r)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay
            (r.gameObject.transform.position, r.gameObject.transform.right * 2);
    }
}

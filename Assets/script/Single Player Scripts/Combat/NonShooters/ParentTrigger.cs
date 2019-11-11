using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentTrigger : MonoBehaviour {

    public void PullTrigger(Collider c)
    {
        if (!c.name.StartsWith("Enemy"))
            return;

        var destructible = c.GetComponent<Destructible>();
        destructible.TakeDamage(3);
    }
	
}

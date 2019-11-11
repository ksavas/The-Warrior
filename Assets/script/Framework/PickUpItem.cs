using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Player")
            return;
        PickUp(collider.transform);
    }
    public virtual void OnPickUpItem(Transform item)
    {

    }
    void PickUp(Transform item)
    {
        OnPickUpItem(item);
    }
}

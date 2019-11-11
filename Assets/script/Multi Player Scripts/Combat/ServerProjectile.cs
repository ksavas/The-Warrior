using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerProjectile : MonoBehaviour {

    Vector3 destination;
    [SerializeField]
    Transform bulletHole;


    void Start () {
        Destroy(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
        if (isDestinationReached())
        {
            Destroy(transform.gameObject);
            return;
        }
        transform.Translate(Vector3.forward * 120 * Time.deltaTime);
        if (destination != Vector3.zero)
            return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
        {
            CheckDestructible(hit);
        }
	}
    void CheckDestructible(RaycastHit hitInfo)
    {
        /*
        var destructable = hitInfo.transform.GetComponent<Destructible>();

        destination = hitInfo.point + hitInfo.normal * .0015f;

        Transform hole = (Transform)Instantiate(bulletHole, destination, Quaternion.LookRotation(hitInfo.normal) * Quaternion.Euler(0, 180, 0));
        hole.SetParent(hitInfo.transform);

        
        if (destructable == null)
            return;

        destructable.TakeDamage(1, GetComponent<Projectile>());

        if (destructable.name.Contains("Enemy"))
        {
            var enemy = hitInfo.transform.GetComponent<EnemyHealth>();
            enemy.GetBulletPosition(transform.position);
        }*/
    }

    bool isDestinationReached()
    {
        if (destination == Vector3.zero)
            return false;

        Vector3 directionToDestination = destination - transform.position;
        float dot = Vector3.Dot(directionToDestination, transform.forward);

        if (dot < 0)
            return true;

        return false;

    }
}

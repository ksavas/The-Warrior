using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] float damage;
    [SerializeField] Transform bulletHole;
    Vector3 destination;
    public string parentName;


    void Start()
    {

        Destroy(gameObject, timeToLive);// timeToLive sonra go destroy edilecek. - 2. paramatre timeToLive is optional.
    }
    void Update()
    {
        if(isDestinationReached())
        {
            Destroy(transform.gameObject);
            return;
        }


        transform.Translate(Vector3.forward * speed * Time.deltaTime);


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

        var destructable = hitInfo.transform.GetComponent<Destructible>();

        destination = hitInfo.point + hitInfo.normal*.0015f;

        Transform hole = (Transform)Instantiate(bulletHole, destination, Quaternion.LookRotation(hitInfo.normal) * Quaternion.Euler(0, 180, 0));
        hole.SetParent(hitInfo.transform);
       

        if (destructable == null)
            return;
        destructable.TakeDamage(damage,GetComponent<Projectile>());

        if (destructable.name.Contains("Enemy"))
        {
            var enemy = hitInfo.transform.GetComponent<EnemyHealth>();
            enemy.GetBulletPosition(transform.position);
        }
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

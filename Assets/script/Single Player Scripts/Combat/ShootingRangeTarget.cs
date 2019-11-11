using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider))]
public class ShootingRangeTarget : Destructible {

    [SerializeField] float rotationSpeed;
    [SerializeField] float repairTime;

    Quaternion initialRotation; 
    Quaternion targetRotation;

    bool requiresRotation;

    void Awake()
    {
        initialRotation = transform.rotation;
    }
    public override void TakeDamage(float amont, Projectile projectile)
    {
        base.TakeDamage(amont, projectile);
    }
    /*public override void TakeDamage(float amont)
    {
        base.TakeDamage(amont);
        print("Damaged");
    }*/
    public override void Die()
    {
        base.Die();
        targetRotation = Quaternion.Euler(transform.right * 90);
        requiresRotation = true;
        SecondGameManager.Instance.Timer.Add(() =>
        {
            targetRotation = initialRotation;
            requiresRotation = true;
        }, repairTime);
    }
    void Update()
    {
        if (!requiresRotation)
            return;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);
        if (transform.rotation == targetRotation)
            requiresRotation = false;
    }
}

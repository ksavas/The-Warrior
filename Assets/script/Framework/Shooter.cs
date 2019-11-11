using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Shooter : Weapon {

    [SerializeField] Projectile projectile;
    [SerializeField] AudioController audioReload;

    public Transform AimTarget;
    public Vector3 AimTargetOffset;

    int projId = 0;

    UdpSender client;
    Client c;

    public WeaponReloader reloader;
  //  public PlayerShoot playerShoot;



    private WeaponRecoil m_WeaponRecoil;
    private WeaponRecoil WeaponRecoil
    {
        get
        {
            if (m_WeaponRecoil == null)
                m_WeaponRecoil = GetComponent<WeaponRecoil>();
            return m_WeaponRecoil;
        }
    }

    public bool canFire;
    [SerializeField] Transform muzzle;
    private ParticleSystem muzzleParticles;

    void Awake()
    {
        muzzle = transform.Find("Model").transform.Find("Muzzle");
        reloader = GetComponent<WeaponReloader>();
        muzzleParticles = muzzle.GetComponent<ParticleSystem>();
        if (!GameManager.Instance.isSinglePlayer)
        {
            client = FindObjectOfType<UdpSender>();
            c = FindObjectOfType<Client>();
        }
    }

    public override void Equip()
    {
        base.Equip();
    }

    public void Reload(){
        if (reloader == null)
            return;
        reloader.Reload();
        audioReload.Play();
    }
    void FireEffect()
    {
        if (muzzleParticles == null)
            return;
        muzzleParticles.Play();
    }
    public override void Attack()
    {
    	base.Attack();
        
        canFire = false;

        if (Time.time < nextAttackAllowed)
            return;

        if (reloader != null)
        {
            if (reloader.IsReloading)
                return;
            if (reloader.RoundsRemainingInClip == 0)
                return;
            reloader.TakeFireFromClip(1);
        }

        nextAttackAllowed = Time.time + rateOfAttack;
        
        bool isLocalPlayerControlled = AimTarget == null;

        if(!isLocalPlayerControlled)
           muzzle.LookAt(AimTarget.position + AimTargetOffset);

       
        Projectile newBullet = (Projectile) Instantiate(projectile, muzzle.position, muzzle.rotation);
        newBullet.transform.SetParent(transform);
        projId++;
        if (!GameManager.Instance.isSinglePlayer)
        {
          //  c.Send("CFIRE|" + c.clientName + "|" + projId.ToString());
            c.Send("CFIRE|"+c.clientId+"|"+newBullet.transform.position.x.ToString()+","+newBullet.transform.position.y.ToString()+","+newBullet.transform.position.z.ToString());
            //client.SendInformation("CFIRE|" + c.clientId.ToString() + "|" + projId.ToString());
            //client.SendInformation("CFIRE|" + c.clientId.ToString() + "|" + transform.position.x + "," + transform.position.y + "," + transform.position.z +
            // "," + transform.rotation.eulerAngles.x.ToString() + "," + transform.rotation.eulerAngles.y.ToString() + "," + transform.rotation.eulerAngles.z.ToString());
        }
        if (newBullet.transform.root.name.Equals("PlayerContainer"))
            newBullet.transform.SetParent(transform.root.GetChild(1).transform);
        else if(newBullet.transform.root.name.Equals("EnemyContainer"))
        {
            while (true)
            {
                newBullet.transform.parent = newBullet.transform.parent.transform.parent;

                if (newBullet.transform.parent.name.StartsWith("EnemyPlayer"))
                    break;
            }
        }

        if(isLocalPlayerControlled)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            Vector3 targetPosition = ray.GetPoint(500);

            if (Physics.Raycast(ray, out hit))
                targetPosition = hit.point;

            newBullet.transform.LookAt(targetPosition+AimTargetOffset);

        }

        if (this.WeaponRecoil)
            this.WeaponRecoil.Activate();

        FireEffect();
        audioAttack.Play();

        canFire = true;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class WeaponRecoil : MonoBehaviour {


    [System.Serializable]
    public struct Layer
    {
        public AnimationCurve curve;
        public Vector3 direction;
    }

    [SerializeField]
    Layer[] layers;

    [SerializeField]
    float recoilSpeed;

    [SerializeField]
    float recoilCooldown;

    [SerializeField]
    float strength;

    float nextRecoilCooldown;
    float recoilActiveTime;

    Shooter m_Shooter;
    Shooter Shooter
    {
        get
        {
            if(m_Shooter == null)
                m_Shooter = GetComponent<Shooter>();
            return m_Shooter;
        }
    }
    CrossHair m_CrossHair;
    CrossHair CrossHair
    {
        get
        {
            if (m_CrossHair == null)
                m_CrossHair = SecondGameManager.Instance.LocaLPlayer.playerAim.GetComponentInChildren<CrossHair>();
            return m_CrossHair;
        }
    }
    public void Activate()
    {
        nextRecoilCooldown = Time.time + recoilCooldown;
    }

    void Update()
    {
        if (nextRecoilCooldown > Time.time)
        {
            recoilActiveTime += Time.deltaTime;
            float percentage = GetPercentage();


            Vector3 recoilAmount = Vector3.zero;
            
            for (int i = 0; i < layers.Length; i++)
            {
                recoilAmount += layers[i].direction * layers[i].curve.Evaluate(percentage);
            }

            this.Shooter.AimTargetOffset = Vector3.Lerp(Shooter.AimTargetOffset, Shooter.AimTargetOffset + recoilAmount,strength * Time.deltaTime);

            this.CrossHair.ApplyScale(percentage * Random.RandomRange(strength * 7,strength*9));

        }
        else
        {
            recoilActiveTime -= Time.deltaTime;

            if (recoilActiveTime < 0)
                recoilActiveTime = 0;

            this.CrossHair.ApplyScale(GetPercentage());

            if (recoilActiveTime == 0)
            {
                this.Shooter.AimTargetOffset = Vector3.zero;
                this.CrossHair.ApplyScale(0);
            }
   
        }
    }

    float GetPercentage()
    {
        float percentage = recoilActiveTime / recoilSpeed;
        return Mathf.Clamp01(percentage);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : PickUpItem {

    [SerializeField] ParticleSystem fire1;
    [SerializeField] ParticleSystem fire2;
    [SerializeField] AudioController audio;
    bool jetPackTaken;
    [SerializeField]float gasRemainedInTube;
    Transform player;
    public PickUpController pickUpContainer;
    public ServerPickUpController sPickUpController;
    Client c;
    public float gasRemaining
    {
        get
        {
            return gasRemainedInTube;
        }
        set
        {
            gasRemainedInTube = value;
        }
    }

    public override void OnPickUpItem(Transform item)
    {
        if (item.name == "Health")
            return;
        if (item.GetComponent<ServerPlayer>() != null)
        {
            //if (!GameManager.Instance.isSinglePlayer)
              //  sPickUpController.SendDestroyJetPack();
            player = item;
            transform.SetParent(item.GetComponentInChildren<JetPackContainer>().transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            return;
        }
        player = item;
        transform.SetParent(item.GetComponentInChildren<JetPackContainer>().transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        jetPackTaken = true;
        item.GetComponent<Player>().hasJetPack = true;
    }

    void Update()
    {
        if (player == null)
            return;
        if(GameManager.Instance.isSinglePlayer){
            if (!player.GetComponent<PlayerHealth>().isAlive)
            {
                GetComponent<BoxCollider>().isTrigger = false;
                SecondGameManager.Instance.Timer.Add(() =>
                {
                    GetComponent<BoxCollider>().isTrigger = true;
                }, 3);
                transform.SetParent(null);
                MonoBehaviour[] children = GetComponentsInChildren<MonoBehaviour>();
                transform.gameObject.layer = 8;
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].transform.gameObject.layer = 8;
                }

                jetPackTaken = false;
                player.GetComponent<Player>().hasJetPack = false;
                player = null;
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            }
        }


        if (gasRemainedInTube <= 0)
        {
            gasRemainedInTube = 0;
            jetPackTaken = false;
            player.GetComponent<Player>().hasJetPack = false;

            if (player.GetComponent<ServerPlayer>() != null)
            {
                return;
            }
            if (!GameManager.Instance.isSinglePlayer)
            {
                sPickUpController = FindObjectOfType<ServerPickUpController>();
                sPickUpController.SendDestroyJetPack();
            }

            if (GameManager.Instance.isSinglePlayer)
            {
                pickUpContainer = FindObjectOfType<PickUpController>();
                pickUpContainer.jetPackIsDead = true;
                Destroy(transform.gameObject);
            }

        }
       if(jetPackTaken&&SecondGameManager.Instance.InputController.jetPackFire)
       {
           fire2.enableEmission = true;
           fire1.enableEmission = true;
           audio.Play();
           gasRemainedInTube--;
           return;
       }
           fire2.enableEmission = false;
           fire1.enableEmission = false;

           
       }
   
    }
   

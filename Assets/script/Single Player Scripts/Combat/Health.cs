using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : PickUpItem {

    public PickUpController pickUpContainer;
    public HealthCounter counter;
    public ServerPickUpController sPickUpController;
    Client c;
    public override void OnPickUpItem(Transform item)
    {
        if (item.name == "Jet Pack")
            return;
        if (item.GetComponent<ServerPlayer>() != null)
        {
            return;
        }
        if (!GameManager.Instance.isSinglePlayer)
        {
            FindObjectOfType<ServerHealtCounter>().SetHealth();
            c = FindObjectOfType<Client>();
            c.Send("CHUPT|"+c.clientId.ToString());
            sPickUpController = FindObjectOfType<ServerPickUpController>();
            sPickUpController.SendDestroyHealth();
            Destroy(transform.gameObject);
        }

        
        if (GameManager.Instance.isSinglePlayer)
        {
            print("1");
            item.GetComponent<PlayerHealth>().hitPointsRemaining = item.GetComponent<PlayerHealth>().hitPointsRemaining + 24;
            print("2");
            counter.OnHealthGained();
            print("3");
            pickUpContainer = FindObjectOfType<PickUpController>();
            print("4");
            pickUpContainer.healthGained = true;
            print("5");
            Destroy(transform.gameObject);
            print("6");
        }
    }
}

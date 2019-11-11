using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Container : MonoBehaviour {

    [System.Serializable]
    public class ContainerItem
    {
        public System.Guid id;
        public string name;
        public int maximum;

        public int amountTaken;

        public ContainerItem(){
            id = System.Guid.NewGuid();
        }

        public int remaining
        {
            get{
               return maximum - amountTaken;
            }
        }

        public int Get(int value)
        {
            if ((amountTaken + value) > maximum)
            {
                int toMuch = (amountTaken + value) - maximum;
                amountTaken = maximum;
                return value - toMuch;
            }

            amountTaken += value;
            return value;
        }
        public void Set(int amount)
        {
            amountTaken -= amount;
            if (amountTaken < 0)
                amountTaken = 0;
        }
    }

    public List<ContainerItem> items;
    public event System.Action OnContainerReady;

    void Awake()
    {
        items = new List<ContainerItem>();
        if (OnContainerReady != null)
            OnContainerReady();

    }
    public System.Guid Add(string name, int maximum)
    {
        items.Add(new ContainerItem
        {
            id = System.Guid.NewGuid(),
            name = name,
            maximum = maximum
        });

        return items.Last<ContainerItem>().id;
    }
    public void Put(string name, int amount)
    {
        var containerItem = items.Where(x => x.name == name).FirstOrDefault();
        if (containerItem == null)
            return;
        containerItem.Set(amount);
    }

    public int TakeFromContainer(System.Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
            return -1;
        return containerItem.Get(amount); 
    }
    public int GetAmountRemaining(System.Guid id)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
            return -1;
        return containerItem.remaining;
    }
    private ContainerItem GetContainerItem(System.Guid id)
    {
        var containerItem = items.Where(x => x.id == id).FirstOrDefault();
        return containerItem;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool pickedUp;
    public int iD;
    public string type;
    public string description;
    public Sprite icon;

    public Item(bool pickedUp, int iD, string type, string description, Sprite icon)
    {
        this.pickedUp = pickedUp;
        this.iD = iD;
        this.type = type;
        this.description = description;
        this.icon = icon;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorable
{
    // // Probably delete later if image static
    // public int iD;
    // public string type;
    // public string description;


    // Acessers
    List<Action> ItemActions { get; }
    GameObject ItemGameObjectForInspect { get; }
    Sprite Icon { get; }


    // Methods
    void StoreItem();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName ="Interactable/Item")]

public class Item : MonoBehaviour
{
    // List of actions for the item
    [SerializeField]
    public List<Action> itemActions;
    
    // Item for inspect item menu and inventory icon
    public GameObject itemGameObjectForInspect;
    public Sprite icon;

    // Probably delete later if image static
    public int iD;
    public string type;
    public string description;

  
}


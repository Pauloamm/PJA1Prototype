using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorable
{
	// // Probably delete later if image static
	// public int iD;
	// public string description;


	// Acessers
	List<Action> ItemActions { get; }
    GameObject ItemGameObjectForInspect { get; }
    Sprite Icon { get; }
	int Quantity { get; set; }
	string Type { get; set; }

    // Methods
    void StoreItem();
}
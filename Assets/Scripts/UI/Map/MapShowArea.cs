using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShowArea : MonoBehaviour
{
	[SerializeField] GameObject mapArea;

	void OnTriggerEnter(Collider other)
	{
		mapArea.SetActive(true);
	}
}

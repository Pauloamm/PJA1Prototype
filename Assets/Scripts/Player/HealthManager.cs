using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	[SerializeField]
	DisplayPanel displayPanel;
	Health playerHealth;
	bool addHealth;
    // Start is called before the first frame update
    void Awake()
    {
		playerHealth = new Health(50);
		displayPanel.HealthPlus += AddHealth;
    }

    // Update is called once per frame
    void Update()
    {
		if (!addHealth) return;
        
		if (addHealth)
		{
			playerHealth.health++;
		}
		

		Debug.Log(playerHealth.health);
    }


	private void AddHealth()
	{
		addHealth = true;
	}
}

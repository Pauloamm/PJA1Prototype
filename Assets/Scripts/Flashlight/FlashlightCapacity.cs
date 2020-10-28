using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlashlightCapacity : MonoBehaviour
{
    [SerializeField]
    GameObject miniB1, miniB2, miniB3, miniB4;
    [SerializeField]
    GameObject Text1;
    [SerializeField]
    Flashlight flashlight;


    // Update is called once per frame
    void Update()
    {

        Text1.GetComponent<Text>().text = flashlight.currentBatteryPercentage + "%";



        if (flashlight.currentBatteryPercentage == 100)
        {
            miniB1.SetActive(true);
            miniB2.SetActive(true);
            miniB3.SetActive(true);
            miniB4.SetActive(true);
        }
        else if (flashlight.currentBatteryPercentage == 75)
        {
            miniB1.SetActive(false);

        }
        else if (flashlight.currentBatteryPercentage == 50)
        {
            miniB2.SetActive(false);
        }
        else if (flashlight.currentBatteryPercentage == 25)
        {
            miniB3.SetActive(false);
        }
        else if (flashlight.currentBatteryPercentage == 0)
        {
            miniB4.SetActive(false);
        }



    }
}

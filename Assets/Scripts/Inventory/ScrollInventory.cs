using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollInventory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject slotHolder;
    private float scrollSensibility = 5f;
    private float max = 326f;
    private float min = 5f;

    void Awake()
    {
        slotHolder = GameObject.Find("Slot Holder");
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 temp = this.transform.position + Input.mouseScrollDelta * Time.deltaTime * 2f;
        //this.transform.position += Input.mouseScrollDelta.y * Time.deltaTime * 2f;

        this.transform.position = new Vector3(
            transform.position.x, transform.position.y + Input.mouseScrollDelta.y * scrollSensibility, transform.position.z);
    }
}

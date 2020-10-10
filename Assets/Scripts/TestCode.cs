using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using Service;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    // Start is called before the first frame update
    public SlotService _slotService = new SlotService();
    
    void Start()
    {

        Action<Stall> action = o =>
        {
            Debug.Log(o.description);
        };
        
        StartCoroutine(_slotService.GETStall(1, action));
        Debug.Log("AA");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

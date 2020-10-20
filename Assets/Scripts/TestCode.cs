using System;
using Model;
using Service;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    private DrinkService _drinkService = new DrinkService();
    private SlotService _slotService = new SlotService();
    private StallService _stallService = new StallService();

    void Start()
    {
        testStallService();
        testDrinkService();
        testSlotService();
    }

    void testStallService()
    {
        Action<Stall> action = o => { Debug.Log(o.description); };
        StartCoroutine(_stallService.GETStall(1, action));
    }

    void testDrinkService()
    {
        StartCoroutine(_drinkService.GETDrinkList(o =>
        {
            foreach (Drink drink in o)
            {
                Debug.Log(drink.description);
            }
        }));
        StartCoroutine(_drinkService.GETDrink(1, o =>
        {
            Debug.Log(o.description);
        }));
    }

    void testSlotService()
    {
        StartCoroutine(_slotService.GETSlotList(o =>
        {
            foreach (Slot slot in o)
            {
                Debug.Log(slot.column);
            }
        }));
        StartCoroutine(_slotService.GETSlot(2, o =>
        {
            Debug.Log(o.incoming_time);
        }));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
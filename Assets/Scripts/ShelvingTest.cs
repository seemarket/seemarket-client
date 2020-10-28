using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelvingTest : MonoBehaviour
{
    public PrefabCollection drink_collection;
    
    public Vector3 ToVector3(Model.Slot slot)
    {
        return new Vector3(slot.row, slot.column, slot.depth);
    }
    // Start is called before the first frame update
    void Start()
    {
        // 시나리오
        // 음료는 현재 5종류가 있다.
        // Drink 1, 2, 3, 4, 5는 각각 PrefabCollection의 index를 id로 인식한다.
        
        // Row Column Depth = 3차원의 데이터를 알려준다.
        // Row, Col, Depth. =  아이템의 좌표다. x, y, z vector 값.
        // x, y, z vector값으로 

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

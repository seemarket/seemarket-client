using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 슬롯의 오브젝트
///</summary>
public class SlotObject : MonoBehaviour
{
    public Model.Slot data;

    [SerializeField]
    public DrinkObject drink_object;

    public bool IsEmpty { get { return data.has_drink; } }
    public int id { get { return data.id; } }
    public Vector3 position
    {
        get
        {
            return new Vector3(data.row, data.column, data.depth);
        }
    }
    
    public void InitializeObject()
    {

    }
}

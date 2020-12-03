using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShelfObject : MonoBehaviour
{
    ///<sumamry>
    /// Slot 인덱스의 상품 가져오기
    ///</summary>
    public List<SlotObject> slotData = new List<SlotObject>();


    // 0.08f*0,1,2,3,4..11
    // 1.7016f
    // 0.21/3*0,1,2,3

    ///<sumamry>
    /// Slot 인덱스의 상품 가져오기
    ///</summary>
    public Dictionary<int, DrinkObject> dic
        = new Dictionary<int, DrinkObject>();

    private void Awake() {

        if (CObjectPool.Instance.main == null)
            CObjectPool.Instance.main = this;
    }
    [ContextMenu("Force Reset&Init")]
    public void forceInitialize()
    {
        Initialize(CLocalDatabase.Instance.SlotDB.Values.ToList());
    }
    
    // 모든 곳은 슬롯이 1순위이다.
    // 슬롯은 occupied, empty, edit, holder,
    public void Initialize(List<Model.Slot> slot_data)
    {
        // Reset 코드.
        foreach (var obj in dic.Values)
        {
            if (obj != null)
                obj.DestoryObject();
        }
        dic.Clear();

        // 재생성
        foreach (var slot in slot_data)
        {
            DrinkObject go = null;
            if (slot.has_drink)
            {
                // 위치 설정
                go = AddDrinkObject(slot.drink_id, slot.row, slot.column, slot.depth);
            }
            // 목록에 추가
                dic.Add(slot.id, go);
        }
    }

    public DrinkObject AddDrinkObject(int drinkID, float row, float col, float depth)
    {
        DrinkObject go = CObjectPool.Instance.CreateDrinkObject(CLocalDatabase.GetProductInfo(drinkID));
        go.transform.SetParent(this.gameObject.transform);
        go.transform.localRotation = Quaternion.identity;
                go.gameObject.SetActive(true);
                go.transform.localPosition = new Vector3(
                    row,
                    col,
                    depth);
        return go;
    }

    public void HandleSlotUpdate(Model.SlotUpdate _)
    {
        //Debug.Log("[123123123] Updated");
        if (_.before_slot_info.has_drink)
        {
            // soldout
            dic[_.before_slot_info.id].DestoryObject();
            dic[_.before_slot_info.id] = null;
        }
        else
        {
            // arrived
            dic.Add(
                key: _.updated_slot_info.id, 
                value: AddDrinkObject(_.updated_slot_info.drink_id, 
                    _.updated_slot_info.row,
                    _.updated_slot_info.column,
                    _.updated_slot_info.depth));
        }
    }

    ///<sumamry>
    /// Slot 인덱스의 상품 가져오기
    ///</summary>
    public SlotObject GetSlotByIndex(int id)
    {
        return slotData.Find(_ => _.id == id);
    }

    ///<sumamry>
    /// 특정 위치의 상품 가져오기
    ///</summary>
    public SlotObject GetSlotByPosition(int row, int col, int depth)
    {
        return slotData.Find(_ =>
            _.data.row == row &&
            _.data.column == col &&
            _.data.depth == depth);
    }

    ///<sumamry>
    /// Drink ID로 시작하는 모든 상품 가져오기
    ///</summary>
    public List<SlotObject> GetSlotsByDrinkID(int drink_id)
    {
        return slotData.FindAll(_ => _.data.drink_id == drink_id);
    }

    ///<sumamry>
    /// 특정 row에 있는 모든 상품 가져오기
    ///</summary>
    public List<SlotObject> GetSlotsByRow(int row)
    {
        return slotData.FindAll(_ => _.data.row == row);
    }

    ///<sumamry>
    /// 특정 row && col에 있는 모든 상품 가져오기
    ///</summary>
    public List<SlotObject> GetSlotsByRowCol(int row, int col)
    {
        return slotData.FindAll(_ => _.data.row == row && _.data.column == col);
    }

    /// <summary>
    /// 모든 값들 다 처리해서 정상적으로 보이도록 하기
    /// </summary>
    public void InitializeDrinks()
    {
        foreach (Model.Slot s in CLocalDatabase.Instance.SlotDB.Values)
        {
            if (s.has_drink)
            {
                var go = CObjectPool.Instance.CreateDrinkObject(CLocalDatabase.GetProductInfo(s.drink_id));
                go.transform.SetParent(this.gameObject.transform);
                go.transform.localRotation = Quaternion.identity;
                go.gameObject.SetActive(true);
                go.transform.localPosition = new Vector3(
                    s.row,
                    s.column,
                    s.depth);
            }
        }
    }
    
    public List<Transform> row_transforms;

    [Range(0, 3)]
    public int test_row = 1;
    [Range(0, 11)]
    public int test_col = 2;
    [Range(0, 3)]
    public int test_depth = 3;

    [ContextMenu("Drink setup test")]
    public void Test()
    {
        foreach (Model.Slot s in CLocalDatabase.Instance.SlotDB.Values)
        {
            if (s.has_drink)
            {
                var go = CObjectPool.Instance.CreateDrinkObject(CLocalDatabase.GetProductInfo(s.drink_id));
                go.transform.SetParent(this.gameObject.transform);
                go.transform.localRotation = Quaternion.identity;
                go.gameObject.SetActive(true);
                go.transform.localPosition = new Vector3(
                    s.column,
                    s.row,
                    s.depth );
            }
        }
    }
    
}

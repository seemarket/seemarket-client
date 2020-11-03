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

    readonly float offset_x = 0.08f;
    readonly float offset_y = 0.0784f;
    readonly float offset_z = 0.07f;

    // 0.08f*0,1,2,3,4..11
    // 1.7016f
    // 0.21/3*0,1,2,3

    ///<sumamry>
    /// Slot 인덱스의 상품 가져오기
    ///</summary>
    public Dictionary<int, DrinkObject> dic
        = new Dictionary<int, DrinkObject>();

    // 모든 곳은 슬롯이 1순위이다.
    // 슬롯은 occupied, empty, edit, holder,
    // 

    public void Initialize(List<Model.Slot> slot_data)
    {
        dic.Clear();
        foreach (var slot in slot_data)
        {
            dic.Add(slot.id, new DrinkObject());
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

    public List<Transform> row_transforms;

    public int test_row = 1;
    public int test_col = 2;
    public int test_depth = 3;

    [ContextMenu("Drink setup test")]
    public void Test()
    {
        if (slotData.Any(_ =>
            _.data.row == test_row &&
            _.data.column == test_col &&
            _.data.depth == test_depth))
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog(
                "오류",
                "이미 할당된 슬롯입니다.", "확인");
#endif
        }
        else if (test_row < 0 || test_row >= row_transforms.Count)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog(
                "오류",
                "0~3사이.", "확인");
#endif
        }
        else
        {
            var go = CObjectPool.Instance.CreateDrinkObject(null);
            var curSlot = new Model.Slot()
            {
                row = test_row,
                column = test_col,
                depth = test_depth
            };

            go.transform.SetParent(row_transforms[curSlot.row]);
            go.transform.localRotation = Quaternion.identity;
            go.gameObject.SetActive(true);
            go.transform.localPosition = new Vector3(
                curSlot.column * offset_x,
                offset_y,
                curSlot.depth * offset_z);
            /*slotData.Add(new SlotObject()
            {
                row = test_row,
                column = test_col,
                depth = test_depth
            });
            */
        }
    }
}

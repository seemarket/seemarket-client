using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Model;
using UnityEngine.UI;


public enum ShelfMode
{
    EDIT, VIEW
}
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

    /// <summary>
    /// 현재 매출
    /// </summary>
    public int currentPrice = 0;
    
    public ShelfMode shelfMode = ShelfMode.EDIT;

    public Text priceText;
    
    private void Awake() {

        if (CObjectPool.Instance.main == null)
            CObjectPool.Instance.main = this;
    }
    
    
    [ContextMenu("Force Reset&Init")]
    public void forceInitialize(ShelfMode shelfmode)
    {
        this.shelfMode = shelfmode;
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

        foreach (Transform child in this.transform) {
            GameObject.Destroy(child.gameObject);
        }
        
        // 재생성
        foreach (var slot in slot_data)
        {
            DrinkObject go = null;
            if (slot.has_drink)
            {
                // 위치 설정
                go = AddDrinkObject(slot);
            }
            // 목록에 추가
                dic.Add(slot.id, go);
        }
    }


    /**
     * 새로운 상품 객체를 할당한다.
     */
    public void HandleCreateProduct(Model.Slot slot)
    {
        DrinkObject go = null;
        if (slot.has_drink)
        {
            // 위치 설정
            go = AddDrinkObject(slot);
        }
        // 목록에 추가
        dic.Add(slot.id, go);
    }
    
    public DrinkObject AddDrinkObject(Model.Slot slot)
    {

        int drinkID = slot.drink_id;
        float row = slot.row;
        float col = slot.column;
        float depth = slot.depth;
        DrinkObject go = CObjectPool.Instance.CreateDrinkObject(CLocalDatabase.GetProductInfo(drinkID));
        go.transform.SetParent(this.gameObject.transform);
        if (this.shelfMode == ShelfMode.EDIT)
        {
            go.isClickable = false;
            go.isDragable = true;

        }
        else
        {
            go.isClickable = true;
            go.isDragable = false;
        }
        go.SetSlotData(slot);
        go.transform.localRotation = Quaternion.identity;
                go.gameObject.SetActive(true);
                go.transform.localPosition = new Vector3(
                    row,
                    col,
                    depth);
        return go;
    }

    public void HandlePriceUpdate(PriceUpdate _)
    {
        currentPrice = _.price;
        if (priceText != null)
        {
            priceText.text = "예상 매출 : " + currentPrice.ToString() + "원";
        }

    }
    
    public void HandleSlotUpdate(Model.SlotUpdate _)
    {

        switch (_.GETUpdateType())
        {
            case UpdateType.SOLD_OUT:
                dic[_.before_slot_info.id].frame = 0;
                StartCoroutine(dic[_.before_slot_info.id].Diasppear());
                dic[_.before_slot_info.id] = null;
                break;
            case UpdateType.ARRIVED:
                dic.Add(
                    key: _.updated_slot_info.id, 
                    value: AddDrinkObject(_.updated_slot_info));
                dic[_.before_slot_info.id].frame = 0;
                StartCoroutine(dic[_.before_slot_info.id].Appear());
                break;
            case UpdateType.MOVE:
                Vector3 toward = new Vector3(_.updated_slot_info.row, _.updated_slot_info.column, _.updated_slot_info.depth);
                dic[_.before_slot_info.id].frame = 0;
                StartCoroutine(dic[_.before_slot_info.id].Move(toward));
                break;
            case UpdateType.CHANGE:
                Model.Product drink = CLocalDatabase.GetProductInfo(_.updated_slot_info.drink_id);
                dic[_.before_slot_info.id].Setup(drink);
                break;
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
        Initialize(CLocalDatabase.Instance.SlotDB.Values.ToList());
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using Service;
using SocketIO;
using Unity.Profiling;
using UnityEngine;

public class CLocalDatabase : CSingletonMono<CLocalDatabase>
{
    DrinkService _drinkService = new DrinkService();
    SlotService _slotService = new SlotService();
    StallService _stallService = new StallService();

    public Dictionary<int, Product> ProductDB = new Dictionary<int, Product>();
    public Dictionary<int, Slot> SlotDB = new Dictionary<int, Slot>();


    public bool didFetchDrink = false;
    public bool didFetchSlot = false;
    
    public static Product GetProductInfo(int id)
    {
        return Instance.ProductDB.ContainsKey(id)
            ? Instance.ProductDB[id] : null;
    }

    private string socketURL =
        "ws://ec2-13-209-66-8.ap-northeast-2.compute.amazonaws.com:8080/socket.io/?EIO=4&transport=websocket";
    
    void Start()
    {
        // 시작 시 음료의 목록을 서버에서 받아온다.
        HandleGetProductList();
        HandleGetSlotList();
        
        // 시작 시 현재 매장의 상황.
        SocketIOComponent.Instance.On("open", HandleOpen);
        SocketIOComponent.Instance.On("error", HandleError);
        SocketIOComponent.Instance.On("close", HandleClose);
        SocketIOComponent.Instance.On("update", HandleSlotUpdate);
        
        // 시뮬레이션 결과를 수신함.
        SocketIOComponent.Instance.On("reset_result", HandleResetSimulation);
        SocketIOComponent.Instance.On("create_result", HandleCreateProduct);
        SocketIOComponent.Instance.On("delete_result", HandleDeleteProduct);
        SocketIOComponent.Instance.On("move_result", HandleMoveProduct);
        
        
    }
    #region HANDLE CODES

    public void FireSimulation()
    {
        
        // 1초마다 업데이트 처리를 하는 시뮬레이션을 호출합니다.
        //  StartCoroutine(StartSimulation());
    }
    public void HandleGetProductList()

    {
        ProductDB.Clear();
        StartCoroutine(_drinkService.GETDrinkList(o => {
            Debug.Log("++++음료수가 업데이트 되었습니다.+++++");
            foreach (Product d in o)
            {
                ProductDB.Add(d.id, d);
                Debug.Log(string.Format("음료:{0}/{1}",
                    d.id, d.title));
            }
            
            this.didFetchDrink = true;
            HandleStallInit();
        }));
    }
    public void HandleGetSlotList()
    {
        SlotDB.Clear();
        StartCoroutine(_slotService.GETSlotList(o => {
            Debug.Log("++++진열대 정보가 업데이트 되었습니다.+++++");
            foreach (Slot s in o)
            {
                SlotDB.Add(s.id, s);
                Debug.Log(string.Format("슬롯:[{0}/{1}/{2}] {3}",
                    s.row, s.column, s.depth, s.has_drink));
            }

            this.didFetchSlot = true;
            HandleStallInit();
        }));
    }

    private void HandleStallInit()
    {
        if (didFetchDrink && didFetchSlot)
        {
            if (CObjectPool.Instance.main != null)
            {
                CObjectPool.Instance.main.InitializeDrinks();
            }
            
        }
        
    }
    
    // 1초마다 재고 여부가 바뀌는 것을 알리는 코드를 수신합니다.
    public void HandleSlotUpdate(SocketIOEvent e)
    {
        //Debug.Log("[SocketIO] Update received: " + e.name + " " + e.data);
        if (e.data == null) { return; }
        string rawString = e.data.ToString();
        SlotUpdate update = JsonUtility.FromJson<SlotUpdate>(rawString);
        //Debug.Log(update.ToString());

        // 이벤트 핸들러
        //update.updated_slot_info.id
        var prev = update.before_slot_info;
        var next = update.updated_slot_info;

        // sold out
        CObjectPool.Instance.main.HandleSlotUpdate(update);

        if (SlotDB.ContainsKey(update.updated_slot_info.id))
        {
            SlotDB[update.updated_slot_info.id] = update.updated_slot_info;
        }
    }
    
    private IEnumerator StartSimulation()
    {
        // wait 1 seconds and continue
        yield return new WaitForSeconds(1);
		
        //SocketIOComponent.Instance.Emit("start_simulation");
        //ResetSimulation();
        
        /**
        CreateCommand createCommand = new CreateCommand();
        createCommand.product_id = 1;
        createCommand.row = 1.45;
        createCommand.depth = 1.5677;
        createCommand.column = 1.6677;
        CreateProduct(createCommand);
        
        **/
        
        /**
        MoveCommand moveCommand = new MoveCommand();
        moveCommand.slot_id = 1470;
        moveCommand.row = 1111.45;
        moveCommand.depth = 111.5677;
        moveCommand.column = 111.6677;
        MoveProduct(moveCommand);
        **/
        
        /**
        DeleteCommand deleteCommand = new DeleteCommand();
        deleteCommand.slot_id = 1470;
        DeleteProduct(deleteCommand);
    */
    }

    /// <summary>
    /// 초기화하고 배치를 원래대로 되돌린다.
    /// </summary>
    private void ResetSimulation()
    {
        SocketIOComponent.Instance.Emit("reset_simulation");
    }
    
    /// <summary>
    /// 초기화 후 결과를 통지받는다.
    /// </summary>
    public void HandleResetSimulation(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Handle Reset received: " + e.name + " " + e.data);
        string rawString = e.data.ToString();
        Response<SlotService.SlotListResponse> value = JsonUtility.FromJson<Response<SlotService.SlotListResponse>>(rawString);
        
        Debug.Log("simulation result" + rawString);
    }
    
    
    /// <summary>
    /// 상품을 매대에 조작하여 추가한다.
    /// </summary>
    private void CreateProduct(CreateCommand createCommand)
    {
        JSONObject request = new JSONObject(
            JsonUtility.ToJson(createCommand));
        SocketIOComponent.Instance.Emit("create_product", request);
    }

    /// <summary>
    /// 초기화 후 결과를 통지받는다.
    /// </summary>
    public void HandleCreateProduct(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Handle create received: " + e.name + " " + e.data);
        string rawString = e.data.ToString();
        Response<SlotService.SlotResponse> value = JsonUtility.FromJson<Response<SlotService.SlotResponse>>(rawString);
        
        Debug.Log("create result" + rawString);
    }

    
    /// <summary>
    /// 상품을 매대에 조작하여 이동한다.
    /// </summary>
    public void MoveProduct(MoveCommand moveCommand)
    {
        JSONObject request = new JSONObject(
            JsonUtility.ToJson(moveCommand));
        SocketIOComponent.Instance.Emit("move_product", request);
    }

    
    /// <summary>
    /// 초기화 후 결과를 통지받는다.
    /// </summary>
    public void HandleMoveProduct(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Handle move received: " + e.name + " " + e.data);
        string rawString = e.data.ToString();
        Response<SlotService.SlotResponse> value = JsonUtility.FromJson<Response<SlotService.SlotResponse>>(rawString);
        
        Debug.Log("move result" + rawString);
    }

    
    /// <summary>
    /// 상품을 매대에 조작하여 제거한다.
    /// </summary>
    private void DeleteProduct(DeleteCommand deleteCommand)
    {
        JSONObject request = new JSONObject(
            JsonUtility.ToJson(deleteCommand));
        SocketIOComponent.Instance.Emit("delete_product", request);
    }
    
    
    
    /// <summary>
    /// 초기화 후 결과를 통지받는다.
    /// </summary>
    public void HandleDeleteProduct(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Handle delete received: " + e.name + " " + e.data);
        string rawString = e.data.ToString();
        Response<SlotService.SlotDeleteResponse> value =  JsonUtility.FromJson<Response<SlotService.SlotDeleteResponse>>(rawString);
        
        Debug.Log("delete result" + rawString);
    }
    
    public void HandleOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }
	
    public void HandleError(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }
	
    public void HandleClose(SocketIOEvent e)
    {	
        Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }
    #endregion

    #region UPDATE LIST

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using Service;
using SocketIO;
using Unity.Profiling;
using UnityEngine;

public class CWebData : CSingletonMono<CWebData>
{
    DrinkService _drinkService = new DrinkService();
    SlotService _slotService = new SlotService();
    StallService _stallService = new StallService();

    public Dictionary<int, Drink> DrinkDB = new Dictionary<int, Drink>();
    public Dictionary<int, Slot> SlotDB = new Dictionary<int, Slot>();
    
    public static Drink GetDrinkModel(int id)
    {
        return Instance.DrinkDB.ContainsKey(id)
            ? Instance.DrinkDB[id] : null;
    }

    private string socketURL =
        "ws://ec2-13-209-66-8.ap-northeast-2.compute.amazonaws.com:8080/socket.io/?EIO=4&transport=websocket";
    
    void Start()
    {
        // 시작 시 음료의 목록을 서버에서 받아온다.
        HandleGetDrinkList();
        HandleGetSlotList();
        
        // 시작 시 현재 매장의 상황.
        SocketIOComponent.Instance.url = socketURL;
        SocketIOComponent.Instance.On("open", HandleOpen);
        SocketIOComponent.Instance.On("error", HandleError);
        SocketIOComponent.Instance.On("close", HandleClose);
        SocketIOComponent.Instance.On("update", HandleSlotUpdate);
        
        // 1초마다 업데이트 처리를 하는 시뮬레이션을 호출합니다.
        StartCoroutine(StartSimulation());
    }
    #region HANDLE CODES
    public void HandleGetDrinkList()
    {
        DrinkDB.Clear();
        StartCoroutine(_drinkService.GETDrinkList(o => {
            Debug.Log("++++음료수가 업데이트 되었습니다.+++++");
            foreach (Drink d in o)
            {
                DrinkDB.Add(d.id, d);
                Debug.Log(string.Format("음료:{0}/{1}",
                    d.id, d.title));
            }
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
        }));
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
		
        SocketIOComponent.Instance.Emit("start_simulation");
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

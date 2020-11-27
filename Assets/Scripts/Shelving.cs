using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using Service;
using SocketIO;
using Unity.Profiling;
using UnityEngine;

public class Shelving : MonoBehaviour
{
 //   public List<Model.Slot> slotData;
    private DrinkService _drinkService = new DrinkService();
    private SlotService _slotService = new SlotService();
    private StallService _stallService = new StallService();    
    
    public int rowSize = 4;
    public int columnSize = 12;
    public int depthSize = 4;
    public List<List<List<Model.Slot>>> slotData;
    // Start is called before the first frame update
    


    private string socketURL =
        "ws://ec2-13-209-66-8.ap-northeast-2.compute.amazonaws.com:8080/socket.io/?EIO=4&transport=websocket";
    
    void Start()
    {
        //SocketIOComponent.Instance.url = socketURL;
        //SocketIOComponent.Instance.On("open", HandleOpen);
        //SocketIOComponent.Instance.On("error", HandleError);
        //SocketIOComponent.Instance.On("close", HandleClose);
        //SocketIOComponent.Instance.On("update", HandleSlotUpdate);
        // 1초마다 업데이트 처리를 하는 시뮬레이션을 호출합니다.
        //StartCoroutine("StartSimulation");
    }

    // 1초마다 재고 여부가 바뀌는 것을 알리는 코드를 수신합니다.
    public void HandleSlotUpdate(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Update received: " + e.name + " " + e.data);
        if (e.data == null) { return; }
        string rawString = e.data.ToString();
        SlotUpdate update = JsonUtility.FromJson<SlotUpdate>(rawString);
        Debug.Log(update.ToString());
        // 이벤트 핸들러
        //update.updated_slot_info.id
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

    [ContextMenu("Test Run 1")]
    public void TestRun1()
    {
        Debug.Log("HEELO WORLD!");
        StartCoroutine(_slotService.GETSlotList(o =>
        {
            initSlotData(rowSize, columnSize, depthSize);
            
            foreach (Slot slot in o)
            {
                if (0 <= slot.row && slot.row < rowSize &&
                    0 <= slot.column && slot.column < columnSize &&
                    0 <= slot.depth && slot.depth < depthSize)
                {
                 
                    slotData[slot.row][slot.column][slot.depth] = slot;   
                } else
                {
                    Debug.Log("OUT OF Index!");
                }
            }
            for (int r = 0; r < rowSize; r++)
            {
                for (int c = 0; c < columnSize; c++)
                {
                    for (int d = 0; d < depthSize; d++)
                    
                    {
                        Debug.Log(slotData[r][c][d].ToString());
                    }
                }

            }
        }));
        
       
    }

    private void initSlotData(int row, int column, int depth)
    {
        slotData = new List<List<List<Slot>>>();
        for (int r = 0; r < row; r++)
        {
            var rowData = new List<List<Slot>>();
            for (int c = 0; c < column; c++)
            {
                var columnData = new List<Slot>();
                for (int d = 0; d < depth; d++)
                {
                    columnData.Add(new Slot());
                }
                rowData.Add(columnData);
            }

            slotData.Add(rowData);
        }
        
        

    }

    [ContextMenu("Test Run 2")]
    public void TestRun2()
    {
        StartCoroutine(_drinkService.GETDrinkList(o => {
            foreach (Product d in o)
            {
                Debug.Log(d.title.ToString());
            }
        }));
        // Slot - Manager
        // 슬롯은 한번에 배치? 
        // 음료 종류
        // 
        // DrinkObject.cs
        //  + OnClickFunction()
        //      - UI Popup
        //  + OnHoverFunction()
        //      - UI Popup
        //  + OnItemAdded()
        //      - Interaction
        //  + OnItemRemoved()
        //      - Interaction
        //  + OnItem
    }

    // Update is called once per frame
    void Update()
    {

    }
}

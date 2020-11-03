using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using Service;
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

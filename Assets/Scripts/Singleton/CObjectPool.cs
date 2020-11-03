using System.Collections;
using System.Collections.Generic;
using DrinkDetailCanvas;
using UnityEngine;

public class CObjectPool : CSingletonMono<CObjectPool>
{
    readonly string collection_path = "ResourcesCollection";
    public PrefabCollection prefab_collection;
    DrinkObject drinkPrefab;
    Queue<DrinkObject> objectPool = new Queue<DrinkObject>();
    readonly int drinkPool = 192;
    private DrinkDetailCanvasControl _detailCanvasControl;
    void Awake()
    {
        // Drink Prefab
        if (drinkPrefab == null)
        {
            drinkPrefab = Resources.Load<DrinkObject>("DrinkPrefab");
         
        }
       
        if (_detailCanvasControl == null)
        {
            _detailCanvasControl = Instantiate(Resources.Load<DrinkDetailCanvasControl>("DrinkCanvas"));
            _detailCanvasControl.gameObject.SetActive(false); 
    
        }

        for (int i = 0; i < drinkPool; ++i)
        {
            var go = Instantiate(drinkPrefab).GetComponent<DrinkObject>();
            go.gameObject.SetActive(false);
            go.transform.SetParent(this.transform);
            objectPool.Enqueue(go);
        }

        // Texture Collection
        prefab_collection = Resources.Load<PrefabCollection>(collection_path);
    }

    public DrinkDetailCanvasControl CreateDetailCanvasControl(Model.Drink drink_data)
    {
        _detailCanvasControl.gameObject.SetActive(true);
        _detailCanvasControl.setDrink(drink_data);
        _detailCanvasControl.SetCanvasState();
        return _detailCanvasControl;
    }
    
    
    
    public DrinkObject CreateDrinkObject(Model.Drink drink_data)
    {
        var go = objectPool.Dequeue();
        go.Setup(drink_data);
        return go;
    }
    public DrinkObject DestroyDrinkObject(DrinkObject go)
    {
        go.gameObject.SetActive(false);
        go.transform.SetParent(this.transform);
        objectPool.Enqueue(go);
        return go;
    }
}

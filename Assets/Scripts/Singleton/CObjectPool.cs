using System.Collections;
using System.Collections.Generic;
using DrinkDetailCanvas;
using UnityEngine;

public class CObjectPool : CSingletonMono<CObjectPool>
{
    public ShelfObject main;
    DrinkObject drinkPrefab;
    Queue<DrinkObject> drinkObjPool = new Queue<DrinkObject>();
    readonly int drinkPool = 192;
    Dictionary<string, Texture> drink_textures
        = new Dictionary<string, Texture>();
    readonly string texture_prefix = "Textures/";
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
            drinkObjPool.Enqueue(go);
        }
    }
<<<<<<< HEAD
    public Texture GetDrinkTexture(string path)
    {
        // Debug
        //path = "cola_test1";
        //
        path = path.Replace(".png", "");
        Texture tex = Resources.Load<Texture>(texture_prefix + path);
        if (drink_textures.ContainsKey(path) == false)
            drink_textures.Add(path, tex);
        return drink_textures[path];
    }

=======

    public DrinkDetailCanvasControl CreateDetailCanvasControl(Model.Drink drink_data)
    {
        _detailCanvasControl.gameObject.SetActive(true);
        _detailCanvasControl.setDrink(drink_data);
        _detailCanvasControl.SetCanvasState();
        return _detailCanvasControl;
    }
    
    
    
>>>>>>> Drink 클릭 프리펩 작성
    public DrinkObject CreateDrinkObject(Model.Drink drink_data)
    {
        var go = drinkObjPool.Dequeue();
        go.Setup(drink_data);
        return go;
    }
    public DrinkObject DestroyDrinkObject(DrinkObject go)
    {
        go.gameObject.SetActive(false);
        go.transform.SetParent(this.transform);
        drinkObjPool.Enqueue(go);
        return go;
    }
}

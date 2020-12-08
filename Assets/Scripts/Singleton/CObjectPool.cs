using System.Collections;
using System.Collections.Generic;
using DrinkDetailCanvas;
using UnityEngine;
using Model.Define;

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

    public Dictionary<ProductType, Texture> textureCache = new Dictionary<ProductType, Texture>();
    public Dictionary<ProductType, MeshRenderer> fbxCache = new Dictionary<ProductType, MeshRenderer>();
    
    public void InitializeFBX()
    {
        if (fbxCache != null && fbxCache.Count > 0)
            Debug.LogWarning("[fbx 초기화가 되었습니다");
        fbxCache.Clear();
        fbxCache.Add(ProductType.CEREAL, Resources.Load<MeshRenderer>("FBX/cereal"));
        fbxCache.Add(ProductType.SANDWICH, Resources.Load<MeshRenderer>("FBX/sandwich"));
        fbxCache.Add(ProductType.DRINK, Resources.Load<MeshRenderer>("FBX/drink"));
        fbxCache.Add(ProductType.SNACK, Resources.Load<MeshRenderer>("FBX/snack"));
    }
    void AddFBXCache(ProductType type, string path){
        fbxCache.Add(type, Resources.Load<MeshRenderer>(path));
    }

    void Awake()
    {
        InitializeFBX();

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

    public DrinkDetailCanvasControl CreateDetailCanvasControl(Model.Product drink_data)
    {
        _detailCanvasControl.gameObject.SetActive(true);
        _detailCanvasControl.setDrink(drink_data);
        _detailCanvasControl.SetCanvasState();
        return _detailCanvasControl;
    }
    
    // public ProductObject CreateObject(Model.Product data){
    //     var go = data.type;
    //     return null;
    // }

    public DrinkObject SpawnDrinkObject(Model.Product drink_data)
    {
        // Drink Prefab
        if (drinkPrefab == null)
        {
            drinkPrefab = Resources.Load<DrinkObject>("DrinkPrefab");
        }
        var go = Instantiate(drinkPrefab).GetComponent<DrinkObject>();
        go.gameObject.SetActive(true);
        go.Setup(drink_data);
        return go;
    }
    
    public DrinkObject CreateDrinkObject(Model.Product drink_data)
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

using System.Collections;
using System.Collections.Generic;
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
    void Awake()
    {
        // Drink Prefab
        if (drinkPrefab == null)
            drinkPrefab = Resources.Load<DrinkObject>("DrinkPrefab");

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

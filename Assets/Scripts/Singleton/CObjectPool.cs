using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjectPool : CSingletonMono<CObjectPool>
{
    readonly string collection_path = "ResourcesCollection";
    public PrefabCollection prefab_collection;
    DrinkObject drinkPrefab;
    Queue<DrinkObject> objectPool = new Queue<DrinkObject>();
    readonly int drinkPool = 192;
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
            objectPool.Enqueue(go);
        }

        // Texture Collection
        prefab_collection = Resources.Load<PrefabCollection>(collection_path);
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

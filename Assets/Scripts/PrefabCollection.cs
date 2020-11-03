using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabData", menuName = "ScriptableObjects/ResourceCollection", order = 1)]
[Serializable]
public class PrefabCollection : ScriptableObject
{
    public List<Texture> textures_normal_cans;
    public List<Texture> textures_tall_cans;
    public List<Texture> textures_fat_cans;

    public DrinkObject prefab_normal_can;
    public DrinkObject prefab_fat_can;
    public DrinkObject prefab_tall_can;


}

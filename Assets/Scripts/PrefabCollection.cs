using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabData", menuName = "ScriptableObjects/Prefab Manager", order = 1)]
[Serializable]
public class PrefabCollection : ScriptableObject
{
    public List<GameObject> prefabs;

    
}

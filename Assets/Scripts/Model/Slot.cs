using System;
using UnityEngine;


namespace Model
{
    [Serializable]
    public class Slot
    {
        public int id;
        public int drink_id;
        public bool has_drink;
        public string incoming_time;
        public int row;
        public int column;
        public int depth;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
    
}
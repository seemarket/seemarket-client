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
        public float row;
        public float column;
        public float depth;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
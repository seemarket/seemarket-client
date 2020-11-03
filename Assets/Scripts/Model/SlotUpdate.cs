using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class SlotUpdate
    {
        public string update_type;
        public Slot before_slot_info;
        public Slot updated_slot_info;
        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
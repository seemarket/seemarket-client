using System;
using UnityEngine;

namespace Model
{

    public enum UpdateType
    {
        SOLD_OUT, ARRIVED, MOVE, CHANGE
    }
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
        
        public UpdateType GETUpdateType()
        {
            switch (update_type)
            {
                case "sold_out":
                    return UpdateType.SOLD_OUT;
                case "arrived":
                    return UpdateType.ARRIVED;
                case "move":
                    return UpdateType.MOVE;
                case "change":
                    return UpdateType.CHANGE;
            }
            return UpdateType.CHANGE;
        }
    }
}
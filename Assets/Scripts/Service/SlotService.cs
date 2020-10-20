using System;
using System.Collections;
using Model;

namespace Service
{
    public class SlotService
    {
        private NetworkService _networkService = new NetworkService();


        [Serializable]
        private class SlotListResponse
        {
            public Slot[] slot_list;
        }


        [Serializable]
        private class SlotResponse
        {
            public Slot slot;
        }

        public IEnumerator GETSlotList(Action<Slot[]> callback)
        {
            string endPoint = $"slot/";
            Action<SlotListResponse> responseCallback = o => { callback(o.slot_list); };

            return _networkService.Get<SlotListResponse>(endPoint, responseCallback);
        }


        public IEnumerator GETSlot(int id, Action<Slot> callback)
        {
            string endPoint = $"slot/{id}/";
            Action<SlotResponse> responseCallback = o => { callback(o.slot); };

            return _networkService.Get<SlotResponse>(endPoint, responseCallback);
        }
    }
}
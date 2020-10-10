using System;
using System.Collections;
using Model;
using UnityEngine;

namespace Service
{
    public class StallService
    {
        
        private NetworkService _networkService = new NetworkService();


        [Serializable]
        private class StallResponse
        {
            public Stall stall;
        }
        
        public IEnumerator GETStall(int id, Action<Stall> callback)
        {
            string endPoint = $"stall/{id}/";
            Action<StallResponse> responseCallback = o =>
            {
                callback(o.stall);
            };

            return _networkService.Get<StallResponse>(endPoint, responseCallback);
        }
    }
}
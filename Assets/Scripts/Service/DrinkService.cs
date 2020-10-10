using System;
using System.Collections;
using Model;

namespace Service
{
    public class DrinkService
    {
        private NetworkService _networkService = new NetworkService();


        [Serializable]
        private class DrinkListResponse
        {
            public Drink[] drink_list;
        }


        [Serializable]
        private class DrinkResponse
        {
            public Drink drink;
        }

        public IEnumerator GETDrinkList(Action<Drink[]> callback)
        {
            string endPoint = $"drink/";
            Action<DrinkListResponse> responseCallback = o => { callback(o.drink_list); };

            return _networkService.Get<DrinkListResponse>(endPoint, responseCallback);
        }


        public IEnumerator GETDrink(int id, Action<Drink> callback)
        {
            string endPoint = $"drink/{id}/";
            Action<DrinkResponse> responseCallback = o => { callback(o.drink); };

            return _networkService.Get<DrinkResponse>(endPoint, responseCallback);
        }
    }
}
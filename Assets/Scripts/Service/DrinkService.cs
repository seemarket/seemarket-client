using System;
using System.Collections;
using Model;

namespace Service
{
    public class DrinkService
    {
        private NetworkService _networkService = new NetworkService();


        [Serializable]
        private class ProductListResponse
        {
            public Product[] product_list;
        }


        [Serializable]
        private class DrinkResponse
        {
            public Product drink;
        }

        public IEnumerator GETDrinkList(Action<Product[]> callback)
        {
            string endPoint = $"drink/";
            Action<ProductListResponse> responseCallback = o => { callback(o.product_list); };

            return _networkService.Get<ProductListResponse>(endPoint, responseCallback);
        }


        public IEnumerator GETDrink(int id, Action<Product> callback)
        {
            string endPoint = $"drink/{id}/";
            Action<DrinkResponse> responseCallback = o => { callback(o.drink); };

            return _networkService.Get<DrinkResponse>(endPoint, responseCallback);
        }
    }
}
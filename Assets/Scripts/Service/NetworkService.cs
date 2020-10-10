using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Model;
using UnityEngine;
using UnityEngine.Networking;


namespace Service
{
    public class NetworkService
    {
        
        private string baseURL = "http://3.34.44.222/api/";
        private string getEndPoint(string endPoint, KeyValuePair<string, string> parameter)
        {
            Uri uri = new Uri(baseURL + endPoint);
            UriBuilder uriBuilder = new UriBuilder(uri);
            NameValueCollection queryString = new NameValueCollection();
            uriBuilder.Query = queryString.ToString();
            return uriBuilder.Uri.ToString();
        }

        public IEnumerator Get<T>(string endPoint, Action<T> callback)
        {
            return Get<T>(endPoint, new KeyValuePair<string, string>(), callback);
        }

    
        public IEnumerator Get<T>(string endPoint, KeyValuePair<string, string> parameter, Action<T> callback)
        {
            string url = getEndPoint(endPoint, parameter);
            UnityWebRequest request = new UnityWebRequest();
            using (request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();
    
                if (request.isNetworkError)
                {
                    Debug.Log(request.error);
                }
    
                Debug.Log(request.downloadHandler.text);
                var data = request.downloadHandler.text;
    
                Response<T> value = (Response<T>) JsonUtility.FromJson(data, typeof(Response<T>));
                if (value.code != 200)
                {
                    Debug.Log("NOT Found ERROR");
    
                }
                else
                {
                 
                    callback(value.data);   
                }
            }
        }
    }
}
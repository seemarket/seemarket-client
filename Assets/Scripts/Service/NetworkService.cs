using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Model;
using UnityEngine;
using UnityEngine.Networking;


namespace Service
{
    public class NetworkService
    {
        
        private string baseURL = "http://3.34.44.222/api/";
        private string ToQueryString(NameValueCollection nvc)
        {
            if (nvc == null) return string.Empty;
            StringBuilder sb = new StringBuilder();

            foreach (string key in nvc.Keys)
            {
                if (string.IsNullOrWhiteSpace(key)) continue;

                string[] values = nvc.GetValues(key);
                if (values == null) continue;

                foreach (string value in values)
                {
                    sb.Append(sb.Length == 0 ? "?" : "&");
                    sb.AppendFormat("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));
                }
            }

            return sb.ToString();
        }
        private string getEndPoint(string endPoint, NameValueCollection parameter)
        {
            Uri uri = new Uri(baseURL + endPoint);
            UriBuilder uriBuilder = new UriBuilder(uri);
            
            uriBuilder.Query = ToQueryString(parameter);
            return uriBuilder.Uri.ToString();
        }

        public IEnumerator Get<T>(string endPoint, Action<T> callback)
        {
            return Get<T>(endPoint, new NameValueCollection(), callback);
        }

    
        public IEnumerator Get<T>(string endPoint, NameValueCollection parameter, Action<T> callback)
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
    
                Debug.Log(url + " : " + request.downloadHandler.text);
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
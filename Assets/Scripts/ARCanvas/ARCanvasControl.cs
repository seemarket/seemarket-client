using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ARCanvas
{
    public class ARCanvasControl : UICanvasBase
    {

        public Button stallButton;
        public Button prefab; // This is our prefab object that will be exposed in the inspector
        
        private int numberToCreate = 5; // number of objects to create. Exposed in inspector

        public GameObject gridObject;
        
        
        
        void Start()
        {
            SetCanvasState();
            StartCoroutine(Populate());
        }

        void SetCanvasState()
        {
            stallButton.onClick.AddListener(goToStall);
        }

        /// <summary>
        /// 매대쪽으로 돌아간다.
        /// </summary>
        private void goToStall()
        {
            CCanvasManager.Instance.currentMainState = CCanvasManager.MainState.Stall;
            SceneManager.LoadScene("kjtest2");
        }
        
        IEnumerator Populate()
        {
            if (CLocalDatabase.Instance.didFetchDrink == false)
            {
                yield return new WaitForSeconds(1);
                StartCoroutine(Populate());
            }
            else
            {
                foreach (Model.Product p in CLocalDatabase.Instance.ProductDB.Values)
                {
                    Button newObj = Instantiate(prefab, gridObject.transform);
                    newObj.onClick.AddListener(() =>
                    {
                        setSpawner(p);
                    });
                    StartCoroutine(DownloadImage(p.thumbnail_url, newObj));
                }
            }
        }
        
        private void setSpawner(Model.Product p)
        {
            
        }
        
        IEnumerator DownloadImage(string MediaUrl, Button clickableButton)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
            {
                Texture texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
                Texture2D texture2D = Texture2D.CreateExternalTexture(
                    texture.width,
                    texture.height,
                    TextureFormat.RGB24,
                    false, false,
                    texture.GetNativeTexturePtr());
                Rect rect = new Rect(0, 0, texture2D.width, texture2D.height);

                clickableButton.image.sprite = Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f));

            }
        }
            
    }
}
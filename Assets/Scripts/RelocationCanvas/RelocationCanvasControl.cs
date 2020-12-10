using System;
using Model;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.RelocationCanvas
{
    public class RelocationCanvasControl: UICanvasBase
    {

        public Vector3 initialSpawn;
      
        
        public Button selectItemButton; // This is our prefab object that will be exposed in the inspector
        public Button resetButton;
        
        public GameObject gridObject;

        public int spawnCount = 0;
        /**
         * 현재 선택된 모델.
         */
        public Model.Product currentSelectedProduct;
        
        void Start()
        {
            resetButton.onClick.AddListener(reload);    
        }

        public void initialize()
        {
            StartCoroutine(Populate());
        }
        
        /**
         * 다시 불러오기
         */
        public void reload()
        {
            CLocalDatabase.Instance.ResetSimulation();
        }
        
        IEnumerator Populate()
        {
            foreach (Transform child in gridObject.transform) {
                GameObject.Destroy(child.gameObject);
            }
            if (CLocalDatabase.Instance.didFetchDrink == false)
            {
                yield return new WaitForSeconds(1);
                StartCoroutine(Populate());
            }
            else
            {
                foreach (Model.Product p in CLocalDatabase.Instance.ProductDB.Values)
                {
                    Button newObj = Instantiate(selectItemButton, gridObject.transform);
                    newObj.onClick.AddListener(() =>
                    {
                        setSpawner(p);
                    });
                    StartCoroutine(DownloadImage(p.thumbnail_url, newObj));
                }

                if (CLocalDatabase.Instance.ProductDB.Count > 0)
                {
                    setSpawner(CLocalDatabase.Instance.ProductDB.Values.First());
                }
                
            }
        }
        
        private void setSpawner(Model.Product p)
        {
            currentSelectedProduct = p;
            CreateCommand createCommand = new CreateCommand();
            createCommand.product_id = p.id;
            createCommand.row = initialSpawn.x;
            createCommand.depth = initialSpawn.z + spawnCount * 0.1;
            spawnCount++;
            createCommand.column = initialSpawn.y;
            CLocalDatabase.Instance.CreateProduct(createCommand);
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

        /**
         * TODO: 새로운 상품 추가
         */
        public void addNewProduct()
        {
            CreateCommand createCommand = new CreateCommand();
            createCommand.product_id = 1;
            createCommand.row = 1.45;
            createCommand.depth = 1.5677;
            createCommand.column = 1.6677;
//            CreateProduct(createCommand);
        }
    }
}
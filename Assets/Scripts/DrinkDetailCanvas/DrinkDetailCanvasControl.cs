using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace DrinkDetailCanvas
{

    public class DrinkDetailCanvasControl : UICanvasBase
    {

        /// <summary>디테일 페이지에서 정보를 보여주는 컴포넌트</summary>
        public Text titleText;

        public Image thumbnailImage;
        public Text rateText;
        public Text priceText;
        public Text incomeText;
        public Text descriptionText;
        public Text etcText;
        public Button backButton;

        private Model.Drink _drink;

        void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.SetResolution(1440, 2960, true);
			CCanvasManager.Instance.SetMain(this);
        }


        void Start()
        {
            SetCanvasState();
        }

        public void setDrink(Model.Drink drink)
        {
            this._drink = drink;
        }

        public void SetCanvasState()
        {
            if (_drink == null)
            {
                return;
            }

            titleText.text = _drink.title;
            rateText.text = "5점"; // TODO
            priceText.text = _drink.price;
            incomeText.text = "5분 후 입고"; // TODO
            descriptionText.text = _drink.description;
            StartCoroutine(DownloadImage(_drink.thumbnail_url));
            backButton.onClick.AddListener(Close);
        }

        public void Close()
        {
            Debug.Log("Button Click");
            this.gameObject.SetActive(false);
            this._drink = null;
        }

        IEnumerator DownloadImage(string MediaUrl)
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

                thumbnailImage.sprite = Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f));

            }
        }
        public override void OnBackKey()
        {
            base.OnBackKey();
            this.Close();
        }
    }
    


}
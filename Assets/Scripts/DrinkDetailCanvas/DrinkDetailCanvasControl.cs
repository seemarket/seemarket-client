using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace DrinkDetailCanvas
{
 
    public class DrinkDetailCanvasControl : MonoBehaviour
{

    /// <summary>디테일 페이지에서 정보를 보여주는 컴포넌트</summary>
    public Text titleText;

    public Image thumbnailImage;
    public Text rateText;
    public Text priceText;
    public Text incomeText;
    public Text descriptionText;
    public Text etcText;
    
    
    private Model.Drink _drink;
    void Awake(){
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1440,2960, true);
    }
    
    
    void Start(){
        SetCanvasState();
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

    }
    
    IEnumerator DownloadImage(string MediaUrl)
    {   
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError) 
            Debug.Log(request.error);
        else
        {
            
        }
            //thumbnailImage.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
    } 

    
}

}
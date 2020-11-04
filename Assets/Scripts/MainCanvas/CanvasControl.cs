using StallCanvas;
using UnityEngine;
using UnityEngine.UI;

namespace test
{
    public class CanvasControl : UICanvasBase
    {
        public enum CanvasStates{
            numInitial = 0,
            numAuthorize = 1,
            numMain = 2,
            numOwnerPanel = 3,
            numStatusPanel = 4,
            // 매대를 보여주는 화
            numStatusShowStall = 5,
        }
        private CanvasStates LastCanvas = CanvasStates.numStatusPanel;
        private CanvasStates currentCanvas = 0;
        
        
    
        public Text AlertText;
        public GameObject Initial;
        public Text init_checkText; //첫 화면에서 에러들 확인하는 단계

        public GameObject Authorize;
        public GameObject Main;
        public GameObject OwnerPanel;
        public GameObject StatusPanel;
        public GameObject AlertBox;
        public GameObject backgroundPanel;

        public StallCanvasControl stallCanvasControl;

        private const string notImplemented = "아직 구현되지 않았습니다.";
        private const string notInMainPanel = "현재 Main 페이지가 아닙니다.";
        private const string notInOwnerPanel = "현재 Owner 페이지가 아닙니다.";

        
        
        void Awake(){
            AlertText.enabled = false;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.SetResolution(1440,2960, true);
        }

        void Start(){
            CCanvasManager.Instance.SetMain(this);
            currentCanvas = CanvasStates.numInitial;
            SetCanvasState();
            checkError();
            init_checkText.text = "실행전 에러 체크";
        }

        private void checkError(){
            //에러들 확인하고 표시해주는 함수. 데모 단계에서는 구현하지 않았음.
        }


        public void enableAlert(){
            //AlertBox.SetActive(true);
        }

        public void deadAlert(){
            //AlertBox.SetActive(false);
        }

        public void accessNotImplemented(){
            alertMessage(notImplemented);
        }

        public void alertMessage(string s){
            enableAlert();
            AlertText.text = s;
            Invoke("deadAlert", 4);
        }

        public void goNext(){
            if(currentCanvas != LastCanvas){
                currentCanvas++;
                SetCanvasState();
            }
        }

        public void goUser(){
            if(currentCanvas != CanvasStates.numMain){
                alertMessage(notInMainPanel);
            }
            else{
                accessNotImplemented();
            }
        }

        public void goOwner(){
            if(currentCanvas != CanvasStates.numMain){
                alertMessage(notInMainPanel);
            }
            else{
                currentCanvas = CanvasStates.numOwnerPanel;
                SetCanvasState();
            }
        }

        public void goStatus(){
            currentCanvas = CanvasStates.numStatusShowStall;
            SetCanvasState();
        }

        public void goRelocation(){
            if(currentCanvas != CanvasStates.numOwnerPanel){
                alertMessage(notInOwnerPanel);
            }
            else{
                accessNotImplemented();
            }
        }

        /// <summary>
        /// 슬롯의 화면으로 간다.
        /// </summary>
        public void goToStall()
        {
            currentCanvas = CanvasStates.numStatusShowStall;
            SetCanvasState();
        }

        public void switchOU(){
            accessNotImplemented();
        }

        public void SetCanvasState(){
            switch (currentCanvas){
                case CanvasStates.numInitial : 
                    Initial.SetActive(true);
                    Authorize.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    break;
                case CanvasStates.numAuthorize :
                    Initial.SetActive(false);
                    Authorize.SetActive(true);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    break;
                case CanvasStates.numMain :
                    Initial.SetActive(false);
                    Authorize.SetActive(false);
                    Main.SetActive(true);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    break;
                case CanvasStates.numOwnerPanel :
                    Initial.SetActive(false);
                    Authorize.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(true);
                    StatusPanel.SetActive(false);
                    break;
                case CanvasStates.numStatusPanel :
                    Initial.SetActive(false);
                    Authorize.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(true);
                    break;
                case CanvasStates.numStatusShowStall:
                    Debug.Log("Status");
                    backgroundPanel.SetActive(false);
                    Initial.SetActive(false);
                    Authorize.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    stallCanvasControl.gameObject.SetActive(true);
                    break;
                default :
                    Debug.Log("default case for SetCanvasSate!");
                    break;
            }
        }

        public override void OnBackKey()
        {
            base.OnBackKey();
            goBack();
        }
        public void goBack(){
            switch(currentCanvas){
                case CanvasStates.numStatusPanel :
                    currentCanvas = CanvasStates.numOwnerPanel;
                    SetCanvasState();
                    break;
                default :
                    Debug.Log("이 페이지에는 뒤로가기 없어야 함!");
                    break;
            }
        }
    }
}

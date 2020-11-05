using StallCanvas;
using UnityEngine;
using UnityEngine.UI;

namespace test
{
    public class CanvasControl : UICanvasBase
    {
        public enum CanvasStates{
            numInitial = 0,
            numMain = 1,
            numOwnerPanel = 2,
            numStatusPanel = 3,
            // 매대를 보여주는 화
            numStatusShowStall = 4,
        }
        private CanvasStates LastCanvas = CanvasStates.numStatusPanel;
        private CanvasStates currentCanvas = 0;
        
        
    
        public Text AlertText;
        public GameObject Initial;
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
       
            stallCanvasControl.parent = this;
            if (CCanvasManager.Instance.currentMainState == CCanvasManager.MainState.Stall)
            {
                this.currentCanvas = CanvasStates.numStatusShowStall;
                SetCanvasState();
            }
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


        /// <summary>
        /// 뒤로가기 버튼을 눌러서 돌아갔을 때 처리
        /// </summary>
        public void GoToOwner()
        {
            currentCanvas = CanvasStates.numOwnerPanel;
            SetCanvasState();
        }
        public void SetCanvasState(){
            switch (currentCanvas){
                case CanvasStates.numInitial :
                    backgroundPanel.SetActive(true);
                    Initial.SetActive(true);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    stallCanvasControl.gameObject.SetActive(false);
                    break;
                case CanvasStates.numMain :
                    backgroundPanel.SetActive(true);
                    Initial.SetActive(false);
                    Main.SetActive(true);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    stallCanvasControl.gameObject.SetActive(false);
                    break;
                case CanvasStates.numOwnerPanel :
                    backgroundPanel.SetActive(true);
                    Initial.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(true);
                    StatusPanel.SetActive(false);
                    stallCanvasControl.gameObject.SetActive(false);
                    break;
                case CanvasStates.numStatusPanel :
                    backgroundPanel.SetActive(true);
                    Initial.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(true);
                    stallCanvasControl.gameObject.SetActive(false);
                    break;
                case CanvasStates.numStatusShowStall:
                    backgroundPanel.SetActive(false);
                    Initial.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    stallCanvasControl.gameObject.SetActive(true);
                    CObjectPool.Instance.main.forceInitialize();
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

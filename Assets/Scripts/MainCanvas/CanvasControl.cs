using StallCanvas;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace test
{
    public class CanvasControl : UICanvasBase
    {
        public enum CanvasStates{
            numInitial = 0, //시작 화면
            numMain = 1, //사용자, 점주 선택 화면
            numOwnerPanel = 2, //오너 시작 화면
            numStatusPanel = 3, //매장 현황 화면. 매대 디지털 트윈
            // 매대를 보여주는 화
            numStatusShowStall = 4, //
            numRelocationPanel = 5, //매대 재배치 화면
            numUserPanel = 6, //매대 재배치 화면
            numObjectScanPanel = 7, //물품 스캔 화면
        }
        private CanvasStates LastCanvas = CanvasStates.numRelocationPanel;
        private CanvasStates currentCanvas = 0;

        public Text AlertText;
        public GameObject Initial;
        public GameObject Main;
        public GameObject OwnerPanel;
        public GameObject StatusPanel;
        public GameObject AlertBox;
        public GameObject BackGroundPanel;
        public GameObject UserPanel;
        public GameObject RelocationPanel;
        //public GameObject ObjectScanPanel;


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
       
            stallCanvasControl.parent = this;
            if (CCanvasManager.Instance.currentMainState == CCanvasManager.MainState.Stall)
            {
                this.currentCanvas = CanvasStates.numStatusShowStall;
                SetCanvasState();
            }
        }

        public void moveToAR()
        {
            SceneManager.LoadScene("ObjectManipulation");
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

        //판넬 전환 버튼 통합 컨트롤 함수
        //에러 처리는 따로 해주지 않기로 했음.
        public void panelChange(CanvasStates selectedPanel){
            currentCanvas = (CanvasStates) selectedPanel;
            SetCanvasState();
        }

        public void panelChangeButton(int data) {
            this.panelChange((CanvasStates) data);
        }

        public void goOwner(){
            this.panelChange(CanvasStates.numOwnerPanel);
        }

        public void SetCanvasState(){
            switch (currentCanvas){
                case CanvasStates.numInitial :
                    BackGroundPanel.SetActive(true);
                    Initial.SetActive(true);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    UserPanel.SetActive(false);
                    stallCanvasControl.gameObject.SetActive(false);
                    break;
                case CanvasStates.numMain :
                    BackGroundPanel.SetActive(true);
                    Initial.SetActive(false);
                    Main.SetActive(true);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    UserPanel.SetActive(false);
                    stallCanvasControl.gameObject.SetActive(false);
                    break;
                case CanvasStates.numOwnerPanel :
                    BackGroundPanel.SetActive(true);
                    Initial.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(true);
                    StatusPanel.SetActive(false);
                    UserPanel.SetActive(false);
                    RelocationPanel.SetActive(false);
                    stallCanvasControl.gameObject.SetActive(false);
                    break;
                case CanvasStates.numStatusPanel :
                    BackGroundPanel.SetActive(false);
                    Initial.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(true);
                    UserPanel.SetActive(false);
                    stallCanvasControl.gameObject.SetActive(false);
                    CObjectPool.Instance.main.forceInitialize(ShelfMode.VIEW);
                    break;
                case CanvasStates.numStatusShowStall:
                    BackGroundPanel.SetActive(false);
                    Initial.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    UserPanel.SetActive(false);
                    stallCanvasControl.gameObject.SetActive(true);
                    RelocationPanel.SetActive(false);
                    CObjectPool.Instance.main.forceInitialize(ShelfMode.VIEW);
                    break;
                case CanvasStates.numRelocationPanel:
                    BackGroundPanel.SetActive(false);
                    Initial.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    RelocationPanel.SetActive(true);
                    UserPanel.SetActive(false);
                    CObjectPool.Instance.main.forceInitialize(ShelfMode.EDIT);
                    break;
                case CanvasStates.numUserPanel:
                    BackGroundPanel.SetActive(true);
                    Initial.SetActive(false);
                    Main.SetActive(false);
                    OwnerPanel.SetActive(false);
                    StatusPanel.SetActive(false);
                    RelocationPanel.SetActive(false);
                    UserPanel.SetActive(true);
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
                case CanvasStates.numRelocationPanel :
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

using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ARCanvas
{
    public class ARCanvasControl : UICanvasBase
    {

        public Button stallButton;
        
        void Start()
        {
            SetCanvasState();
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
        
            
    }
}
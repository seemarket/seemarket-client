using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace DefaultNamespace.ScanCanvas
{
    public class ScanCanvasControl : UICanvasBase

    {


        public Button backButton;

        void Start()
        {
            CCanvasManager.Instance.currentMainState = CCanvasManager.MainState.User;
            backButton.onClick.AddListener(() => { SceneManager.LoadScene("kjtest2"); });
        }
    }
}
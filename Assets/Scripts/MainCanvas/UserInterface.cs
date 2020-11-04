using UnityEngine;

namespace test
{
    public class UserInterface : MonoBehaviour
    {
        public enum CanvasStates{
            Initial,
            Authorize,
            Main
        }

        [Header("Canvas")]
        //[ToolTip("Place the Initial canvas here")]
        public GameObject Initial;
 
        //[ToolTip("Place the Authorize canvas here")]
        public GameObject Authorize;
 
        //[ToolTip("Place the Main canvas here")]
        public GameObject Main;
 
 
        // This method will remain active one cavas at a time just pass the state like
        /*Example
     public void Example(){
         SetCanvasState (CanvasStates.PopUp);
     }
     */
        public void SetCanvasState(CanvasStates state){
            Initial.SetActive (state == CanvasStates.Initial);
            Authorize.SetActive (state == CanvasStates.Authorize);
            Main.SetActive (state == CanvasStates.Main);
        }
    }
}

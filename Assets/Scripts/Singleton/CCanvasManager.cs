using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUICanvas
{
	void OnBackKey();
    void OnOpen();
    void OnClose();
}

public class CCanvasManager : CSingletonMono<CCanvasManager>
{
    List<UICanvasBase> canvas_list;
    public UICanvasBase main_focus;
    Stack<UICanvasBase> canvas_stack = new Stack<UICanvasBase>();

    void Awake()
    {
		canvas_list = new List<UICanvasBase>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
	public void SetMain(UICanvasBase canvas)
    {
        main_focus = canvas;
    }

    void Update()
    {
     	if (Application.platform == RuntimePlatform.Android || 
         	Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log("AAA");
            if (Input.GetKey(KeyCode.Escape)){    
        Debug.Log("BB");	
            
            	if (main_focus != null){
        Debug.Log("CC");

	        		main_focus.OnBackKey();
                }
            }
        }
    }
}

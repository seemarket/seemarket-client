using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshCollider))]
public class DrinkObject : MonoBehaviour
{
    readonly float offset_height = 0.0784f;
    readonly float offset_rotation = 200f;

    public Model.Drink data;
    Renderer m_Renderer;

    #region TEST CODE
    public Texture _setter;
    [ContextMenu("SET TEST")]
    void SetTexture() { SetTexture(_setter); }
    #endregion

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }
    public void SetTexture(Texture tex)
    {
        m_Renderer.material.SetTexture("_MainTex", _setter);
    }

    ///<summary>
    // 오브젝트 세팅 시, Drink로 이닛을 생성해야한다.
    ///</summary>
    public void Setup(Model.Drink drink_data)
    {
        this.data = drink_data;
    }

    #region Unity interactions

    #endregion

    ///<summary>
    // [유니티 기능] 마우스 클릭시 오픈되는 페이지
    ///</summary>
    void OnMouseUp() { OnClickFunction(); }
    //void OnMouseOver() { OnHoverFunction(); }
    void OnClickFunction()
    {
        CObjectPool.Instance.CreateDetailCanvasControl(this.data);
        Debug.Log("Mouse click up");
    }
    void OnHoverFunction()
    {
        Debug.Log("Mouse is hovering");
    }

    public void DestoryObject()
    {
        CObjectPool.Instance.DestroyDrinkObject(this);
    }
}

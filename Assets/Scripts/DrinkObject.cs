using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

[RequireComponent(typeof(MeshRenderer), typeof(Collider))]
public class DrinkObject : MonoBehaviour
{
    public List<GameObject> interactions;

    readonly float offset_height = 0.0784f;
    readonly float offset_rotation = 200f;
//1.7031
//1.2655
//0.8296
//-0.44 , 0.391
    public Product data;
    public Renderer m_Renderer;

    void Awake()
    {
        m_Renderer = this.GetComponent<Renderer>();
    }
    public void SetFBX()
    {
        //m_Renderer = 
    }
    public void SetTexture(Texture tex)
    {
        m_Renderer.material.SetTexture("_MainTex", tex);
    }

    ///<summary>
    // 오브젝트 세팅 시, Product로 이닛을 생성해야한다.
    ///</summary>
    public void Setup(Model.Product drink_data)
    {
        this.data = drink_data;
        this.SetTexture(CObjectPool.Instance.GetDrinkTexture(drink_data.prefab_url));
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
        Debug.Log(m_Renderer.bounds.size.y);
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

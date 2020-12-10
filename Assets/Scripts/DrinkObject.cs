using System;
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
    public MeshFilter m_MeshFilter;

    public Mesh snackMesh;
    public Mesh cerealMesh;
    public Mesh sandwichMesh;
    public Mesh drinkMesh;

    public Boolean isClickable = true;
    void Awake()
    {
        m_Renderer = this.GetComponent<Renderer>(); 
        m_MeshFilter = GetComponent<MeshFilter>();
    }
    public void SetFBX()
    {
        //m_Renderer = 
    }
    public void SetTexture(Texture tex)
    {
        m_Renderer.material.SetTexture("_MainTex", tex);
    }

    public void SetMesh(Mesh mesh)
    {
        m_MeshFilter.mesh = mesh;
    }

    ///<summary>
    // 오브젝트 세팅 시, Product로 이닛을 생성해야한다.
    ///</summary>
    public void Setup(Model.Product drink_data)
    {
        this.data = drink_data;
        switch (drink_data.GETProductType())
        {
            case ProductType.CEREAL:
                SetMesh(cerealMesh);
                break;
            case ProductType.DRINK:
                SetMesh(drinkMesh);
                break;
            case ProductType.SANDWICH:
                SetMesh(sandwichMesh);
                break;
            case ProductType.SNACK:
                SetMesh(snackMesh);
                break;
        }
        this.SetTexture(CObjectPool.Instance.GetDrinkTexture(drink_data.prefab_url));

    }

    #region Unity interactions

    #endregion

    ///<summary>
    // [유니티 기능] 마우스 클릭시 오픈되는 페이지
    ///</summary>
    void OnMouseUp()
    {
        if (isClickable)
        {
            OnClickFunction();
        }
    }
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

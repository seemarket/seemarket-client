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

    public Boolean isClickable = false;
    public Boolean isDragable = false;

    public Model.Slot slot_data;

    public Vector3 previousLocation;
    public Vector3 finalLocation;


    public int frame = 0;
    public IEnumerator Diasppear()
    {
        Vector3 beforePosition = transform.position;
        Vector3 targetPosition = transform.position + Vector3.forward * 3f;
        frame++;
        if (frame < 100)
        {
            float time = ((float) frame) / 100f;
            this.transform.position = Vector3.Lerp(beforePosition, targetPosition, 1f / time);
            yield return new WaitForSeconds(0.1f);
        }
        
        DestoryObject();
    }

    public IEnumerator Appear()
    {
        Vector3 beforePosition = transform.position + Vector3.forward * 3f;
        Vector3 targetPosition = transform.position;
        frame++;
        if (frame < 100)
        {
            float time = ((float) frame) / 100f; 
            this.transform.position = Vector3.Lerp(beforePosition, targetPosition, 1f / time);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator Move(Vector3 targetPosition)
    {
        Vector3 beforePosition = transform.position;
       
        frame++;
        if (frame < 100)
        {
            float time = ((float) frame) / 100f; 
   
            this.transform.position = Vector3.Lerp(beforePosition, targetPosition, 1f / time);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    
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

    public void SetSlotData(Model.Slot slot_data)
    {
        this.slot_data = slot_data;
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


    private void OnMouseDown()
    {
        this.previousLocation = this.transform.position;
    }

    /**
     * 움직인 결과를 서버에 전송한다. 
     */
    private void moveProduct()
    {
        MoveCommand moveCommand = new MoveCommand();
        moveCommand.slot_id = this.slot_data.id;
        moveCommand.row = this.transform.position.x;
        moveCommand.depth = this.transform.position.z;
        moveCommand.column = this.transform.position.y;
        
        CLocalDatabase.Instance.MoveProduct(moveCommand);

    }

    ///<summary>
    // [유니티 기능] 마우스 클릭시 오픈되는 페이지
    ///</summary>
    void OnMouseUp()
    {
        if (isDragable)
        {
            moveProduct();
        }
        
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
    void OnMouseDrag()
    {
        if (isDragable)
        {
            Vector3 mousePosition 
                = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z);
            //마우스 좌표를 스크린 투 월드로 바꾸고 이 객체의 위치로 설정해 준다.
            Vector3 point = Camera.main.ScreenToWorldPoint(mousePosition); // this.transform.position = new Vector3(point.x, point.y, this.transform.position.z);

            this.transform.position = point;
            this.finalLocation = this.transform.position;
        }
    }
    public void DestoryObject()
    {
        CObjectPool.Instance.DestroyDrinkObject(this);
    }
}

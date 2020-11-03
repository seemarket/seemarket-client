using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 모드에 따른 카메라의 디폴트 위치
/// 카메라와 관련된 유틸 기능들을 추가할 예정
///</summary>
public class CCameraManager : CSingletonMono<CCameraManager>
{
    public enum eMode
    {
        FREE_ASPECT = -1,
        TOP_VIEW, 
        ITEM_ZOOM,
        AR_MODE,
    }

    Dictionary<eMode, Vector3> dic_position = new Dictionary<eMode, Vector3>();

    void Awake()
    {
        dic_position.Add(eMode.TOP_VIEW, 
            new Vector3(0f, 2.37f, -1.83f));
        dic_position.Add(eMode.ITEM_ZOOM, 
            new Vector3(0, 2.37f, -1.83f));
        dic_position.Add(eMode.FREE_ASPECT, 
            new Vector3(0, 2.37f, -1.83f));
        //Camera.main.po
    }
}

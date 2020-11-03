using UnityEngine;

public interface ISingleton
{
}

public class CSingleton<T> : ISingleton where T : new()
{
    private static T _instance;
    public CSingleton() {}

    /// <summary>
    /// 프리팹이나 아이템 오브젝트를 참조해야하는, 즉 오브젝트화 해야하는 싱글톤인 경우 hierarchy에 미리 생성해두는 것이 편하다.
    /// use class CSingletonMono instead. MonoBehaviour.AddComponent( typeof( T ) 가 호출되면 new T(); 역시 같이 호출된다.
    /// ㅡ 경준.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }
    public bool Initialize() { return true; }
    public void Clear(){ }
}

/// <summary>
/// 모노의 경우 Instance를 Awake문 전에 부를 수 없다.
/// </summary>
public class CSingletonMono<T> : MonoBehaviour, ISingleton where T : CSingletonMono<T>
{
    new public static string name { get { return typeof( T ).ToString(); } }
    private static T _instance;
	//private static bool isCreated = false;
    public CSingletonMono() { }

    public static T Instance
    {
        get
        {
            if ( ReferenceEquals(_instance, null))
            {
                _instance = Object.FindObjectOfType( typeof( T ) ) as T;
				if (_instance == null)
				{
					GameObject obj = new GameObject(name);
					_instance = obj.AddComponent(typeof(T)) as T;
				}
				Object.DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    public static void Clear()
    {
        GameObject.Destroy( Instance.gameObject );
        _instance = null;
    }
    //public void Awake() { _instance = Instance; }
}
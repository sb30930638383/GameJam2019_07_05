/// <summary>
/// Generic Mono singleton.
/// </summary>
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>{
	
	private static T m_Instance = null;
    
	public static T instance{
        get{
			if( m_Instance == null )
            {
                var type = typeof(T);
            //先查一下场景中 有没有当前类对象

     //方式一：如果多于一个 就销毁到只剩下一个。
   //  但是会报个不影响运行的错误 看着难受可以换下头那个
            //  var array = GameObject.FindObjectsOfType(typeof(T));
            //if(array!=null&&array.Length>0)
            //{
            //   m_Instance = array[0]as T;
            //   for (int i = 1; i < array.Length; i++)
			//{
            //   Destroy(array[i]);
			//}
            //   
            //}


         //方式2 如果有两个删一个  这个不会报错  与上头的2选一
            m_Instance = GameObject.FindObjectOfType<T>();  
                if( m_Instance == null )
                {
                    m_Instance = new GameObject("Singleton of " + type.Name, type).GetComponent<T>();
					 m_Instance.Init();
                }
               
            }
            return m_Instance;
        }
    }

    private void Awake(){
   
        if( m_Instance == null ){
            m_Instance = this as T;
        }
    }
 
    public virtual void Init(){}
 

    private void OnApplicationQuit(){
        m_Instance = null;
    }
}
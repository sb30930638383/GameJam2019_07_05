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
            //�Ȳ�һ�³����� ��û�е�ǰ�����

     //��ʽһ���������һ�� �����ٵ�ֻʣ��һ����
   //  ���ǻᱨ����Ӱ�����еĴ��� �������ܿ��Ի���ͷ�Ǹ�
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


         //��ʽ2 ���������ɾһ��  ������ᱨ��  ����ͷ��2ѡһ
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
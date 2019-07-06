using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoSingleton<GameObjectPool>{

    /// <summary>
    /// 已经缓存的对象容器
    /// </summary>
    private Dictionary<string, List<GameObject>> cache = new Dictionary<string, List<GameObject>>();//对象容器 字典<string,List<GameObject>>()

    /// <summary>
    /// 预制体容器
    /// </summary>
    private Dictionary<string,GameObject> prefabMap=new Dictionary<string,GameObject>();//预制体容器
    
    
    /// <summary>
    /// 将原型对象注册到对象池
    /// </summary>
    public void Register(string key,GameObject prefab,int preloadAmount=0 )
    {
        if (prefabMap.ContainsKey(key))       //查找 预制体容器有没有这个 Key 如果有               
            throw new System.ArgumentException(key + "已经注册过了");
        prefabMap.Add(key, prefab);         // 预制体容器 增加一个键值对
        for (int i = 0; i < preloadAmount; i++)    // 如果给了第三个参数，  生成对应数量的对象
        {
            var go = Instantiate(prefab) as GameObject;   //先创建出来
            AddGameObject(key, go);                       //增加到 对象池物体
            CollectObject(go);                            //再马上回收（给隐藏了）
        }

    }
    /// <summary>
    /// 注销预制体
    /// </summary>
    public void UnRegister(string key)
    {   if(prefabMap.ContainsKey(key))
        prefabMap.Remove(key);
    }
    /// <summary>
    /// 查找注册的原型对象
    /// </summary>
    public GameObject FindRegister(string key)
    {
        if (prefabMap.ContainsKey(key))
            return prefabMap[key];
        return null;
    }

    //存放对象（小怪A，go）
    public void AddGameObject(string key,GameObject go)
    {   
        if(!cache.ContainsKey(key))  //如果不包含key
           cache.Add(key,new List<GameObject>()); //对象数组 加一个 key的 新键值对

        cache[key].Add(go);  //那个key 还得添加一下物体
    }

    /// <summary>
    /// 取得对象
    /// </summary>
    /// <param name="key">对象KEY</param>
    /// <param name="prefab">创建对象的原型</param>
    /// <param name="position">创建对象的位置</param>
    /// <param name="quaternion">朝向</param>
    /// <returns></returns>
    public GameObject CreateObject(string key,GameObject prefab, Vector3 position,Quaternion quaternion)
    {

        //如果有空闲对象直接返回， 没有的话 创建之后存入缓存再返回

        GameObject tempGo = FindUsabe(key);  //查找key有没有闲置物体
        if(tempGo!=null)                    //有闲置物体
        {
            tempGo.transform.position=position;
            tempGo.transform.rotation=quaternion;
            tempGo.SetActive(true);
        }
        else
        {                                                   //没有闲置物体时
           tempGo= Instantiate(prefab, position, quaternion);
            AddGameObject(key,tempGo);
        }
        tempGo.transform.parent = this.transform;  //将闲置物体 放在 对象池父物体下  方便看。。

         return tempGo;

    }

    public GameObject CreateObject(string key, Vector3 position, Quaternion quaternion)
    {
        GameObject go = FindRegister(key);
        if(go!=null)
        {
           return CreateObject(key, go, position, quaternion);
        }
        return null;
    }
    /// <summary>
    /// 查找可用对象
    /// </summary>
    /// <param name="key">查找的KEY</param>
    /// <returns>返回一个非激活的物体</returns>
    private GameObject FindUsabe(string key)
    {
       if(cache.ContainsKey(key))
       {
           return cache[key].Find(p => !p.activeSelf);      
       }
       return null;
    }



    
    /// <summary>
    /// 释放某一类对象
    /// </summary>
    /// <param name="key"></param>
    public void Clear(string key)
    {
        //找到对应的list 将List中所有的物体销毁，并从字典中移除
        if(cache.ContainsKey(key))
        {
            for (int i = 0; i < cache[key].Count; i++)
            {
                Destroy(cache[key][i]);
            }
            //cache[key].ForEach(p => Destroy(p));   //或者这样是一样的
            cache.Remove(key);
        }
    }
    
    /// <summary>
    /// 释放所有对象
    /// </summary>
    public void ClearAll()
    {
        //遍历所有KEY 并调用CLEAR方法

        List<string> keys = new List<string>(cache.Keys);
        while(keys.Count>0)
        {
            Clear(keys[0]);
            keys.RemoveAt(0);
        }
    }
    
    /// <summary>
    /// 即时回收对象
    /// </summary>
    /// <param name="go"></param>
    public void CollectObject(GameObject go)
    {
        //将 GO 隐藏  设为非激活状态
        go.SetActive(false);
        go.transform.parent = this.transform;
    }
    
    /// <summary>
    /// 延时回收对象
    /// </summary>
    /// <param name="go"></param>
    /// <param name="time"></param>
    public void CollectObject(GameObject go, float time)
    {
        //等待一段时间之后  再调用即时回收
        StartCoroutine(WaitTime(go,time));

    }

    private IEnumerator WaitTime(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        CollectObject(go);
    }



  
}

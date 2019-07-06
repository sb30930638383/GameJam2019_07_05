using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager :MonoSingleton<SpawnManager>{

    public GameObject[] enemys;

    public List<Transform> bornPoint;

    public List<Wave> waves;

    public float moveWaitTime = 0.5f;
 

    Transform waveTeam;
    Transform bornPointTeam;

    public void Awake()
    {
        waveTeam = GameObject.Find("WaveTeam").transform;
        bornPointTeam = GameObject.Find("BornPointTeam").transform;

        for (int i = 0; i < waveTeam.transform.childCount; i++)
        {
            waves.Add(waveTeam.GetChild(i).GetComponent<Wave>());
        }

        for (int i = 0; i < bornPointTeam.transform.childCount; i++)
        {
            bornPoint.Add(bornPointTeam.GetChild(i).transform);
        }
    }

    public void Update()
    {

        //暂时没设计好怎么生成
        if (Input.GetKeyDown(KeyCode.Space))
        {
          StartCoroutine(BornEnemy());
        }

    }


    /// <summary> 用来设置 镜头缩放的 默认3波 放一次 5波放一次  以后再改</summary>
    public int waveCameraControl1=3;
    /// <summary> 用来设置 镜头缩放的 默认3波 放一次 5波放一次  以后再改</summary>
    public int waveCameraControl2 = 5;

    //生怪
    public IEnumerator BornEnemy()
    {
       
        for (int i = 0; i < enemys.Length; i++)
        {

            for (int j = 0; j < waves[0].enemyCount[i]; j++)
            {
                int ran = Random.Range(0, bornPoint.Count);

                GameObjectPool.instance.CreateObject(enemys[i].name, enemys[i], bornPoint[ran].position, Quaternion.identity);
                yield return new WaitForSeconds(moveWaitTime);
            }
        }
        waves.RemoveAt(0);//从波次里移除 每次生成从wavelist里读第一个元素
        GameManager.instance.waveCount += 1;
       if (GameManager.instance.waveCount == waveCameraControl1 || GameManager.instance.waveCount == waveCameraControl2)
       {
          GetComponent<CameraControl>().ScalingCamera();
       }
    }
}

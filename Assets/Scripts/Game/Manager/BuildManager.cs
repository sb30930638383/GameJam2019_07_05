using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Inst { get { return inst; } }
        private static BuildManager inst = new BuildManager();

        public GameObject EnemyPrefab;
        public Transform BuildPoints;
        public float BuildWaitTime = 0.5f;
        private List<Transform> buildPointsList;

        
        private void Start()
        {
            buildPointsList = new List<Transform>();
            if (BuildPoints!=null)
            {
                foreach (Transform item in BuildPoints)
                {
                    buildPointsList.Add(item);
                }
            }
        }
        /// <summary>
        /// 开启协程生成敌人
        /// </summary>
        /// <param name="BuildByWave">每隔X秒生成一波敌人</param>
        /// <param name="EnemyCount">第X波生成敌人数目</param>
        /// <returns></returns>
        public IEnumerator BuildEnemyWaveByMusic(float[] BuildByWave,int[] EnemyCount)
        {
            for (int i = 0; i < BuildByWave.Length; i++)
            {
                for (int j = 0; j < EnemyCount[i]; i++)
                {
                    Transform buildPoint = buildPointsList[Random.Range(0, buildPointsList.Count)];
                    EnemyEntity go = EntityManager.Inst.CreateEnemy<EnemyEntity>(buildPoint.position, buildPoint.forward);
                    go.RefreshModel("Enemey");
                    //不知道模型上是否有脚本EnemyEntity？
                    yield return new WaitForSeconds(BuildWaitTime);
                }
                yield return new WaitForSeconds(BuildByWave[i]);
            }

        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Inst { get; private set; }

        public Vector2 Position
        { get { return Global.MainCamera.transform.position; }
            set { Global.MainCamera.transform.position = new Vector3(value.x, value.y, -49); } }

        public Vector2 ShakeVec { set { shakeVec = value;  } }
        
        public Vector2 MousePosition { get; private set; }

        private EntityBase followEntity;
        private Coroutine shakeCor;
        private Vector2 aimPos;
        private Vector2 shakeVec;

        public void Init()
        {
            Inst = this;
        }

        private void Update()
        {
            RefreshAimPos();
            RefreshCameraPosition();
            MousePosition = Global.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        public void Follow(EntityBase entity)
        {
            followEntity = entity;
        }

        private void RefreshAimPos()
        {
            if (followEntity != null)
                aimPos = followEntity.Position;
        }

        private void RefreshCameraPosition()
        {
            Position = aimPos + shakeVec;
        }

        public void Shake(float atten = 1f, float duration = 0.6f, float speed = 1f, float eulerAtten = 0.5f)
        {
            if (shakeCor != null)
                StopCoroutine(shakeCor);
            shakeCor = StartCoroutine(IShake(atten, duration, speed, eulerAtten));
        }

        private IEnumerator IShake(float atten = 1f, float duration = 0.6f, float speed = 1f, float eulerAtten = 0.5f)
        {
            float radiusCoef = 0.3f * atten;    //抖动幅度 相关系数
            float shakeSpeed = 10 * speed;      //抖动速度
            Vector2 forPos = Vector2.zero;
            Vector2 curPos = Vector2.zero;
            shakeVec = forPos;
            Vector2 dir;
            float partDuration = 0;
            while (duration > 0)
            {
                forPos = curPos;
                curPos = UnityEngine.Random.insideUnitCircle * duration * radiusCoef;
                shakeVec = forPos;
                dir = (curPos - forPos).normalized;
                partDuration = (curPos - forPos).magnitude / shakeSpeed;
                while (partDuration > 0)
                {
                    shakeVec = forPos + dir * Time.deltaTime;
                    //outEulerShakeFloat = shakeVec * eulerAtten;
                    partDuration -= Time.deltaTime;
                    duration -= Time.deltaTime;
                    if (duration <= 0) break;
                    yield return null;
                }
                while (Time.timeScale <= 0)
                {
                    shakeVec = Vector2.zero;
                    //outEulerShakeFloat = 0;
                    yield return null;
                }
            }
            shakeVec = Vector2.zero;
            shakeCor = null;
            //outEulerShakeFloat = 0;
        }
    }
}
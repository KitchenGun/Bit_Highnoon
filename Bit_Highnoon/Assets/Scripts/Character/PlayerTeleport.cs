using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] OVRInput.Axis2D stick;
    private LineRenderer laser; //사용자에게 보일 레이저
    [SerializeField] int laserSteps = 10; //이동가능한 거리
    [SerializeField] float laserSegmentDistance = 1f, dropPerSegment = .1f; //레이저를 휘어지게 만드는 변수
    [SerializeField] Transform head, cameraRig;
    [SerializeField] int collisionLayer;//사용자가 이동가능한 레이어
    #region 텔레포트 조건 변수
    private bool TeleportEnable = false;
    private int SceneIdx;
    private GameObject GM;
    #endregion
    private Vector3 targetPos; //위치
    bool targetAcquired = false; //목표를 획득했는가

    #region Audio
    private AudioSource WalkAudio;
    #endregion

    private void Awake()
    {
        #region Scene 확인
        GM = GameObject.Find("GameManager");
        if (GM == null)
        {
            SceneIdx = 0;
        }//gamemanager가 존재 안할경우
        else
        {
            SceneIdx = GM.GetComponent<GameManager>().GetSceneIndex();
            if(SceneIdx==6)
            {
                SetTeleportEnable(true);
            }
            else
            {
                SetTeleportEnable(false);
            }
        }
        #endregion
        #region 씬 넘버를 통해서 텔레포드 조건 확인
        if(SceneIdx==0||SceneIdx==6)
        {
            TeleportEnable = true;
        }
        else
        {
            TeleportEnable = false;
        }
        #endregion
        laser = this.gameObject.GetComponent<LineRenderer>();
        laser.startWidth = laser.endWidth = 0.5f;
        laser.positionCount = laserSteps;//레이저가 보여질 거리
        #region Audio
        WalkAudio = this.gameObject.GetComponent<AudioSource>();
        #endregion
    }

    #region 텔포 가능 불가능 함수 전달
    private void SetTeleportEnable(bool value)
    {
        if(value)
        {
            this.gameObject.GetComponent<LineRenderer>().enabled = value;
        }
        else
        {
            this.gameObject.GetComponent<LineRenderer>().enabled = value;
        }
        TeleportEnable = value; 
    }
    #endregion

    private void Update()
    {
        //텔레포트 가능 상태일 경우
        if (TeleportEnable)
        {
            #region 텔레포트
            if (OVRInput.Get(stick).y > .8f)
            {
                TryToGetTeleportTarget();
            }
            else if (targetAcquired == true && OVRInput.Get(stick).y < .2f)
            {
                Teleport();
            }
            else if (targetAcquired == false && OVRInput.Get(stick).y < .2f)
            {
                ResetLaser();
            }
            #endregion
        }
    }

    #region 순간이동 타겟 얻기위해 레이저 쏘기
    private void TryToGetTeleportTarget()
    {
        targetAcquired = false;
        RaycastHit hit;
        Vector3 origin = transform.position;
        laser.SetPosition(0, origin);

        for (int i = 0; i < laserSteps - 1; i++)
        {
            Vector3 offset = (transform.forward + (Vector3.down * dropPerSegment * i)).normalized * laserSegmentDistance;

            if (Physics.Raycast(origin, offset, out hit, laserSegmentDistance))
            {
                for (int j = i + 1; j < laser.positionCount; j++)
                {
                    laser.SetPosition(j, hit.point);
                }

                if (hit.transform.gameObject.layer == collisionLayer) //레이어가 맞는 이동가능한 곳일 경우
                {
                    LaserColorSet(Color.green); //녹레이저
                    targetPos = hit.point;
                    targetAcquired = true;
                    return;
                }
                else
                {
                    LaserColorSet(Color.red);
                    return;
                }
            }
            else
            {
                laser.SetPosition(i + 1, origin + offset);
                origin += offset;
            }
        }
        LaserColorSet(Color.red);
    }
    #endregion

    #region 이동
    private void Teleport()
    {
        targetAcquired = false;
        ResetLaser();

        Vector3 offset = new Vector3(targetPos.x - head.transform.position.x, targetPos.y - cameraRig.position.y, targetPos.z - head.transform.position.z);

        cameraRig.position += offset;
        WalkAudio.clip = GM.GetComponent<GameManager>().RandomSound("walk");
        WalkAudio.Play();
    }
    #endregion

    #region 레이저 초기화
    private void ResetLaser()
    {
        for (int i = 0; i < laser.positionCount; i++)
        {
            laser.SetPosition(i, Vector3.zero);
        }
    }


    #endregion

    #region 레이저 색상 변경

    public void LaserColorSet(Color color)
    {
        laser.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        Gradient gradient = new Gradient();//그라데이션 색을 인자값으로 전달
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(1f, 1.0f) }
        );
        laser.colorGradient = gradient;
    }

    #endregion
}

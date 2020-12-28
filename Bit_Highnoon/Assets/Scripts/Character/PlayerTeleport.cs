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

    private Vector3 targetPos; //위치

    bool targetAcquired = false; //목표를 획득했는가

    private void Awake()
    {
        laser = this.gameObject.GetComponent<LineRenderer>();
        laser.startWidth = laser.endWidth = 0.5f;
        laser.positionCount = laserSteps;//레이저가 보여질 거리
    }

    private void Update()
    {
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
                    laser.startColor = laser.endColor = Color.green; //녹레이저
                    targetPos = hit.point;
                    targetAcquired = true;
                    return;
                }
                else
                {
                    laser.startColor = laser.endColor = Color.red;
                    return;
                }
            }
            else
            {
                laser.SetPosition(i + 1, origin + offset);
                origin += offset;
            }
        }

        laser.startColor = laser.endColor = Color.red;
    }
    #endregion

    #region 이동
    private void Teleport()
    {
        targetAcquired = false;
        ResetLaser();

        Vector3 offset = new Vector3(targetPos.x - head.transform.position.x, targetPos.y - cameraRig.position.y, targetPos.z - head.transform.position.z);

        cameraRig.position += offset;
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
}

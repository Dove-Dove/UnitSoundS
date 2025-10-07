using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyFind : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float angleRange = 50.0f;
    public float radius = 7.0f;
        
    public bool isCollision = false;


    //색갈 관련
    Color blue = new Color(0f, 0f, 1f, 0.2f);
    Color red = new Color(1f, 0f, 0f, 0.2f);
    Color yellow = new Color(1f, 1f, 0f, 0.2f);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 interV = target.position - transform.position;

        // target과 나 사이의 거리가 radius 보다 작다면
        if (interV.magnitude <= radius)
        {
            // '타겟-나 벡터'와 '내 정면 벡터'를 내적
            float dot = Vector3.Dot(interV.normalized, transform.forward);
            // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
            float theta = Mathf.Acos(dot);
            // angleRange와 비교하기 위해 degree로 변환
            float degree = Mathf.Rad2Deg * theta;
            RaycastHit hit;

            // 시야각 판별
            if (degree <= angleRange / 2f)
            {

                Debug.DrawRay(transform.position, interV, Color.red);
                //레이케스트를 이용한 벽 확인 
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    if (hit.collider.tag == "Player")
                    {
                        isCollision = true;

                    }
                    else
                    {
                        isCollision = false;
                    }
                }

                else
                {
                    isCollision = true;
                }

            }

        }
        else
            isCollision = false;
    }

    private void OnDrawGizmos()
    {
        Color color = isCollision ? red : blue;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);

    }
}

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


    //���� ����
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

        // target�� �� ������ �Ÿ��� radius ���� �۴ٸ�
        if (interV.magnitude <= radius)
        {
            // 'Ÿ��-�� ����'�� '�� ���� ����'�� ����
            float dot = Vector3.Dot(interV.normalized, transform.forward);
            // �� ���� ��� ���� �����̹Ƿ� ���� ����� cos�� ���� ���ؼ� theta�� ����
            float theta = Mathf.Acos(dot);
            // angleRange�� ���ϱ� ���� degree�� ��ȯ
            float degree = Mathf.Rad2Deg * theta;
            RaycastHit hit;

            // �þ߰� �Ǻ�
            if (degree <= angleRange / 2f)
            {

                Debug.DrawRay(transform.position, interV, Color.red);
                //�����ɽ�Ʈ�� �̿��� �� Ȯ�� 
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
        // DrawSolidArc(������, ��ֺ���(��������), �׷��� ���� ����, ����, ������)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody characterRigidbody;

    bool moveKeyDown = false;
    float movetime = 0;

    public LayerMask enemyLayer;
    public LayerMask wallLayer;

    public int rayCount = 30;
    public int collisionRayCount = 2;

    public float walkNoiseRadius = 3f;

    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
            moveKeyDown = true;
        else
            moveKeyDown = false;

        Vector3 move = new Vector3(h, 0, v);
        transform.Translate(move * speed * Time.deltaTime, Space.World);

        // ������ �� ���� �ð����� �Ҹ� �߻�
        if (moveKeyDown) movetime += Time.deltaTime;
        if (movetime >= 0.5f)
        {
            MakeNoise(walkNoiseRadius);
            noiseRay(transform.position, walkNoiseRadius, collisionRayCount, rayCount, 1.0f);
            movetime = 0;
        }
    }


    void MakeNoise(float radius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        foreach (Collider hit in hits)
        {
            EnemyMove enemy = hit.GetComponent<EnemyMove>();
            if (enemy != null)
            {
                enemy.GetPoint(transform.position);
            }
        }
    }

    void noiseRay(Vector3 pos , float radius, float deptm, int rayCount ,float intensity)
    {

        if (deptm <= 0 || intensity <= 0.1f)
            return;
     

        for (int i = 0; i < rayCount; i++)
        {
            Vector3 sRay = Random.onUnitSphere; //���� ǥ�鿡 �ִ� ���� ���Ƿ� �����Ͽ� ��ȯ�մϴ�. �� ���� ��ġ�� ��ȯ
            if (sRay.y < 0)
                continue;
           
            if (Physics.Raycast(pos, sRay ,out RaycastHit hit , enemyLayer | wallLayer))
            {
                Debug.DrawRay(pos, sRay * hit.distance, Color.yellow, 0.5f);
                EnemyMove enemy = hit.collider.GetComponent<EnemyMove>();
                if (enemy != null)
                {
                    enemy.GetPoint(pos);
                }

                if(((hit.collider.gameObject.layer) == wallLayer ) && deptm > 0)
                {
                    Vector3 reRay = Vector3.Reflect(pos, hit.normal); //Reflect ->���͸� �ݻ��Ű�� �Լ�
                    float reInten = intensity / 2;
                    noiseRay(pos + sRay * 0.1f, radius * 0.7f, deptm - 1, rayCount / 2, reInten);
                }
            }
        }

    }



    // ����׿� Gizmos (�Ҹ� �ݰ� �ð�ȭ)
    void OnDrawGizmos()
    {
        if (moveKeyDown) // ������ ���� ǥ��
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, walkNoiseRadius);
        }
    }
}

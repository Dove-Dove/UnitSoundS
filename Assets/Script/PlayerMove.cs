using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody characterRigidbody;

    bool moveKeyDown = false;
    float movetime = 0;

    public LayerMask enemyLayer;  
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

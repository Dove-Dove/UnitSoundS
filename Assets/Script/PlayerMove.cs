using UnityEngine;

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

        // 움직일 때 일정 시간마다 소리 발생
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

    void noiseRay(Vector3 pos, float radius, float deptm, int rayCount, float intensity)
    {
        if (deptm <= 0 || intensity <= 0.1f)
            return;

        for (int i = 0; i < rayCount; i++)
        {
            Vector3 sRay = Random.onUnitSphere; //구의 표면에 있는 점을 임의로 선택하여 반환합니다. 즉 랜덤 위치로 반환
            if (sRay.y < 0) continue;

            if (Physics.Raycast(pos, sRay, out RaycastHit hit, radius, enemyLayer | wallLayer))
            {
                Debug.DrawRay(pos, sRay * hit.distance, Color.red, 5f);

                EnemyMove enemy = hit.collider.GetComponent<EnemyMove>();
                if (enemy != null)
                    enemy.GetPoint(pos);

                // 반사 처리
                if (((1 << hit.collider.gameObject.layer) & wallLayer) != 0 && deptm > 0)
                {
                    Vector3 reRay = Vector3.Reflect(sRay, hit.normal); //Reflect ->백터를 반사시키는 함수
                    float reInten = intensity / 2f;

                    Debug.DrawRay(hit.point, reRay * (radius * 0.7f), Color.green, 5f);

                    noiseRay(hit.point + reRay * 0.2f, radius * 0.7f, deptm - 1, 1, reInten);
                }
            }
            else
            {
                Debug.DrawRay(pos, sRay * radius, Color.blue, 5f);
            }
        }
    }



    // 디버그용 Gizmos (소리 반경 시각화)
    void OnDrawGizmos()
    {
        if (moveKeyDown) // 움직일 때만 표시
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, walkNoiseRadius);
        }
    }
}

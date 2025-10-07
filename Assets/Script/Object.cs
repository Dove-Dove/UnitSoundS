using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충격량(Impulse 벡터)
        Vector3 impulse = collision.impulse;

        // 충격량 크기
        float impactForce = impulse.magnitude;

        Debug.Log($"충격량: {impactForce}");
    }
}

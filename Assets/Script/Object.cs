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
        // ��ݷ�(Impulse ����)
        Vector3 impulse = collision.impulse;

        // ��ݷ� ũ��
        float impactForce = impulse.magnitude;

        Debug.Log($"��ݷ�: {impactForce}");
    }
}

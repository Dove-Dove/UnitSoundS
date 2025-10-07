using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkHintObj : MonoBehaviour
{
    // Start is called before the first frame update
    float time = 0;
    

    // Update is called once per frame
    void Update()
    {
        createObjh();
    }

    void createObjh()
    {
        time += Time.deltaTime;

        transform.localScale =new Vector3(time  , time , time );
        if(time >= 4.0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyMove>().GetPoint(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}

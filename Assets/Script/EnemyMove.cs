using UnityEngine;
using UnityEngine.AI;


public class EnemyMove : MonoBehaviour
{
    enum UnitState
    {
        none,
        stop,
        move,

    }

    UnitState state;
    float moveSpeed = 3.0f;
    bool move = false;
    NavMeshAgent agent;
    public Vector3 movePoints;

    CharacterController cc;
    
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        state = UnitState.stop;

        //에이전트 컴포먼트
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case UnitState.stop:
                stopAction(movePoints);
                break;
            case UnitState.move:
                moveUnit(movePoints);
                break;
        }
        
    }

    void stopAction(Vector3 movePoint)
    {
        if (move)
        {
           state = UnitState.move;
        }
    }

    void moveUnit(Vector3 movePoint)
    {
        
        if (Vector3.Distance(transform.position, movePoint) > 1.0f)
        {
            agent.speed = moveSpeed;
            agent.destination = movePoint;

        }
        else
        {
            state = UnitState.stop;
            move = false;
        }
    }

    public void GetPoint(Vector3 getPos)
    {
        movePoints = getPos;
        move = true;
    }
}

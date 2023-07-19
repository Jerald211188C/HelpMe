using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{

    [SerializeField]


    private List<GameObject> Waypoints = new List<GameObject>();
    private int targetIndex;
    private Vector3 destination;
    private Vector3 dir;
    private float totalTime;
    private float startAngle;
    private float targetAngle;
    private float idleTime;

    public enum State
    {
        IDLE,
        PATROL,
        ATTACK
    }
    [SerializeField] private State currentState;


    // Start is called before the first frame update
    void Start()
    {

        targetIndex = 0;
        destination = Waypoints[targetIndex].transform.position;
        dir = (destination - transform.position).normalized;
        ChangeState(currentState);
    }





    // Update is called once per frame
    void Update()
    {

        if (currentState == State.IDLE)
        {
            Idle();
        }
        else if (currentState == State.PATROL)
        {
            Patrol();
        }
        else if (currentState == State.ATTACK)
        {

        }
    }

    private void ChangeState(State next)
    {
        if (next == State.IDLE)
        {
            idleTime = 0.0f;
            Vector3 axis = Vector3.zero;
            transform.rotation.ToAngleAxis(out startAngle,
            out axis);
            startAngle = transform.rotation.eulerAngles.z;
            targetAngle = startAngle + 360;
        }
        else if (next == State.PATROL)
        {
            destination = Waypoints[targetIndex].transform.position;
            dir = (destination - transform.position).normalized;
        }
        currentState = next;
    }

    private void Patrol()
    {
        transform.position += dir * Time.deltaTime;
        if (Vector3.Distance(destination, transform.position) <= 0.5f)
        {
            targetIndex++;
            targetIndex %= Waypoints.Count;
            ChangeState(State.IDLE);
        }
    }

    private void Idle()
    {
        idleTime += Time.deltaTime;


        float nextAngle = Mathf.Lerp(startAngle, targetAngle, idleTime);
        transform.rotation = Quaternion.Euler(0, 0, nextAngle);

        if (idleTime >= 2.0f)
        {
            ChangeState(State.PATROL);
        }
    }

}
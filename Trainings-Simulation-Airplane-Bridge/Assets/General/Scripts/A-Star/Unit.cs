using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool displayTurnGizmos;

    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    public Transform target;
    public Transform[] possibleTargets;
    public Transform[] hjonkTargets;

    public float speed = 5;
    public float turnDst = 5;
    public float turnSpeed = 3;
    public float stoppingDst = 10;

    /* needed for upgraded version
    public int maxItterations;

    public bool foundValidTarget;
    public bool waitingAtTarget;

    public List<Unit> bobs;
    */

    Paths path;

    [Range(0.0f, 30.0f)]
    public float minWaitTime, maxWaitTime;

    public AudioSource hjonk;

    public bool playedHjonk, arrivedAtTarget;

    [Header("Debug")]
    public string unitName;

    private void Start()
    {
        if (target == null)
        {
            target = possibleTargets[Random.Range(0, possibleTargets.Length)];
        }

        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        if(hjonk.isPlaying && !playedHjonk)
        {
            playedHjonk = true;

            StopAllCoroutines();

            target = hjonkTargets[Random.Range(0, hjonkTargets.Length)];

            //StartCoroutine(UpdatePath());

            StartCoroutine(ResetHjonk());
        }
    }

    IEnumerator NextPos()
    {
        Transform previousTarget = target;

        float nextPosTime = Random.Range(minWaitTime, maxWaitTime);

        yield return new WaitForSeconds(nextPosTime);

        target = possibleTargets[Random.Range(0, possibleTargets.Length)];

        arrivedAtTarget = false;

        StartCoroutine(UpdatePath());
    }

    IEnumerator ResetHjonk()
    {
        yield return new WaitForSeconds(hjonk.clip.length);

        playedHjonk = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Target" && !arrivedAtTarget)
        {
            arrivedAtTarget = true;

            target = possibleTargets[Random.Range(0, possibleTargets.Length)];

            StartCoroutine(NextPos());
            StartCoroutine(UpdatePath());

            arrivedAtTarget = false;
        }
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccesfull)
    {
        if (pathSuccesfull)
        {
            path = new Paths(waypoints, transform.position, turnDst, stoppingDst);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }

        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath()
    {
        bool followingPath = true;

        int pathIndex = 0;

        transform.LookAt(path.lookPoints[0]);

        float speedPercent = 1;

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);

            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {
                if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);

                    if (speedPercent < 0.01f)
                    {
                        followingPath = false;
                    }
                }

                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
            }

            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null && displayTurnGizmos)
        {
            path.DrawWithGizmos();
        }
    }
}
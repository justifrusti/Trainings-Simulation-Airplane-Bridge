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

    public int maxItterations;

    public bool foundValidTarget;
    public bool waitingAtTarget;

    public List<Unit> bobs;

    Paths path;

    public float minWaitTime, maxWaitTime;

    public AudioSource hjonk;

    public bool playedHjonk, arrivedAtTarget;

    [Header("Debug")]
    public string unitName;

    private void Start()
    {
        playedHjonk = false;

        StartCoroutine(UpdatePath());

        FindTarget(maxItterations);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < stoppingDst && !arrivedAtTarget)
        {
            arrivedAtTarget = true;

            if (waitingAtTarget)
            {
                StartCoroutine(NextPos());
            }
        }

        if(hjonk.isPlaying && !playedHjonk)
        {
            playedHjonk = true;
            StartCoroutine(ResetHjonk());

            target = hjonkTargets[Random.Range(0, hjonkTargets.Length)];
            
            Debug.Log("Moving towards escape target: " + target);
        }
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccesfull)
    {
        if(pathSuccesfull)
        {
            path = new Paths(waypoints, transform.position, turnDst, stoppingDst);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {
        if(Time.timeSinceLevelLoad < .3f)
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
                PathRequestManager.RequestPath(new PathRequest (transform.position, target.position, OnPathFound));
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

    IEnumerator ResetHjonk()
    {
        yield return new WaitForSeconds(1);

        playedHjonk = false;
    }

    IEnumerator NextPos()
    {
        waitingAtTarget = true;

        foundValidTarget = false;

        float timeToWait = Random.Range(minWaitTime, maxWaitTime);
        print("Time till next move: "+ timeToWait + " for Unit: " + unitName);

        yield return new WaitForSeconds(timeToWait);

        FindTarget(maxItterations);

        arrivedAtTarget = false;

        waitingAtTarget = false;
    }

    public void FindTarget(int maxItterations)
    {
        if(target == null)
        {
            target = possibleTargets[Random.Range(0, possibleTargets.Length)];
        }

        int itterations = 0;

        if(maxItterations > itterations)
        {
            for (int i = 0; i < bobs.Count; i++)
            {
                if(bobs[i].target == target)
                {
                    target = possibleTargets[Random.Range(0, possibleTargets.Length)];

                    foundValidTarget = false;
                }
            }
        }else if(maxItterations == itterations && !foundValidTarget)
        {
            target = possibleTargets[Random.Range(0, possibleTargets.Length)];

            foundValidTarget = true;
        }
    }

    public void OnDrawGizmos()
    {
        if (displayTurnGizmos)
        {
            if (path != null)
            {
                path.DrawWithGizmos();
            }
        }
    }
}
using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class EnemyAi : MonoBehaviour {

    public Transform target;

    public float updateRate = 2f; // czas co jaki AI bedzie szukac drogi

    private Seeker seeker;
    private Rigidbody2D rb;

    //obliczana droga
    public Path path;

    public float aiSpeed = 30f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    public float nextWayPointDistance = 3; // max odleglosc od AI do punktu docelowego aby kontynuowac droge do kolejnego punktu 

    private int currentWaypoint = 0; // punkt docelowy w danym momencie 

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            Debug.LogError("Target error no player found!");
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    void FixedUpdate()
    {
        if(target == null)
        {
        return;
        }

        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
            else
            {
                Debug.Log("End of path");
                pathIsEnded = true;
                return;
            }
        }
        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] = transform.position).normalized;
        dir *= aiSpeed * Time.fixedDeltaTime;

        // poruszanie AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWayPointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
           yield return false;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Path" + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlohaMove : EntityMove
{
    //hunts for the nearest player
    GameObject[] players;
    protected override Transform findTarget()
    {
        GameObject closestPlayer = null;
        float minDistance = 1000;

        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            if (closestPlayer == null)
            {
                closestPlayer = p;
                minDistance = Vector3.Distance(transform.position, p.transform.position);
            }
            else if (Vector3.Distance(transform.position, p.transform.position) < minDistance)
            {
                closestPlayer = p;
                minDistance = Vector3.Distance(transform.position, p.transform.position);
            }
        }

        if (closestPlayer == null) return null;
        return closestPlayer.transform;
    }
}

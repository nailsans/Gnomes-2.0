using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hunts for the nearest player
/// </summary>
public class BlohaMove : EntityMove
{
    /// <summary>
    /// Making array of all players as we will hunt for them
    /// </summary>
    GameObject[] players;

    /// <summary>
    /// Finding nearest players
    /// </summary>
    /// <returns></returns>
    protected override Transform findTarget()
    {
        GameObject closestPlayer = null;
        float minDistance = 1000;

        /// Finding available players to hunt for
        players = GameObject.FindGameObjectsWithTag("Player");

        /// Calculating which player is closest to our position
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

        /// If player is found succeccsfully, move to it
        if (closestPlayer == null) return null;
        return closestPlayer.transform;
    }
}

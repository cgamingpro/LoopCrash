
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailSpawner : MonoBehaviour
{
    public GameObject[] trailsOnj;
    public float delay = 1f;
    public Vector3 skycentre;
    public Vector3 spaawnlimit;     // full size of the spawn box (x,y,z)
    public Vector3 rotaion;
    public int maxTrail = 10;
    public int currentTrial = 0;

    // tuning
    public int maxAttempts = 10;       // how many times to re-roll before fallback
    public float minDistanceFromPlayer = 5f; // minimum distance from player

    [Tooltip("Assign player Transform in inspector (optional). If left null, script will try to find GameObject tagged 'Player' or Camera.main")]
    public Transform player;

    void Start()
    {
        // try to auto-find a player transform if none assigned
        if (player == null)
        {
            var pgo = GameObject.FindGameObjectWithTag("Player");
            if (pgo != null) player = pgo.transform;
            else if (Camera.main != null) player = Camera.main.transform;
        }

        InvokeRepeating(nameof(spawntrail), 0f, delay);
    }

    void spawntrail()
    {
        if (currentTrial >= maxTrail) return;
        if (trailsOnj == null || trailsOnj.Length == 0) return;

        GameObject prefab = trailsOnj[Random.Range(0, trailsOnj.Length)];
        Vector3 randPos = GetSpawnPosition();

        GameObject trail = Instantiate(prefab, randPos, Quaternion.Euler(rotaion.x, rotaion.y, Random.Range(-rotaion.z, rotaion.z)));
        trail.transform.localScale = Vector3.one *  Random.Range(1, 4);
        currentTrial++;

        ParticleSystem ps = trail.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            float life = GetParticleLifetime(ps);
            Destroy(trail, life);
            // original behavior: if particle system exists, don't count it against maxTrail
            currentTrial--;
        }
    }

    Vector3 GetSpawnPosition()
    {
        float minDistSqr = minDistanceFromPlayer * minDistanceFromPlayer;
        Vector3 pos = skycentre;
        int attempts = 0;

        // sample inside axis-aligned box until we get a position far enough from player
        while (attempts < maxAttempts)
        {
            pos = skycentre + new Vector3(
                Random.Range(-spaawnlimit.x * 0.5f, spaawnlimit.x * 0.5f),
                Random.Range(-spaawnlimit.y * 0.5f, spaawnlimit.y * 0.5f),
                Random.Range(-spaawnlimit.z * 0.5f, spaawnlimit.z * 0.5f)
            );

            if (player == null) break; // no player to check against
            if ((pos - player.position).sqrMagnitude >= minDistSqr) break;

            attempts++;
        }

        // fallback: push the position out along the direction from player to pos
        if (player != null && (pos - player.position).sqrMagnitude < minDistSqr)
        {
            Vector3 dir = (pos - player.position).normalized;
            if (dir == Vector3.zero) dir = Vector3.forward; // avoid zero vector
            pos = player.position + dir * minDistanceFromPlayer;

            // clamp fallback inside spawn box so it doesn't end outside allowed area
            Vector3 local = pos - skycentre;
            local.x = Mathf.Clamp(local.x, -spaawnlimit.x * 0.5f, spaawnlimit.x * 0.5f);
            local.y = Mathf.Clamp(local.y, -spaawnlimit.y * 0.5f, spaawnlimit.y * 0.5f);
            local.z = Mathf.Clamp(local.z, -spaawnlimit.z * 0.5f, spaawnlimit.z * 0.5f);
            pos = skycentre + local;
        }

        return pos;
    }

    float GetParticleLifetime(ParticleSystem ps)
    {
        // Use the max lifetime as a safe destruction time for varied lifetime curves
        try
        {
            return ps.main.startLifetime.constantMax;
        }
        catch
        {
            // fallback if constantMax isn't available for some reason
            try { return ps.main.startLifetime.constant; } catch { return 5f; }
        }
    }

    void Update()
    {
        skycentre = transform.position;
    }
}

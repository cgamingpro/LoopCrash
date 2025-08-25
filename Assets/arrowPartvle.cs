using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowPartvle : MonoBehaviour
{
    [SerializeField]List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    // Start is called before the first frame update
    private void OnEnable()
    {
        foreach (ParticleSystem p in particleSystems)
        {
            p.Play();
        }
    }
}

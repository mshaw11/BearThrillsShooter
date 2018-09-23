using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    
    void Awake()
    {
        ParticleSystem ps = this.GetComponentInChildren<ParticleSystem>();
        var main = ps.main;
        Destroy(this.gameObject, main.duration + main.startLifetime.constantMax);
    }
}

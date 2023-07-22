using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem _ps;
    // Start is called before the first frame update
    void Start()
    {
        if(!_ps)
        _ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_ps.isPlaying)
            Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class DestroyWhenDone : MonoBehaviour
{

    // Use this for initialization
    private void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            GameObject.Destroy(gameObject, ps.main.duration + ps.main.startLifetime.Evaluate(1f));
        }
    }
}
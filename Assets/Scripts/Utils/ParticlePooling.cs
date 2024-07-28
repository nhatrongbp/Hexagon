using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooling : MonoBehaviour
{
    public static ParticlePooling instance;
    public List<ParticleSystem> particles;
    Camera _cam;

    void Awake(){
        if(instance != null && instance != this) Destroy(gameObject);
        else {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        _cam = Camera.main;
        foreach (var p in particles)
        {
            p.Stop();
        }
    }

    // Update is called once per frame
    public void PlayParticle(int i, Vector2 pos)
    {
        MyDebug.Log("play particle at {0} {1}", pos.x, pos.y);
        if(i < particles.Count){
            particles[i].transform.position = pos;
            particles[i].Play();
        }
    }
}

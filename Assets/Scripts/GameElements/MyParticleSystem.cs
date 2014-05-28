using UnityEngine;
using System.Collections.Generic;

public abstract class MyParticleSystem : MonoBehaviour {

    protected System.Random rg = new System.Random();

    protected bool IsActive = false;

    public Vector3 InitialPosition;
    public Material ParticleMaterial;
    protected Color ParticleColor;
    // blend mode
    public string SystemType;

    public List<GameObject> Particles;

    public int NumAliveParticles;
    public int NumMaxParticles = 20;
    // bounding box???

    protected float phi;
    protected float theta;
    protected float baseVelocity;


    void Start() {
        Particles = new List<GameObject>();
        ParticleColor = ParticleMaterial.GetColor("_TintColor");
        Debug.Log("start: " + GameObject.Find("Explosion").transform.position);
    }
    

    // Init 

    //public void CreateSP(Vector3 pos) {
    //    IsActive = true;
    //    // generic        
    //    GameObject g;
    //    MyParticle par;
    //    //Vector3 pos = transform.position;
    //    for (int i = 0; i < NumMaxParticles; i++) {
    //        g = Instantiate(GameObject.Find("ParticleSphere"), pos, new Quaternion()) as GameObject;
    //        par = g.GetComponent(typeof(MyParticle)) as MyParticle;
    //        InitParticle(par);
    //        Particles.Add(g);
    //        g.renderer.material = par.Mat;
    //    }
    //    NumAliveParticles = Particles.Count;
    //    InitSP(pos);
    //}

    public void InitSP() {
        GameObject go;
        MyParticle p;
        Vector3 pos2 = transform.position;
        InitialPosition = pos2;
        //Vector3 pos2 = new Vector3(-17, -148, 20);

        IsActive = true;

        if (Particles.Count > 0) {
            Debug.Log("Init1");
            foreach (GameObject g in Particles) {
                p = g.GetComponent(typeof(MyParticle)) as MyParticle;
                InitParticle(p, pos2);
                g.renderer.material = p.Mat;
            }
        }
        else {
            Debug.Log("Init2");
            
            // first instantiation
            for (int i = 0; i < NumMaxParticles; i++) {
                //go = Instantiate(GameObject.Find("ParticleSphere"), pos, new Quaternion()) as GameObject;
                go = Instantiate(GameObject.Find("ParticleSphere"), pos2, new Quaternion()) as GameObject;
                p = go.GetComponent(typeof(MyParticle)) as MyParticle;
                InitParticle(p, pos2);
                Particles.Add(go);
                go.renderer.material = p.Mat;
            }
        }
        NumAliveParticles = Particles.Count;
    }

    public void Die() {
        foreach(GameObject g in Particles) {
            Destroy(g);
        }
        Particles.Clear();
    }

    public abstract void InitFormulas();
    public abstract void InitPosition(MyParticle p, Vector3 pos);
    public abstract void InitVelocity(MyParticle p);
    public abstract void InitAcceleration(MyParticle p);
    public abstract void UpdateAcceleration(MyParticle p);

    public void InitParticle(MyParticle p, Vector3 pos) {
        InitFormulas();

        p.LifeTime = 1.0f;
        p.Mat = ParticleMaterial;
        InitPosition(p, pos);
        p.PreviousVelocity.x = p.PreviousVelocity.y = p.PreviousVelocity.z = 0;
        InitVelocity(p);
        InitAcceleration(p);
        p.Size = 0.1f;
    }

    // Auxiliary updates

    protected void UpdatePreviousValues(MyParticle p) {
        p.PreviousPosition.x = p.Position.x;
        p.PreviousPosition.y = p.Position.y;
        p.PreviousPosition.z = p.Position.z;

        p.PreviousVelocity.x = p.Velocity.x;
        p.PreviousVelocity.y = p.Velocity.y;
        p.PreviousVelocity.z = p.Velocity.z;
    }

    protected void UpdatePosition(MyParticle p) {
        p.Position.x = p.PreviousPosition.x + Time.deltaTime * p.PreviousVelocity.x;
        p.Position.y = p.PreviousPosition.y + Time.deltaTime * p.PreviousVelocity.y;
        p.Position.z = p.PreviousPosition.z + Time.deltaTime * p.PreviousVelocity.z;
    }

    protected void UpdateVelocity(MyParticle p) {
        p.Velocity.x = p.PreviousVelocity.x + Time.deltaTime * p.Acceleration.x;
        p.Velocity.y = p.PreviousVelocity.y + Time.deltaTime * p.Acceleration.y;
        p.Velocity.z = p.PreviousVelocity.z + Time.deltaTime * p.Acceleration.z;
    }

    protected void UpdateSize(MyParticle p) {
        p.Size += p.LifeTimeDelta;
    }

    protected void UpdateMaterial(MyParticle p) {
        ParticleColor.a = p.LifeTime;
        p.Mat.SetColor("_TintColor", ParticleColor);
    }
}

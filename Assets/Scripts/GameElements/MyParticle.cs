using UnityEngine;
using System.Collections;

public class MyParticle : MonoBehaviour {


    public Vector3 Position;
    public Vector3 PreviousPosition;
    public Vector3 Velocity;
    public Vector3 PreviousVelocity;
    public Vector3 Acceleration;
    public float Size;
    public float LifeTime;
    public float LifeTimeDelta;
    public Material Mat;
    // optional:
    // - wind

    void Awake() {
    }

    void Start() {
        Mat = renderer.material;
        LifeTime = 1.0f;
        LifeTimeDelta = 0.05f;
    }

    void Update() {
        if (IsAlive()) {
            transform.position = Position;
            renderer.material = Mat;
        }
    }



    public bool IsAlive() {
        if (LifeTime <= 0.0f)
            return false;
        return true;
    }
}

using UnityEngine;
using System.Collections;

public class WaterPS : MyParticleSystem {

    protected override void InitLifeTime(MyParticle p) {
        p.LifeTime = (float)rg.NextDouble();
        //p.LifeTime = 1.0f;
    }

    public override void InitFormulas() {
        phi = (float)rg.NextDouble() * Mathf.PI/3; ; //(float)rg.NextDouble() * 2* Mathf.PI;
        theta = (float)rg.NextDouble() * Mathf.PI/3;
        baseVelocity = 0.001f; // (float)rg.NextDouble() * 0.8f + 0.2f;
    }

    public override void InitVelocity(MyParticle p) {
        p.Velocity.x = baseVelocity * Mathf.Cos(theta) * Mathf.Sin(phi);
        p.Velocity.y = baseVelocity * Mathf.Cos(phi);
        p.Velocity.z = baseVelocity * Mathf.Sin(theta) * Mathf.Sin(phi);
    }

    public override void InitAcceleration(MyParticle p) {
        p.Acceleration.x = (p.Velocity.x - p.PreviousVelocity.x) / Time.deltaTime;
        p.Acceleration.y = -10.0f * (p.Velocity.y - p.PreviousVelocity.y) / Time.deltaTime;
        p.Acceleration.z = (p.Velocity.z - p.PreviousVelocity.z) / Time.deltaTime;
    }

    public override void InitPosition(MyParticle p, Vector3 pos) {
        p.Position.x = p.PreviousPosition.x = pos.x + (float)(rg.NextDouble());
        p.Position.y = p.PreviousPosition.y = pos.y - (float)(rg.NextDouble() * 10.0f);
        p.Position.z = p.PreviousPosition.z = pos.z + (float)(rg.NextDouble());
    }

    public override void UpdateAcceleration(MyParticle p) {
        p.Acceleration.x = (p.Velocity.x - p.PreviousVelocity.x) / Time.deltaTime;
        p.Acceleration.y = -10.0f + (p.Velocity.y - p.PreviousVelocity.y) / Time.deltaTime;
        p.Acceleration.z = (p.Velocity.z - p.PreviousVelocity.z) / Time.deltaTime;
    }

    void Update() {

        if (IsActive) {

            /*if (NumAliveParticles <= 0) {
                IsActive = false;
            }
            else {*/
                NumAliveParticles = 0;

                foreach (GameObject g in Particles) {

                    MyParticle p = g.GetComponent(typeof(MyParticle)) as MyParticle;

                    // Particle count
                    if (p.IsAlive()) {
                        NumAliveParticles++;
                        p.LifeTime -= p.LifeTimeDelta;
                        if (p.LifeTime < 0.0f) p.LifeTime = 0.0f;
                        UpdatePreviousValues(p);
                        UpdateMaterial(p);
                        UpdateSize(p);
                        UpdatePosition(p);
                        UpdateVelocity(p);
                        UpdateAcceleration(p);
                    }
                    else {
                        InitParticle(p, InitialPosition);
                    }
                }
            //}
        }

    }
}

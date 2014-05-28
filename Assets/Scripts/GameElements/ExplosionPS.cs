using UnityEngine;
using System.Collections;

public class ExplosionPS : MyParticleSystem {

    public override void InitFormulas() {
        phi = (float)rg.NextDouble() * Mathf.PI;
        theta = (float)rg.NextDouble() * 2 * Mathf.PI;
        baseVelocity = (float)rg.NextDouble() * 0.8f + 0.2f;
    }

    public override void InitVelocity(MyParticle p) {
        p.Velocity.x = baseVelocity * Mathf.Cos(theta) * Mathf.Sin(phi);
        p.Velocity.y = baseVelocity * Mathf.Cos(phi);
        p.Velocity.z = baseVelocity * Mathf.Sin(theta) * Mathf.Sin(phi);
    }

    public override void InitAcceleration(MyParticle p) {
        p.Acceleration.x = (p.Velocity.x - p.PreviousVelocity.x) / Time.deltaTime;
        p.Acceleration.y = (p.Velocity.y - p.PreviousVelocity.y) / Time.deltaTime;
        p.Acceleration.z = (p.Velocity.z - p.PreviousVelocity.z) / Time.deltaTime;
    }

    public override void InitPosition(MyParticle p, Vector3 pos) {
        p.Position.x = p.PreviousPosition.x = pos.x;
        p.Position.y = p.PreviousPosition.y = pos.y;
        p.Position.z = p.PreviousPosition.z = pos.z;
    }

    public override void UpdateAcceleration(MyParticle p) {
        p.Acceleration.x = (p.Velocity.x - p.PreviousVelocity.x) / Time.deltaTime;
        p.Acceleration.y = (p.Velocity.y - p.PreviousVelocity.y) / Time.deltaTime;
        p.Acceleration.z = (p.Velocity.z - p.PreviousVelocity.z) / Time.deltaTime;
    }

    void Update() {

        if (IsActive) {

            if (NumAliveParticles <= 0) {
                IsActive = false;
            }
            else {

                NumAliveParticles = 0;

                foreach (GameObject g in Particles) {

                    MyParticle p = g.GetComponent(typeof(MyParticle)) as MyParticle;

                    // Particle count
                    if (p.IsAlive()) {
                        NumAliveParticles++;
                        p.LifeTime -= p.LifeTimeDelta;
                        if (p.LifeTime < 0) p.LifeTime = 0.0f;
                        UpdatePreviousValues(p);
                        UpdateMaterial(p);
                        UpdateSize(p);
                        UpdatePosition(p);
                        UpdateVelocity(p);
                        UpdateAcceleration(p);
                    }
                }
            }
        }

    }

}

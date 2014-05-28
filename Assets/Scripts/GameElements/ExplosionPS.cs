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
                    //else {
                    //    InitParticle(p);
                    //}
                }
            }
        }

    }

}

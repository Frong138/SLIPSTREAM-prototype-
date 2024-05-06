using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCollision : MonoBehaviour
{
    private CarController cc;

    void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        Component component = ps.trigger.GetCollider(0);

        cc = component.GetComponent<CarController>();

        if (component.CompareTag("PlayerCar"))
        {
            if (cc != null)
            {
                while(cc.boost < cc.maxBoost)
                {
                    cc.boost += 2;
                    if (cc.boost > cc.maxBoost) cc.boost = cc.maxBoost;
                    return;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] int numberOfParticles = 10;
    List<ParticleCollisionEvent> particleCollisionEvents;
    void Start()
    {
        particleCollisionEvents = new List<ParticleCollisionEvent>();
    }
    void OnParticleCollision(GameObject other)
    {
        ParticleSystem lasers = other.GetComponent<ParticleSystem>();
        ParticlePhysicsExtensions.GetCollisionEvents(lasers, gameObject, particleCollisionEvents);
        for (int i = 0; i < particleCollisionEvents.Count; i++)
        {
            hitEffect.transform.position = particleCollisionEvents[i].intersection;
            hitEffect.transform.rotation = Quaternion.LookRotation(particleCollisionEvents[i].normal);
            hitEffect.Emit(numberOfParticles);
        }
    }
}

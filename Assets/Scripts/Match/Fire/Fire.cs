using UnityEngine;

public abstract class Fire : MonoBehaviour
{
    [SerializeField] private protected ParticleSystem Particle;

    protected abstract void OnTriggerEnter(Collider collider);
}

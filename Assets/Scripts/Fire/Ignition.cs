using UnityEngine;

public class Ignition : Fire
{
    [SerializeField] private Match _match;

    private int _stateBurnt = 2;

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Attenuation _) 
            || collider.TryGetComponent(out Fuse _))
        {
            if (_match.State != _stateBurnt)
            {
                Particle.gameObject.SetActive(true);
                Particle.Play();
            }

        }
    }
}

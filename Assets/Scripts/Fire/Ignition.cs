using UnityEngine;

public class Ignition : Fire
{
    [SerializeField] private Match _match;

    private int stateBurnt = 2;

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Attenuation _) 
            || collider.TryGetComponent(out Fuse _) 
            && _match.State != stateBurnt)
        {
            Particle.gameObject.SetActive(true);
            Particle.Play();
        }
    }

    public override void Reload()
    {
        Particle.Stop();
        Particle.gameObject.SetActive(false);
    }
}

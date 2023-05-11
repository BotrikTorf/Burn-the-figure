using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(SphereCollider))]
public class Combustion : Fire
{
    [SerializeField] private Transform _endTransform;
    [SerializeField] private Match _match;

    private Transform _transform;
    private SphereCollider _sphereCollider;
    private float _speed = 1f;
    private int _stateBurnt = 2;

    public event UnityAction BurningMatch;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Attenuation _)
            || collider.TryGetComponent(out Fuse _))
        {
            if (_match.State != _stateBurnt)
            {
                BurningMatch?.Invoke();
                Particle.gameObject.SetActive(true);
                Particle.Play();
                StartCoroutine(MovementFire());
            }
        }
    }

    public void ControlsStateCollider()
    {
        if (_sphereCollider.enabled)
            _sphereCollider.enabled = false;
        else
            _sphereCollider.enabled = true;
    }

    private IEnumerator MovementFire()
    {
        while (_transform.position != _endTransform.position)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position,
                _endTransform.position,
                _speed * Time.deltaTime);
            yield return null;
        }

        Particle.Stop();
        Particle.gameObject.SetActive(false);
        StopCoroutine(MovementFire());
    }
}

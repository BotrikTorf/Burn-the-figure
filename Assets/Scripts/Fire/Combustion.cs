using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(Transform))]
public class Combustion : Fire
{
    [SerializeField] private Transform _endTransform;
    [SerializeField] private Match _match;
    [SerializeField] private Transform _startTransform;

    private Transform _transform;
    private Vector3 _startPosition;
    private float _speed = 0.0005f;
    private int _stateBurnt = 2;
    private float _startPositionX;
    private float _startPositionY;
    private float _startPositionZ;

    public event UnityAction BurningMatch;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Attenuation _)
            || collider.TryGetComponent(out Fuse _)
            && _match.State != _stateBurnt)
        {
            BurningMatch?.Invoke();
            Particle.gameObject.SetActive(true);
            Particle.Play();
            StartCoroutine(MovementFire());
        }
    }

    private void Start()
    {
        //_startPosition = new Vector3(
        //    _startTransform.position.x,
        //    _startTransform.position.y,
        //    _startTransform.position.z);

        _startPositionX = transform.position.x;
         _startPositionY = transform.position.y;
        _startPositionZ = transform.position.z;
    }

    public override void Reload()
    {
        StopCoroutine(MovementFire());
        transform.position = new Vector3(_startPositionX, _startPositionY, _startPositionZ);
        Particle.Stop();
        Particle.gameObject.SetActive(false);
    }

    private IEnumerator MovementFire()
    {
        float deltaPosition = 0f;
        Transform startTransform = _transform;

        while (Vector3.Distance(_endTransform.position, _transform.position) > 0.3)
        {
            _transform.position = Vector3.Lerp(
                startTransform.position,
                _endTransform.position,
                deltaPosition);
            deltaPosition += Time.deltaTime * _speed;
            yield return null;
        }

        Particle.Stop();
        Particle.gameObject.SetActive(false);
        StopCoroutine(MovementFire());
    }
}

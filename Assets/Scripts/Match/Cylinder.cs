using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cylinder : MonoBehaviour
{
    [SerializeField] private Material _material;

    private MeshRenderer _meshRenderer;


    private void Awake() => _meshRenderer = GetComponent<MeshRenderer>();

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Combustion _))
            _meshRenderer.material = _material;
    }
}

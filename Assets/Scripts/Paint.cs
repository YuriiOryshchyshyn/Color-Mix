using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(MeshRenderer))]
public class Paint : MonoBehaviour
{
    [SerializeField] private Material _transparentMaterial;

    private MeshRenderer _meshRenderer;

    public bool IsPainted { get => GetComponent<MeshRenderer>().material.color.a != 0; }
    public Color Color { get => _meshRenderer.material.color; set => _meshRenderer.material.color = value; }
    public MeshRenderer Renderer => _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetTransparentMaterial()
    {
        _meshRenderer.material = _transparentMaterial;
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }

    public void SetMaterial(Material material)
    {
        _meshRenderer.material = material;
    }
}
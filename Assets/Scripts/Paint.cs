using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(MeshRenderer))]
public class Paint : MonoBehaviour
{
    [SerializeField] private Material _transparentMaterial;

    private MeshRenderer _meshRenderer;
    private Color _color;

    public bool IsPainted { get => _color.a != 0; }
    public Color Color => _color;
    public MeshRenderer Renderer => _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _color = _meshRenderer.material.color;
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
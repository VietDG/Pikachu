using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public int sortingOrder = 90;

    [Range(0f, 1f)]
    public float radius = 0.5f;

    [Range(0f, 1f)]
    public float size = 0.5f;

    [Range(0f, 0.1f)]
    public float magnitude = 0.05f;

    private Material material;

    private static int radiusId = Shader.PropertyToID("_Radius");

    private static int sizeId = Shader.PropertyToID("_Size");

    private static int magnitudeId = Shader.PropertyToID("_Force");

    void Awake()
    {
        if (meshRenderer != null) meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingOrder = sortingOrder;
        material = meshRenderer.material;
    }

    private void Update()
    {
        material.SetFloat(radiusId, radius);
        material.SetFloat(sizeId, size);
        material.SetFloat(magnitudeId, magnitude);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChunk : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private bool _isHit;

    public bool TryPainting(Color color)
    {
        if (_isHit)
        {
            _meshRenderer.material.color = Color.red;
            return false;
        }

        _meshRenderer.enabled = true;
        _isHit = true;
        _meshRenderer.material.color = color;
        return true;
    }
}
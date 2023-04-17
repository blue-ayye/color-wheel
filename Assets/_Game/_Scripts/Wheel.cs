using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private MeshRenderer _smoothWheelRenderer;

    [SerializeField] private WheelChunk _wheelSectionPrefab;
    [SerializeField] private float _wheelRadius = 9.4f;
    [SerializeField] private int _numberOfChunks = 23;
    [SerializeField] private float _wheelRotationSpeed = 100f;
    [SerializeField] private float _wheelRotationDuration = 1f;
    [SerializeField] private float _waitTimeBetweenRotations = .5f;

    private List<WheelChunk> _wheelChunks;

    private void Start()
    {
        SpawnChunks();
        RotateWheel();
    }

    public void PaintWheel(Color color)
    {
        _smoothWheelRenderer.material.color = color;
        _wheelChunks.ForEach(chunk => chunk.DisableRenderer());
    }

    private void SpawnChunks()
    {
        var angleStep = 360f / _numberOfChunks;

        _wheelChunks = new List<WheelChunk>();

        for (int i = 0; i < _numberOfChunks; i++)
        {
            var angle = angleStep * i * Mathf.Deg2Rad;
            var x = _wheelRadius * Mathf.Cos(angle);
            var z = _wheelRadius * Mathf.Sin(angle);

            var wheelChunk = Instantiate(_wheelSectionPrefab,
                                         new Vector3(x, transform.position.y, z),
                                         Quaternion.identity,
                                         transform);
            wheelChunk.DisableRenderer();
            _wheelChunks.Add(wheelChunk);
            wheelChunk.transform.rotation = Quaternion.Euler(0, -angleStep * i + 90f, 0);
        }
    }

    private void RotateWheel()
    {
        Sequence wheelRotationSequence = DOTween.Sequence();

        // Rotate in one direction
        wheelRotationSequence.Append(transform.DORotate(new Vector3(0, _wheelRotationSpeed, 0),
                                                        _wheelRotationDuration,
                                                        RotateMode.LocalAxisAdd));

        // Pause
        wheelRotationSequence.AppendInterval(_waitTimeBetweenRotations);

        // Rotate in the opposite direction
        wheelRotationSequence.Append(transform.DORotate(new Vector3(0, -_wheelRotationSpeed, 0),
                                                        _wheelRotationDuration,
                                                        RotateMode.LocalAxisAdd));

        // Loop the sequence
        wheelRotationSequence.SetLoops(-1);
    }
}
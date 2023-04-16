using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class WheelSpawner : MonoBehaviour
{
    [Header("Wheel Config")]
    [SerializeField] private Wheel _wheelPrefab;
    [SerializeField] private float _wheelHeight = 4f;

    [Header("Spawn Config")]
    [SerializeField] private Vector3 _spawnPosition = new Vector3(0, 15, 0);
    [SerializeField] private float _fallDuration = .15f;
    [SerializeField] private Ease _wheelFallEase = Ease.Flash;

    [Header("Impact Config")]
    [SerializeField] private float _wheelShiftDuration = .3f;
    [SerializeField] private Ease _wheelShiftEase = Ease.OutBack;

    private readonly List<Wheel> _spawnedWheels = new();

    private void Start()
    {
        SpawnWheel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnWheel();
        }
    }

    private void SpawnWheel()
    {
        var wheel = Instantiate(_wheelPrefab, _spawnPosition, Quaternion.identity);
        wheel.transform.parent = transform;
        _spawnedWheels.Add(wheel);

        wheel.transform.DOMoveY(_wheelHeight, _fallDuration)
             .SetEase(_wheelFallEase)
             .OnComplete(ShiftWheelsDown);
    }

    private void ShiftWheelsDown()
    {
        var pos = transform.position.y - _wheelHeight;

        transform.DOMoveY(pos, _wheelShiftDuration)
                 .SetEase(_wheelShiftEase);
    }
}
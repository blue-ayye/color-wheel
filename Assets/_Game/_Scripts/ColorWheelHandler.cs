using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ColorWheelHandler : MonoBehaviour
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

    private List<Wheel> _spawnedWheels;

    private void Awake()
    {
        _spawnedWheels = new List<Wheel>();
    }

    public void ClearWheels()
    {
        _spawnedWheels.ForEach(wheel => Destroy(wheel.gameObject));
        _spawnedWheels.Clear();
    }

    public void SpawnWheel()
    {
        var wheel = Instantiate(_wheelPrefab, _spawnPosition, Quaternion.identity, transform);
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

    public void CompleteWheel(Color ballColor)
    {
        _spawnedWheels[^1].PaintWheel(ballColor);
    }
}
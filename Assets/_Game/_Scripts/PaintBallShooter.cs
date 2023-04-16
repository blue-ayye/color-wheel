using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBallShooter : MonoBehaviour
{
    [SerializeField] private PaintBall _paintBallPrefab;
    [SerializeField] private float _paintBallForce = 5f;
    [SerializeField] private Color _paintBallColor;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var paintBall = Instantiate(_paintBallPrefab, transform.position, Quaternion.identity);
            paintBall.Shoot(Vector3.forward * _paintBallForce, _paintBallColor);
        }
    }
}
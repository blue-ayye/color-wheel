using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBallShooter : MonoBehaviour
{
    [SerializeField] private GameObject _paintBallPrefab;
    [SerializeField] private float _paintBallForce = 5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var paintBall = Instantiate(_paintBallPrefab, transform.position, Quaternion.identity);
            paintBall.GetComponent<Rigidbody>().AddForce(Vector3.forward * _paintBallForce, ForceMode.Impulse);
        }
    }
}
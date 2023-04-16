using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpawner : MonoBehaviour
{
    [SerializeField] private Wheel _wheelPrefab;

    private readonly List<Wheel> _spawnedWheels = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var wheel = Instantiate(_wheelPrefab);
            _spawnedWheels.Add(wheel);

            _spawnedWheels.ForEach(wheel1 => wheel1.transform.position += Vector3.down * 4);
            // wheel.transform.position -= new Vector3(0, -1, 0);
        }
    }
}
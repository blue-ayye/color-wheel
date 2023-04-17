using System;
using UnityEngine;

public class PaintBallHandler : MonoBehaviour
{
    public static event Action OnPaintComplete;
    public static event Action OnPaintFailed;

    [SerializeField] private PaintBall _paintBallPrefab;
    [SerializeField] private float _paintBallForce = 5f;

    private Color _ballColor;
    private int _remainingBalls;
    private bool _canShoot;

    private void Awake()
    {
        PaintBall.OnHit += PaintBall_OnOnHit;
    }

    private void Start()
    {
        Init(3, Color.cyan);
    }

    public void Init(int ballCount, Color ballColor)
    {
        _remainingBalls = ballCount;
        _ballColor = ballColor;
        _canShoot = true;
    }

    private void PaintBall_OnOnHit(bool hitValid)
    {
        _canShoot = true;

        if (hitValid)
        {
            _remainingBalls--;

            if (_remainingBalls == 0)
                OnPaintComplete?.Invoke();
        }
        else
        {
            OnPaintFailed?.Invoke();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            _canShoot = false;
            var paintBall = Instantiate(_paintBallPrefab, transform.position, Quaternion.identity);
            paintBall.Shoot(Vector3.forward * _paintBallForce, _ballColor);
        }
    }
}
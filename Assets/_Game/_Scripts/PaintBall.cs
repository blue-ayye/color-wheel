using System;
using UnityEngine;

public class PaintBall : MonoBehaviour
{
    public static event Action<bool> OnHit;

    [SerializeField] private Rigidbody _rb;

    private Color _painBallColor;
    private bool _hasHit;

    public void Shoot(Vector3 paintBallForce, Color paintBallColor)
    {
        _painBallColor = paintBallColor;
        _rb.AddForce(paintBallForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_hasHit) return;

        if (!other.transform.TryGetComponent(out WheelChunk wheelChunk)) return;

        _hasHit = true;
        OnHit?.Invoke(wheelChunk.TryPainting(_painBallColor));
        Destroy(gameObject);
    }
}
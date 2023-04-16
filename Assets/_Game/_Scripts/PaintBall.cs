using UnityEngine;

public class PaintBall : MonoBehaviour
{
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
        if (other.transform.TryGetComponent(out WheelChunk wheelChunk))
        {
            if (wheelChunk.TryPainting(_painBallColor))
            {
                //Hit
            }
            else
            {
                Debug.Log("Failed");
            }
        }
        _hasHit = true;
        Destroy(gameObject);
    }
}
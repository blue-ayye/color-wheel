using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [FormerlySerializedAs("_wheelSpawner")]
    [SerializeField] private ColorWheelHandler _colorWheelHandler;
    [SerializeField] private PaintBallHandler _paintBallHandler;

    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _currentWheel = 1;
    [SerializeField] private int _totalWheels = 4;
    [SerializeField] private int _ballsPerLevel = 3;
    [SerializeField] private List<Gradient> _paintColors;

    [SerializeField] private Gradient _currentPaintPallet;

    private Color BallColor => _currentPaintPallet.Evaluate((float)_currentWheel / _totalWheels);

    private void Awake()
    {
        PaintBallHandler.OnPaintComplete += PaintComplete;
        PaintBallHandler.OnPaintFailed += PaintFailed;
    }

    private void Start()
    {
        _currentLevel = PlayerPrefs.GetInt(nameof(_currentLevel), 1);
        _currentWheel = 1;
        _currentPaintPallet = _paintColors[Random.Range(0, _paintColors.Count)];
        SpawnWheel();
        ResetBall();
    }

    private void PaintFailed()
    {
        Debug.Log("Level failed");
    }

    private void PaintComplete()
    {
        _colorWheelHandler.CompleteWheel(BallColor);

        if (_currentWheel == _totalWheels)
        {
            StartCoroutine(LevelCompleteRoutine());
            return;
        }

        _currentWheel++;
        SpawnWheel();
        ResetBall();
    }

    private IEnumerator LevelCompleteRoutine()
    {
        yield return new WaitForSeconds(2f);
        _currentLevel++;
        PlayerPrefs.SetInt(nameof(_currentLevel), _currentLevel);

        yield return new WaitForSeconds(.1f);
        DOTween.KillAll();
        _colorWheelHandler.ClearWheels();

        yield return new WaitForSeconds(.1f);
        _currentWheel = 1;
        _currentPaintPallet = _paintColors[Random.Range(0, _paintColors.Count)];
        ResetBall();
        SpawnWheel();
    }

    private void SpawnWheel() => _colorWheelHandler.SpawnWheel();
    private void ResetBall() => _paintBallHandler.Init(_ballsPerLevel, BallColor);
}
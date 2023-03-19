using TMPro;
using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _ballShootTr;
    [SerializeField] private float _shootInterval = 0.1f;
    
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private int _startingBallCount = 3;

    [SerializeField] private float _playerMoveXLimit;

    [SerializeField] private TMP_Text _ballCountText;

    private AimController _aim;
    
    private bool _playerTurn = false;
    private float _entryBallX;

    private List<Ball> _balls = new List<Ball>();
    private int _ballsReturned;
    private int _ballCount; 

    public Action OnPlayerTurnFinished;

    private void Start()
    {
        for (int i = 0; i < _startingBallCount; i++)
        {
            AddBall();
        }

        _aim = GetComponent<AimController>();
        _aim.enabled = false;

        _ballCountText.text = $"x {_startingBallCount}";
    }

    private void Update()
    {
        if (!_playerTurn) return;
    
        if (Input.GetMouseButtonDown(1)) 
        {
            _aim.enabled = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _aim.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void AddBall(Ball ball = null)
    {
        if (ball == null)
            ball = Instantiate(_ballPrefab);
        _balls.Add(ball);
    }

    private void Shoot()
    {
        _playerTurn = false;
        _aim.enabled = false;

        _ballCount = _balls.Count; // length could increase mid shooting
        _ballsReturned = 0;

        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {        
        for (int i = 0; i < _ballCount; i++)
        {
            _balls[i].Activate(_ballShootTr);
            _ballCountText.text = $"x {_ballCount - i - 1}";
            yield return new WaitForSeconds(_shootInterval);
        }
    }

    public void OnBallReturn(float x)
    {   
        if (++_ballsReturned == 1)
        {
            _entryBallX = x;
        }
        if (_ballsReturned == _ballCount)
        {
            StartCoroutine(TurnFinishRoutine());
        }
    }

    private IEnumerator TurnFinishRoutine() // move to entry ball pos and notify turn finish
    {
        Vector3 dest = transform.position;
        dest.x = Mathf.Clamp(_entryBallX, -_playerMoveXLimit, _playerMoveXLimit);

        yield return StartCoroutine(transform.SmoothMoveTo(dest, .25f));

        OnPlayerTurnFinished?.Invoke();
    }

    public void OnPlayerTurnStart()
    {
        _ballCountText.text = $"x {_balls.Count}";
        _playerTurn = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 xLimitStart = transform.position + Vector3.left * _playerMoveXLimit;
        Gizmos.DrawLine(xLimitStart, xLimitStart + Vector3.right * _playerMoveXLimit * 2);
        Handles.Label(xLimitStart, "player move constraints");
    }

}

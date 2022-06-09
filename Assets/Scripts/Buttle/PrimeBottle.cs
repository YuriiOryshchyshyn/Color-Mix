using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using System;

public class PrimeBottle : MonoBehaviour
{
    [SerializeField] private float _animationUpOffset;
    [SerializeField] private List<Paint> _paints;
    [SerializeField] private float _primeSpeed;

    private PrimerController _primeController;
    private bool _isChoosedButtle = false;
    private List<Paint> _paintedPaints;
    private List<Paint> _notPaintedPaints;
    private Paint _currentPaint;
    private Paint _currentNotPaint;
    private bool _isFull = false;
    private bool _isEmpty = false;

    public event UnityAction<PrimeBottle, PrimeBottle> EndPrimeProcess;

    public Paint CurrentNotPaint => _currentNotPaint;
    public bool IsFull => _isFull;
    public bool IsEmpty => _isEmpty;

    private void Awake()
    {
        LocalInit();
    }

    public void TryMoveUpAnimation()
    {
        if (_isChoosedButtle)
            return;
        transform.DOMoveY(transform.localPosition.y + _animationUpOffset, 0.5f);
        _isChoosedButtle = true;
    }

    public void Init(PrimerController primeContoller, ButtleModel bottleModel)
    {
        _primeController = primeContoller;
        for (int i = 0; i < bottleModel.PaintsModels.Count; i++)
        {
            _paints[i].SetMaterial(bottleModel.PaintsModels[i].Material);
        }
        LocalInit();
    }

    private void LocalInit()
    {
        _paintedPaints = _paints.Where(paint => paint.IsPainted).ToList();
        _notPaintedPaints = _paints.Where(paint => paint.IsPainted == false).ToList();
        _notPaintedPaints.Reverse();
    }

    public void TryMoveDownAnimation()
    {
        if (!_isChoosedButtle)
            return;
        transform.DOMoveY(transform.localPosition.y - _animationUpOffset, 0.5f);
        _isChoosedButtle = false;
    }

    public void PrimeColor(PrimeBottle buttle)
    {
        Reload();
        buttle.Reload();
        IEnumerator primeColorCoroutine = PrimeColorCoroutine(buttle);
        StartCoroutine(primeColorCoroutine);
    }

    public void CurrentPaintSetColor(Color color)
    {
        Reload();
        _currentPaint.SetColor(color);
    }

    private IEnumerator PrimeColorCoroutine(PrimeBottle buttle)
    {
        Vector3 originScale = _currentPaint.transform.localScale;
        Vector3 targetScale = new Vector3(_currentPaint.transform.localScale.x, _currentPaint.transform.localScale.y, 0);
        float lerpValue = 0;
        buttle.Reload();
        buttle.CurrentNotPaint.SetMaterial(_currentPaint.Renderer.material);
        buttle.CurrentNotPaint.transform.localScale = new Vector3(100, 100, 0);
        float originalScaleZ = buttle.CurrentNotPaint.transform.localScale.z;
        Color lastPaintColor = _currentPaint.Color;

        while (_currentPaint.transform.localScale != targetScale)
        {
            _currentPaint.transform.localScale = Vector3.Lerp(originScale, targetScale, lerpValue);
            buttle.Prime(originalScaleZ, lerpValue);
            buttle.ColorLerp(_currentPaint, lerpValue);
            lerpValue += _primeSpeed * Time.deltaTime;
            yield return null;
        }

        _currentPaint.SetTransparentMaterial();
        _currentPaint.transform.localScale = originScale;
        _paintedPaints.Remove(_currentPaint);
        _notPaintedPaints.Add(_currentPaint);
        buttle.AddPaint();
        buttle.Reload();
        _primeController.SetEmptyButtles();
        Reload();

        if (_currentPaint.Color == lastPaintColor && buttle._notPaintedPaints.Count > 0)
        {
            StartCoroutine(PrimeColorCoroutine(buttle));
        }
        else
        {
            TryMoveDownAnimation();
            EndPrimeProcess?.Invoke(this, buttle);
        }
    }

    public void ColorLerp(Paint currentPaint, float lerpValue)
    {
        Color originalColor = currentPaint.Color;
        Color targetColor = originalColor + _currentPaint.Color;
        _currentNotPaint.Color = Color.Lerp(originalColor, targetColor, lerpValue);
    }

    private void Reload()
    {
        if (_paintedPaints.Count == 0)
        {
            _isEmpty = true;
        }
        else
        {
            _isEmpty = false;
            _currentPaint = _paintedPaints[_paintedPaints.Count - 1];
        }

        if (_notPaintedPaints.Count == 0)
        {
            _isFull = true;
        }
        else
        {
            _isFull = false;
            _currentNotPaint = _notPaintedPaints[_notPaintedPaints.Count - 1];
        }
    }

    private void Prime(float originalScaleZ, float lerpValue)
    {
        _currentNotPaint.transform.localScale = new Vector3(100, 100, Mathf.Lerp(originalScaleZ, 100, lerpValue));
    }

    public void AddPaint()
    {
        _paintedPaints.Add(_currentNotPaint);
        _notPaintedPaints.Remove(_currentNotPaint);
        if (_notPaintedPaints.Count > 0)
        {
            _currentNotPaint = _notPaintedPaints[0];
        }
        else
        {
            _currentNotPaint = null;
        }
        Reload();
    }
}

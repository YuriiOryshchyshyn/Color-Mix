using UnityEngine;

public class PrimerController : MonoBehaviour
{
    [SerializeField] private TouchController _touchController;

    private PrimeBottle _firstButtle;
    private PrimeBottle _secondButtle;

    private bool _inPrimeProcess;

    private void OnEnable()
    {
        _touchController.PointerOnButtle += OnPointerButtleClick;
    }

    private void OnDisable()
    {
        _touchController.PointerOnButtle -= OnPointerButtleClick;
    }

    private void OnPointerButtleClick(PrimeBottle buttle)
    {
        if (_inPrimeProcess)
            return;

        if (_firstButtle == null)
        {
            _firstButtle = buttle;
            _firstButtle.EndPrimeProcess += OnPrimeEnded;
            if (_firstButtle.IsEmpty)
            {
                _firstButtle.EndPrimeProcess -= OnPrimeEnded;
                SetEmptyButtles();
            }
            else
            {
                buttle.TryMoveUpAnimation();
            }
        }
        else
        {
            _secondButtle = buttle;
            _secondButtle.EndPrimeProcess += OnPrimeEnded;
            if (_secondButtle.IsFull || _firstButtle == _secondButtle)
            {
                _firstButtle.TryMoveDownAnimation();
                OnPrimeEnded(_firstButtle, _secondButtle);
                SetEmptyButtles();
            }
            else
            {
                PrimeColor(_firstButtle, _secondButtle);
                _inPrimeProcess = true;
            }
        }
    }

    private void OnPrimeEnded(PrimeBottle firstButtle, PrimeBottle secondButtle)
    {
        _inPrimeProcess = false;
        firstButtle.EndPrimeProcess -= OnPrimeEnded;
        secondButtle.EndPrimeProcess -= OnPrimeEnded;
    }

    private void PrimeColor(PrimeBottle firstButtle, PrimeBottle secondButtle)
    {
        firstButtle.PrimeColor(secondButtle);
    }

    public void SetEmptyButtles()
    {
        _firstButtle = null;
        _secondButtle = null;
    }
}

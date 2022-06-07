using UnityEngine;

public class PrimerController : MonoBehaviour
{
    [SerializeField] private TouchController _touchController;

    private Buttle _firstButtle;
    private Buttle _secondButtle;

    private void OnEnable()
    {
        _touchController.PointerOnButtle += OnPointerButtleClick;
    }

    private void OnDisable()
    {
        _touchController.PointerOnButtle -= OnPointerButtleClick;
    }

    private void OnPointerButtleClick(Buttle buttle)
    {
        if (_firstButtle == null)
        {
            _firstButtle = buttle;
            if (_firstButtle.IsEmpty)
            {
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
            if (_secondButtle.IsFull)
            {
                _firstButtle.TryMoveDownAnimation();
                SetEmptyButtles();
            }
            else
            {
                PrimeColor(_firstButtle, _secondButtle);
            }
        }
    }

    private void PrimeColor(Buttle firstButtle, Buttle secondButtle)
    {
        firstButtle.PrimeColor(secondButtle);
    }

    public void SetEmptyButtles()
    {
        _firstButtle = null;
        _secondButtle = null;
    }
}

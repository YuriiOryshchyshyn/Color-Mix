using UnityEngine;
using UnityEngine.Events;

public class TouchController : MonoBehaviour
{
    public event UnityAction<Buttle> PointerOnButtle;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                Hit(hit);
        }
    }

    private void Hit(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Buttle buttle))
        {
            PointerOnButtle?.Invoke(buttle);
        }
    }
}

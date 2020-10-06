using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(BoxCollider2D))]
public class CustomButton : MonoBehaviour
{
    public UnityEvent DoOnClick;

    private void OnMouseDown()
    {
#if UNITY_EDITOR
        Debug.Log("Button clicked");
#endif
        DoOnClick?.Invoke();
    }
}
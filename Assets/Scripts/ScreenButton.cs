using UnityEngine;

public class ScreenButton : MonoBehaviour
{
    [SerializeField] private Color UI_Color;
    private GameObject _selected;

    void Awake()
    {
        _selected = transform.GetChild(0).gameObject;
    }

    public Color GetColor()
    {
        return(UI_Color);
    }

    public GameObject GetSelected()
    {
        return(_selected);
    }
}

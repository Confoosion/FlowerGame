using UnityEngine;
using UnityEngine.UI;

public class SelectHighlight : MonoBehaviour
{
    private GameObject selectHighlight;
    private string HIGHLIGHT_NAME = "SelectHighlight";
    private bool lockHighlight = false;

    void Awake()
    {
        foreach(Transform child in transform)
        {
            if(child.name.Equals(HIGHLIGHT_NAME))
            {
                selectHighlight = child.gameObject;
                break;
            }
        }
        
        if(selectHighlight == null)
        {
            Debug.LogWarning(gameObject + " has no SelectHighlight GameObject attached!");
            return;
        }
        
        selectHighlight.SetActive(false);
    }

    public void DisplayHighlight(bool show)
    {
        selectHighlight.SetActive(show);
    }

    public void LockHighlight(bool _lock)
    {
        lockHighlight = _lock;
    }

    public bool IsHighlightLocked()
    {
        return(lockHighlight);
    }
}

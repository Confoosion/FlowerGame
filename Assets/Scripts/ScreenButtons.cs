using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenButtons : MonoBehaviour
{
    [SerializeField] private ScreenButton currentButton;
    private GameObject currentButton_obj;
    private Image buttons_BG;
    private Coroutine colorLerpRoutine;
    private Color currentButton_color;
    private Color lerpingColor;
    private float colorLerpDuration = 0.5f;

    void Awake()
    {
        buttons_BG = GetComponent<Image>();

        if(currentButton != null)
        {
            SelectButton(currentButton);
        }
    }

    public void SelectButton(ScreenButton button)
    {
        if(button.GetSelected() == currentButton_obj)
            return;
        
        if(currentButton_obj != null)
        {
            currentButton_obj.SetActive(false);
            currentButton = null;

            ChangeUIColor(button.GetColor());
        }
        else
        {
            currentButton_color = button.GetColor();
            lerpingColor = currentButton_color;
            buttons_BG.color = currentButton_color;
        }

        currentButton_obj = button.GetSelected();
        currentButton_obj.SetActive(true);

        button = currentButton;
    }

    private void ChangeUIColor(Color newColor)
    {
        Color oldColor = currentButton_color;
        currentButton_color = newColor;

        if(colorLerpRoutine != null)
        {
            StopCoroutine(colorLerpRoutine);
            colorLerpRoutine = StartCoroutine(ColorLerp(lerpingColor, newColor));
        }

        colorLerpRoutine = StartCoroutine(ColorLerp(oldColor, newColor));
    }

    IEnumerator ColorLerp(Color oldColor, Color newColor)
    {
        float t = 0f;

        while(t < colorLerpDuration)
        {
            lerpingColor = Color.Lerp(oldColor, newColor, t/colorLerpDuration);
            buttons_BG.color = lerpingColor;

            t += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        lerpingColor = newColor;
        buttons_BG.color = lerpingColor;

        yield return null;
    }
}

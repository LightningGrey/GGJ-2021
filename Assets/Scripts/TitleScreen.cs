using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Button controls;
    [SerializeField] private Button exit;
    [SerializeField] private Image controlsPopup;
    [SerializeField] private Image controlsBG;

    private Color originalPopupColor;
    private Color hiddenPopupColor;

    private Color originalBGColor;
    private Color hiddenBGColor;

    private bool controlsIsHidden = true;


    // Start is called before the first frame update
    void Start()
    {
        originalPopupColor = controlsPopup.color;
        originalBGColor = controlsBG.color;
        hiddenPopupColor = originalPopupColor;
        hiddenBGColor = originalBGColor;
        hiddenPopupColor.a = 0.0f;
        hiddenBGColor.a = 0.0f;
        // set image transparent
        controlsPopup.color = hiddenPopupColor;
        controlsBG.color = hiddenBGColor;
    }

    public void onPressControls()
    {
        if (controlsIsHidden)
        {
            controlsPopup.color = originalPopupColor;
            controlsBG.color = originalBGColor;
            controlsIsHidden = false;
        }
        else
        {
            controlsPopup.color = hiddenPopupColor;
            controlsBG.color = hiddenBGColor;
            controlsIsHidden = true;
        }
        
    }

    public void onPressContinue()
    {
        //Debug.Log("bing");
        SceneManager.LoadScene(1);
        //SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }
}

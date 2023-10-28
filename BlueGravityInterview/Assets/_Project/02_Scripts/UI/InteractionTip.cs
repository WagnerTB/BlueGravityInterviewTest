using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class InteractionTip : MonoBehaviour
{
    [SerializeField]
    private CanvasGroupFade _canvasGroupFade;

    private bool _isVisible = false;
    
    public void SetEnabled(bool enabled)
    {
        _isVisible = enabled;
        
        if(_isVisible)
            _canvasGroupFade.FadeIn();
        else
        {
            _canvasGroupFade.FadeOut();
        }
    }
    
}

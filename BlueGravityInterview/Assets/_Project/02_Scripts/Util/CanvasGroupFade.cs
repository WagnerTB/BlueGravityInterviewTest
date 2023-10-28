using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupFade : MonoBehaviour
    {
        /// <summary>
        /// True = Ended Fade In, 
        /// False = Ended Fade out
        /// </summary>
        public Action<bool> OnCompletedFade;

        [SerializeField]
        private float _defaultFadeTime = 1;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        private Coroutine _fadeInCoroutine;
        private Coroutine _fadeOutCoroutine;

        private float _elapsedTime = 0;
        private float _initialValue = 0;
        private float _endValue;

        public void FadeIn(float fadeTime = -1)
        {
            if (_fadeInCoroutine != null)
            {
                Debug.LogWarning("[Canvas Group Fade] Has fade in running!");
                return;
            }

            if (_canvasGroup.alpha == 1)
                return;

            _elapsedTime = 0;

            if (_fadeOutCoroutine != null)
            {
                StopCoroutine(_fadeOutCoroutine);
                OnCompletedFade?.Invoke(false);
                _initialValue = _canvasGroup.alpha;
            }
            else
            {
                _initialValue = 0;
            }

            _endValue = 1;

            if (fadeTime <= 0)
                fadeTime = _defaultFadeTime;

            _fadeInCoroutine = StartCoroutine(CoFade(_initialValue, _endValue, fadeTime));
        }

        public void FadeOut(float fadeTime = -1)
        {
            if (_fadeOutCoroutine != null)
            {
                Debug.LogWarning("[Canvas Group Fade] Has fade out running!");
                return;
            }

            if (_canvasGroup.alpha == 0)
                return;

            _elapsedTime = 0;


            if (_fadeInCoroutine != null)
            {
                StopCoroutine(_fadeInCoroutine);
                OnCompletedFade?.Invoke(true);
                _initialValue = _canvasGroup.alpha;
            }
            else
            {
                _initialValue = 1;
            }

            _endValue = 0;

            if (fadeTime <= 0)
                fadeTime = _defaultFadeTime;

            _fadeOutCoroutine = StartCoroutine(CoFade(_initialValue, _endValue, fadeTime));
        }

        private IEnumerator CoFade(float initialValue, float endValue, float time)
        {
            _canvasGroup.alpha = initialValue;
            if (initialValue == 1)
            {
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }

            while (_elapsedTime < time)
            {
                var alphaLerp = Mathf.Lerp(initialValue, endValue, _elapsedTime / time);
                _canvasGroup.alpha = alphaLerp;
                _elapsedTime += Time.deltaTime;
                yield return null;
            }


            _canvasGroup.alpha = endValue;

            if (endValue == 1)
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
                _fadeInCoroutine = null;
                OnCompletedFade?.Invoke(true);
            }
            else
            {
                _fadeOutCoroutine = null;
                OnCompletedFade?.Invoke(false);
            }
        }

        private void Reset()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetAlpha(int alpha)
        {
            _canvasGroup.alpha = alpha;
        }
    }
}
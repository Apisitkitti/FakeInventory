using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Rov.InventorySystem
{
    public class UIManager : MonoBehaviour
    {
        public float UIfadeTime = 1f;
        public CanvasGroup canvasGroup;
        public RectTransform rectTransform;

        public void PanelFadeIn()
        {
            canvasGroup.alpha = 0f;
            rectTransform.transform.localPosition = new Vector3(0f, -30f, 0f);
            rectTransform.DOAnchorPos(new Vector2(0f, 0f), UIfadeTime, false).SetEase(Ease.OutElastic);
            canvasGroup.DOFade(1, UIfadeTime);
        }

        public void PanelFadeOut()
        {
            canvasGroup.alpha = 1f;
            rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
            rectTransform.DOAnchorPos(new Vector2(0f, -30f), UIfadeTime, false).SetEase(Ease.InOutQuint);
            canvasGroup.DOFade(0, UIfadeTime);
        }

    }
    
}
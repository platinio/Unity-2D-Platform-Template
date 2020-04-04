using Platinio.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace Gamaga.UI
{
    public class Popup : MonoBehaviour
    {

        [SerializeField] private Vector2 startPosition = Vector2.zero;
        [SerializeField] private Vector2 desirePosition = Vector2.zero;
        [SerializeField] private PivotPreset startPivot = PivotPreset.MiddleCenter;
        [SerializeField] private PivotPreset movePivot = PivotPreset.MiddleCenter;
        [SerializeField] private RectTransform canvas = null;
        [SerializeField] private float time = 0.5f;
        [SerializeField] private Ease enterEase = Ease.EaseInOutExpo;
        [SerializeField] private Ease exitEase = Ease.EaseInOutExpo;
        [SerializeField] private Button button = null;

        private bool isVisible = false;
        private bool isBusy = false;
        private RectTransform thisRect = null;

        public UnityEvent OnClick { get { return button.onClick; } } 

        private void Start()
        {
            thisRect = GetComponent<RectTransform>();

            thisRect.anchoredPosition = thisRect.FromAbsolutePositionToAnchoredPosition(startPosition, canvas , startPivot);
        }

        public void Show(Action onComplete = null )
        {
            thisRect.MoveUI(desirePosition, canvas, time , movePivot).SetEase(enterEase).SetOnComplete(delegate
            {
                isBusy = false;
                isVisible = true;
            }).SetOnComplete(onComplete);

        }

        public void Hide(Action onComplete = null)
        {
            thisRect.MoveUI(startPosition, canvas, time , movePivot).SetEase(exitEase).SetOnComplete(delegate
            {
                isBusy = false;
                isVisible = false;
            }).SetOnComplete( onComplete );
        }

        public void Toggle(Action onComplete = null)
        {
            if (isBusy)
                return;

            isBusy = true;

            if (isVisible)
                Hide( onComplete );
            else
                Show( onComplete );
        }
    }

}

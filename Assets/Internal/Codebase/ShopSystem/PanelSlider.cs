using DG.Tweening;
using UnityEngine;

namespace Codebase
{
    public class PanelSlider : MonoBehaviour
    {
        public enum SlideDirection
        {
            FromRight,
            FromLeft,
            FromBottom,
            FromTop
        }

        private bool isOpen;

        [SerializeField] private RectTransform panel;
        [SerializeField] private SlideDirection direction = SlideDirection.FromRight;
        [SerializeField] private float moveDuration = 0.5f;

        private Vector2 hiddenPosition;
        private Vector2 visiblePosition;
        private int originalSiblingIndex;

        private void Awake()
        {
            originalSiblingIndex = panel.GetSiblingIndex();
            CalculatePositions();

            // Старт — скрыт
            panel.anchoredPosition = hiddenPosition;
            isOpen = false;
        }

        private void CalculatePositions()
        {
            RectTransform canvasRect = panel.parent as RectTransform;
            if (canvasRect == null) return;

            float canvasWidth = canvasRect.rect.width;
            float canvasHeight = canvasRect.rect.height;

            float panelWidth = panel.rect.width;
            float panelHeight = panel.rect.height;

            // Если панель растянута (full screen), берём ширину/высоту канваса
            if (panelWidth <= 0) panelWidth = canvasWidth;
            if (panelHeight <= 0) panelHeight = canvasHeight;

            switch (direction)
            {
                case SlideDirection.FromRight:
                    // Панель прижата к правому краю (anchor = правый центр)
                    panel.anchorMin = new Vector2(1f, 0.5f);
                    panel.anchorMax = new Vector2(1f, 0.5f);
                    panel.pivot = new Vector2(1f, 0.5f);

                    visiblePosition = new Vector2(0f, 0f);
                    hiddenPosition = new Vector2(panelWidth, 0f);
                    break;

                case SlideDirection.FromLeft:
                    panel.anchorMin = new Vector2(0f, 0.5f);
                    panel.anchorMax = new Vector2(0f, 0.5f);
                    panel.pivot = new Vector2(0f, 0.5f);

                    visiblePosition = new Vector2(0f, 0f);
                    hiddenPosition = new Vector2(-panelWidth, 0f);
                    break;

                case SlideDirection.FromBottom:
                    panel.anchorMin = new Vector2(0.5f, 0f);
                    panel.anchorMax = new Vector2(0.5f, 0f);
                    panel.pivot = new Vector2(0.5f, 0f);

                    visiblePosition = new Vector2(0f, 0f);
                    hiddenPosition = new Vector2(0f, -panelHeight);
                    break;

                case SlideDirection.FromTop:
                    panel.anchorMin = new Vector2(0.5f, 1f);
                    panel.anchorMax = new Vector2(0.5f, 1f);
                    panel.pivot = new Vector2(0.5f, 1f);

                    visiblePosition = new Vector2(0f, 0f);
                    hiddenPosition = new Vector2(0f, panelHeight);
                    break;
            }
        }

        public void TogglePanel()
        {
            if (!isOpen)
                OpenPanel();
            else
                ClosePanel();
        }

        public void OpenPanel()
        {
            panel.SetAsLastSibling();
            panel.gameObject.SetActive(true);

            panel
                .DOAnchorPos(visiblePosition, moveDuration)
                .SetEase(Ease.OutQuad);

            isOpen = true;
        }

        public void ClosePanel()
        {
            panel
                .DOAnchorPos(hiddenPosition, moveDuration)
                .SetEase(Ease.InQuad)
                .OnComplete(() =>
                {
                    panel.SetSiblingIndex(originalSiblingIndex);
                });

            isOpen = false;
        }

        private void OnRectTransformDimensionsChange()
        {
            CalculatePositions();
        }
    }
}
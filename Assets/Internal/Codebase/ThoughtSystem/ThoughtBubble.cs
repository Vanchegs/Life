using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Codebase
{
    public class ThoughtBubble : MonoBehaviour
    {
        [SerializeField] private TMP_Text textComponent;
        [SerializeField] private RectTransform rect;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private Tween currentTween;
        
        public void ShowStatThought(string text, float duration, float startY, System.Action onComplete)
        {
            Show(text, duration, startY, onComplete);
        }
        
        public void ShowRandomThought(string text, float duration, float startY, System.Action onComplete)
        {
            Show(text, duration, startY, onComplete);
        }
        
        public void MoveToPosition(float yPos, float duration)
        {
            rect.DOAnchorPosY(yPos, duration).SetEase(Ease.OutQuad);
        }
        
        private void Show(string text, float duration, float startY, System.Action onComplete)
        {
            textComponent.text = text;
            gameObject.SetActive(true);
            
            currentTween?.Kill();
            
            // Сброс
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, startY - 30f);
            canvasGroup.alpha = 0f;
            
            Sequence seq = DOTween.Sequence();
            // Появление снизу
            seq.Append(rect.DOAnchorPosY(startY, 0.3f).SetEase(Ease.OutBack));
            seq.Join(canvasGroup.DOFade(1f, 0.2f));
            // Ожидание
            seq.AppendInterval(duration);
            // Исчезновение
            seq.Append(canvasGroup.DOFade(0f, 0.3f));
            
            seq.OnComplete(() =>
            {
                gameObject.SetActive(false);
                onComplete?.Invoke();
            });
            
            currentTween = seq;
        }
        
        public void HideImmediately()
        {
            currentTween?.Kill();
            gameObject.SetActive(false);
        }
        
        private void OnDestroy()
        {
            currentTween?.Kill();
        }
    }
}
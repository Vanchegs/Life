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
        private float startY;
        
        private void Awake()
        {
            startY = rect.anchoredPosition.y;
        }
        
        public void ShowStatThought(string text, float duration, System.Action onComplete)
        {
            Show(text, duration, onComplete, false);
        }
        
        public void ShowRandomThought(string text, float duration, System.Action onComplete)
        {
            Show(text, duration, onComplete, true);
        }
        
        private void Show(string text, float duration, System.Action onComplete, bool isRandom)
        {
            textComponent.text = text;
            gameObject.SetActive(true);
            
            currentTween?.Kill();
            
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, startY);
            canvasGroup.alpha = 1f;
            
            Sequence seq = DOTween.Sequence();
            seq.Append(rect.DOAnchorPosY(startY + 30, 0.3f).SetEase(Ease.OutBack));
            seq.Join(canvasGroup.DOFade(1f, 0.2f));
            
            seq.AppendInterval(duration);
            
            seq.Append(canvasGroup.DOFade(0f, 0.3f));
            seq.Join(rect.DOAnchorPosY(rect.anchoredPosition.y + 20, 0.3f));
            
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
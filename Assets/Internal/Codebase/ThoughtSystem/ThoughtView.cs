using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Codebase
{
    public class ThoughtView : MonoBehaviour
    {
        [SerializeField] private RectTransform thoughtPanel;
        [SerializeField] private TMP_Text thoughtText;
        
        private Vector2 startPosition;
        private Vector2 endPosition;
        
        private void Awake()
        {
            startPosition = new Vector2(-300, -500);
            endPosition = new Vector2(-800, -200);
            
            thoughtPanel.anchoredPosition = startPosition;
            thoughtPanel.gameObject.SetActive(false);
        }
        
        public void ShowThought(string text, float duration = 3f)
        {
            StartCoroutine(AnimateThought(text, duration));
        }
        
        private System.Collections.IEnumerator AnimateThought(string text, float duration)
        {
            thoughtText.text = text;
            thoughtPanel.gameObject.SetActive(true);
            thoughtPanel.anchoredPosition = startPosition;
            thoughtPanel.localScale = Vector3.one;
            
            // Поднятие наверх
            thoughtPanel.DOAnchorPosY(-150, 0.4f).SetEase(Ease.OutBack);
            
            yield return new WaitForSeconds(duration);
            
            // Уход влево и исчезновение
            Sequence hide = DOTween.Sequence();
            hide.Join(thoughtPanel.DOAnchorPosX(endPosition.x, 0.5f).SetEase(Ease.InBack));
            hide.Join(thoughtPanel.DOScale(0.8f, 0.5f));
            hide.OnComplete(() => thoughtPanel.gameObject.SetActive(false));
            hide.Play();
        }
    }
}

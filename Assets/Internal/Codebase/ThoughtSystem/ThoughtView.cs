using DG.Tweening;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace Codebase
{
    public class ThoughtView : MonoBehaviour
    {
        [SerializeField] private RectTransform statThoughtPanel;
        [SerializeField] private TMP_Text statThoughtText;
        
        [SerializeField] private RectTransform randomThoughtPanel;
        [SerializeField] private TMP_Text randomThoughtText;
        
        [SerializeField] private Vector2 defaultPosition = new Vector2(245, -100);
        [SerializeField] private Vector2 offsetPosition = new Vector2(245, -160);
        [SerializeField] private Vector2 endPosition = new Vector2(-800, -200);
        
        private Queue<ThoughtMessage> messageQueue = new Queue<ThoughtMessage>();
        private bool isShowing = false;
        private bool isStatPanelActive = false;
        private bool isRandomPanelActive = false;
        
        private enum ThoughtType
        {
            Stat,
            Random
        }
        
        private class ThoughtMessage
        {
            public string text;
            public float duration;
            public ThoughtType type;
            
            public ThoughtMessage(string text, float duration, ThoughtType type)
            {
                this.text = text;
                this.duration = duration;
                this.type = type;
            }
        }
        
        private void Awake()
        {
            statThoughtPanel.anchoredPosition = defaultPosition;
            randomThoughtPanel.anchoredPosition = defaultPosition;
            statThoughtPanel.gameObject.SetActive(false);
            randomThoughtPanel.gameObject.SetActive(false);
        }
        
        public void ShowStatThought(string text, float duration = 3f)
        {
            messageQueue.Enqueue(new ThoughtMessage(text, duration, ThoughtType.Stat));
            
            if (!isShowing)
                StartCoroutine(ProcessQueue());
        }
        
        public void ShowRandomThought(string text, float duration = 3f)
        {
            messageQueue.Enqueue(new ThoughtMessage(text, duration, ThoughtType.Random));
            
            if (!isShowing)
                StartCoroutine(ProcessQueue());
        }
        
        private System.Collections.IEnumerator ProcessQueue()
        {
            isShowing = true;
            
            while (messageQueue.Count > 0)
            {
                var message = messageQueue.Dequeue();
                
                // Проверяем, есть ли активная панель другого типа
                bool hasOtherPanel = (message.type == ThoughtType.Stat && isRandomPanelActive) ||
                                    (message.type == ThoughtType.Random && isStatPanelActive);
                
                Vector2 targetPosition = hasOtherPanel ? offsetPosition : defaultPosition;
                
                if (message.type == ThoughtType.Stat)
                {
                    yield return StartCoroutine(AnimateStatThought(message.text, targetPosition, message.duration));
                }
                else
                {
                    yield return StartCoroutine(AnimateRandomThought(message.text, targetPosition, message.duration));
                }
                
                yield return new WaitForSeconds(0.1f);
            }
            
            isShowing = false;
        }
        
        private System.Collections.IEnumerator AnimateStatThought(string text, Vector2 startPos, float duration)
        {
            isStatPanelActive = true;
            statThoughtText.text = text;
            statThoughtPanel.gameObject.SetActive(true);
            statThoughtPanel.anchoredPosition = startPos;
            statThoughtPanel.localScale = Vector3.one;
            
            // Поднятие наверх (Y = -20)
            statThoughtPanel.DOAnchorPosY(-20, 0.4f).SetEase(Ease.OutBack);
            
            yield return new WaitForSeconds(duration);
            
            // Уход влево
            Sequence hide = DOTween.Sequence();
            hide.Join(statThoughtPanel.DOAnchorPosX(endPosition.x, 0.5f).SetEase(Ease.InBack));
            hide.OnComplete(() => {
                statThoughtPanel.gameObject.SetActive(false);
                isStatPanelActive = false;
            });
            hide.Play();
        }
        
        private System.Collections.IEnumerator AnimateRandomThought(string text, Vector2 startPos, float duration)
        {
            isRandomPanelActive = true;
            randomThoughtText.text = text;
            randomThoughtPanel.gameObject.SetActive(true);
            randomThoughtPanel.anchoredPosition = startPos;
            randomThoughtPanel.localScale = Vector3.one;
            
            // Поднятие наверх (Y = -20)
            randomThoughtPanel.DOAnchorPosY(-20, 0.4f).SetEase(Ease.OutBack);
            
            yield return new WaitForSeconds(duration);
            
            // Уход влево
            Sequence hide = DOTween.Sequence();
            hide.Join(randomThoughtPanel.DOAnchorPosX(endPosition.x, 0.5f).SetEase(Ease.InBack));
            hide.OnComplete(() => {
                randomThoughtPanel.gameObject.SetActive(false);
                isRandomPanelActive = false;
            });
            hide.Play();
        }
        
        public void ClearQueue()
        {
            messageQueue.Clear();
            isShowing = false;
            StopAllCoroutines();
            
            statThoughtPanel.gameObject.SetActive(false);
            randomThoughtPanel.gameObject.SetActive(false);
            isStatPanelActive = false;
            isRandomPanelActive = false;
        }
    }
}
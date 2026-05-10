using System.Collections;
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
        
        [Header("Позиции")]
        [SerializeField] private Vector2 defaultPosition = new(245, -100);
        [SerializeField] private Vector2 upperPosition = new(245, -160);
        [SerializeField] private Vector2 endPosition = new(-800, -200);
        [SerializeField] private float targetY = -20;
        
        private Queue<ThoughtMessage> messageQueue = new();
        private bool isProcessing;
        
        private RectTransform currentActivePanel;
        private bool isStatPanelVisible;
        private bool isRandomPanelVisible;
        
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
            
            if (!isProcessing)
                StartCoroutine(ProcessQueue());
        }
        
        public void ShowRandomThought(string text, float duration = 3f)
        {
            messageQueue.Enqueue(new ThoughtMessage(text, duration, ThoughtType.Random));
            
            if (!isProcessing)
                StartCoroutine(ProcessQueue());
        }
        
        private IEnumerator ProcessQueue()
        {
            isProcessing = true;
            
            while (messageQueue.Count > 0)
            {
                var message = messageQueue.Dequeue();
                
                // Проверяем, есть ли видимая панель другого типа
                bool hasVisiblePanel = (message.type == ThoughtType.Stat && isRandomPanelVisible) ||
                                      (message.type == ThoughtType.Random && isStatPanelVisible);
                
                if (hasVisiblePanel)
                {
                    // Находим старую панель и поднимаем её
                    RectTransform oldPanel = message.type == ThoughtType.Stat ? randomThoughtPanel : statThoughtPanel;
                    oldPanel.DOAnchorPosY(upperPosition.y, 0.3f).SetEase(Ease.OutQuad);
                }
                
                // Показываем новую панель
                if (message.type == ThoughtType.Stat)
                {
                    yield return StartCoroutine(AnimateStatThought(message.text, message.duration));
                }
                else
                {
                    yield return StartCoroutine(AnimateRandomThought(message.text, message.duration));
                }
                
                yield return new WaitForSeconds(0.1f);
            }
            
            isProcessing = false;
        }
        
        private IEnumerator AnimateStatThought(string text, float duration)
        {
            // Останавливаем все текущие анимации этой панели
            statThoughtPanel.DOKill();
            
            // Устанавливаем текст
            statThoughtText.text = text;
            
            // Сбрасываем позицию
            statThoughtPanel.anchoredPosition = defaultPosition;
            statThoughtPanel.localScale = Vector3.one;
            
            // Показываем панель
            statThoughtPanel.gameObject.SetActive(true);
            isStatPanelVisible = true;
            currentActivePanel = statThoughtPanel;
            
            // Анимация появления
            statThoughtPanel.DOAnchorPosY(targetY, 0.4f).SetEase(Ease.OutBack);
            
            // Ждём указанное время
            yield return new WaitForSeconds(duration);
            
            // Анимация ухода
            statThoughtPanel.DOAnchorPosX(endPosition.x, 0.5f).SetEase(Ease.InBack);
            
            // Ждём половину анимации ухода, чтобы скрыть панель
            yield return new WaitForSeconds(0.3f);
            
            // Скрываем панель
            statThoughtPanel.gameObject.SetActive(false);
            isStatPanelVisible = false;
            
            // Сбрасываем позицию для следующего раза
            statThoughtPanel.anchoredPosition = defaultPosition;
            
            if (currentActivePanel == statThoughtPanel)
                currentActivePanel = null;
        }
        
        private IEnumerator AnimateRandomThought(string text, float duration)
        {
            // Останавливаем все текущие анимации этой панели
            randomThoughtPanel.DOKill();
            
            // Устанавливаем текст
            randomThoughtText.text = text;
            
            // Сбрасываем позицию
            randomThoughtPanel.anchoredPosition = defaultPosition;
            randomThoughtPanel.localScale = Vector3.one;
            
            // Показываем панель
            randomThoughtPanel.gameObject.SetActive(true);
            isRandomPanelVisible = true;
            currentActivePanel = randomThoughtPanel;
            
            // Анимация появления
            randomThoughtPanel.DOAnchorPosY(targetY, 0.4f).SetEase(Ease.OutBack);
            
            // Ждём указанное время
            yield return new WaitForSeconds(duration);
            
            // Анимация ухода
            randomThoughtPanel.DOAnchorPosX(endPosition.x, 0.5f).SetEase(Ease.InBack);
            
            // Ждём половину анимации ухода
            yield return new WaitForSeconds(0.3f);
            
            // Скрываем панель
            randomThoughtPanel.gameObject.SetActive(false);
            isRandomPanelVisible = false;
            
            // Сбрасываем позицию для следующего раза
            randomThoughtPanel.anchoredPosition = defaultPosition;
            
            if (currentActivePanel == randomThoughtPanel)
                currentActivePanel = null;
        }
        
        public void ClearQueue()
        {
            messageQueue.Clear();
            isProcessing = false;
            StopAllCoroutines();
            
            statThoughtPanel.DOKill();
            randomThoughtPanel.DOKill();
            
            statThoughtPanel.gameObject.SetActive(false);
            randomThoughtPanel.gameObject.SetActive(false);
            
            isStatPanelVisible = false;
            isRandomPanelVisible = false;
            currentActivePanel = null;
        }
        
        private void OnDestroy()
        {
            ClearQueue();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase
{
    public class ThoughtView : MonoBehaviour
    {
        [SerializeField] private RectTransform container;
        [SerializeField] private ThoughtBubble thoughtPrefab;
        [SerializeField] private float bubbleHeight = 70f;
        [SerializeField] private float spacing = 10f;
        [SerializeField] private int maxVisible = 5;
        
        private List<ThoughtBubble> activeBubbles = new();
        private Queue<ThoughtBubble> pool = new();
        private bool isShowing;
        
        private void Start()
        {
            for (int i = 0; i < maxVisible; i++)
            {
                var bubble = Instantiate(thoughtPrefab, container);
                bubble.gameObject.SetActive(false);
                pool.Enqueue(bubble);
            }
        }
        
        public void ShowStatThought(string text, float duration = 5f)
        {
            StartCoroutine(ShowBubble(text, duration, true));
        }
        
        public void ShowRandomThought(string text, float duration = 5f)
        {
            StartCoroutine(ShowBubble(text, duration, false));
        }
        
        private IEnumerator ShowBubble(string text, float duration, bool isStat)
        {
            while (isShowing)
                yield return null;
    
            isShowing = true;
    
            if (activeBubbles.Count >= maxVisible)
            {
                var oldest = activeBubbles[0];
                activeBubbles.RemoveAt(0);
                oldest.HideImmediately();
                ReturnToPool(oldest);
            }
    
            var bubble = pool.Dequeue();
    
            activeBubbles.Add(bubble);
    
            for (int i = 0; i < activeBubbles.Count; i++)
            {
                float yPos = (activeBubbles.Count - 1 - i) * (bubbleHeight + spacing);
                activeBubbles[i].MoveToPosition(yPos, 0.3f);
            }
    
            float startY = 0;
            if (isStat)
                bubble.ShowStatThought(text, duration, startY, () => OnBubbleComplete(bubble));
            else
                bubble.ShowRandomThought(text, duration, startY, () => OnBubbleComplete(bubble));
    
            isShowing = false;
        }
        
        private void OnBubbleComplete(ThoughtBubble bubble)
        {
            int index = activeBubbles.IndexOf(bubble);
            if (index >= 0)
            {
                activeBubbles.RemoveAt(index);
                ReturnToPool(bubble);
                
                // Пересчитываем позиции оставшихся пузырьков
                for (int i = 0; i < activeBubbles.Count; i++)
                {
                    float yPos = i * (bubbleHeight + spacing);
                    activeBubbles[i].MoveToPosition(yPos, 0.3f);
                }
            }
        }
        
        private void ReturnToPool(ThoughtBubble bubble)
        {
            bubble.gameObject.SetActive(false);
            pool.Enqueue(bubble);
        }
        
        public void ClearAll()
        {
            StopAllCoroutines();
            foreach (var bubble in activeBubbles)
            {
                bubble.HideImmediately();
                ReturnToPool(bubble);
            }
            activeBubbles.Clear();
            isShowing = false;
        }
    }
}
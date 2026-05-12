using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Codebase
{
    public class ThoughtView : MonoBehaviour
    {
        [SerializeField] private RectTransform container;
        [SerializeField] private ThoughtBubble thoughtPrefab;
        [SerializeField] private float hideDelay = 5f;
        [SerializeField] private int maxVisible = 5;
        
        private Queue<ThoughtBubble> activeBubbles = new();
        private Queue<ThoughtBubble> pool = new();
        
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
            var bubble = GetBubble();
            bubble.ShowStatThought(text, duration, () => ReturnToPool(bubble));
        }
        
        public void ShowRandomThought(string text, float duration = 5f)
        {
            var bubble = GetBubble();
            bubble.ShowRandomThought(text, duration, () => ReturnToPool(bubble));
        }
        
        private ThoughtBubble GetBubble()
        {
            if (activeBubbles.Count >= maxVisible)
            {
                var oldest = activeBubbles.Dequeue();
                oldest.HideImmediately();
                ReturnToPool(oldest);
            }
            
            var bubble = pool.Dequeue();
            activeBubbles.Enqueue(bubble);
            return bubble;
        }
        
        private void ReturnToPool(ThoughtBubble bubble)
        {
            bubble.gameObject.SetActive(false);
            pool.Enqueue(bubble);
            activeBubbles = new Queue<ThoughtBubble>(activeBubbles.Where(b => b != bubble));
        }
    }
    
}
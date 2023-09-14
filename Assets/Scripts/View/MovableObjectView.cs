using System;
using Controller;
using UnityEngine;
using View.Interface;

namespace View
{
    public class OnClickedEventArgs : EventArgs
    {
        public bool IsClicked { get; set; }
    }
    
    public class MovableObjectView: MonoBehaviour 
    {
        [SerializeField] public SpriteRenderer spriteRenderer;
        public Color DefaultColor { get; set; }
        public int health;
        public event EventHandler<OnClickedEventArgs> OnClicked = (sender, e) => { };
        public bool isTriggered { get; set; }

        public event EventHandler<TriggeredEventArg> OnTriggeredEvent = (sender, e) => { };
        public event EventHandler<MouseMoveEventArg> OnMouseDragEvent = (sender, e) => { };
        
        public int Health
        {
            get => health;
            set => health = value;
            //SoldierBarrack bir healli clası olmalı
            //transform.GetComponent<EnemyObjectClass>()._enemyHealth = soldierBarrackHealth;
        }
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            DefaultColor = spriteRenderer.color;
        }
        
        private void Update()
        {
            if (health <= 0) Destroy(gameObject);
        }

        private void OnMouseDown()
        {
            var onClickedEvent = new OnClickedEventArgs
            {
                IsClicked = true
            };
            OnClicked(this, onClickedEvent);
        }
        
        private void OnMouseUp()
        {
            var onClickedEvent = new OnClickedEventArgs
            {
                IsClicked = false
            };
            OnClicked(this, onClickedEvent);
        }

        private void OnMouseDrag()
        {
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            var point = new Pointer3D((int)curPosition.x + .5f, (int)curPosition.y + .5f, -.1f);
            var mouseMoveEventArg = new MouseMoveEventArg
            {
                point = point
            };
            OnMouseDragEvent(this, mouseMoveEventArg);
        }
        
        
        private void setObjectPosition(Pointer3D position) {
            transform.position = new Vector3((int)position.x + .5f, (int)position.y + .5f, -.1f);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            isTriggered = true;
            var eventArgs = new TriggeredEventArg
            {
                IsTriggered = isTriggered
            };
            OnTriggeredEvent(this, eventArgs);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            isTriggered = false;
            var eventArgs = new TriggeredEventArg
            {
                IsTriggered = isTriggered
            };
            OnTriggeredEvent(this, eventArgs);
        }
        
        public void SetToDefaultColor()
        {
            spriteRenderer.color = DefaultColor;
        }

        public void ChangeColor(Color color)
        {
            spriteRenderer.color = color;
        }
        
    }
}
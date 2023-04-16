using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using Assets.Scripts.Controller.Factory;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Assets.Scripts.View
{
    

    public class PowerPlantClickedEventArgs : EventArgs
    {
    }
    public interface IPowerPlantView
    {
        event EventHandler<SoldierBarrackClickedEventArgs> OnClicked;
        Vector3 Position { get; set; }
        int Health { get; set; }
        void OnClick();
    }

    public class PowerPlantView : MonoBehaviour, IPowerPlantView
    {
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] public Color _spriteRendererStartColor;

        [SerializeField] public bool isTrigger;
        public int index = 0;
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRendererStartColor = spriteRenderer.color;
        }
        public event EventHandler<SoldierBarrackClickedEventArgs> OnClicked = (sender, e) => { };

        public Vector3 position;
        public int powerPlantHealth;

        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;
                transform.position = value;
            }
        }

        public int Health
        {
            get => powerPlantHealth;
            set
            {
                powerPlantHealth = value;
                //SoldierBarrack bir healli clası olmalı
                //transform.GetComponent<EnemyObjectClass>()._enemyHealth = soldierBarrackHealth;
            }
        }


        private void Update()
        {
            if (powerPlantHealth <= 0) Destroy(gameObject);
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(
                    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5)),
                    Vector2.zero);
                if (hit.transform.gameObject == gameObject)
                {
                    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5);
                    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                    transform.position = new Vector3((int)(curPosition.x) + .5f, (int)(curPosition.y) + .5f, -.1f);
                    var eventArgs = new SoldierBarrackClickedEventArgs();
                    OnClicked(this, eventArgs);
                }
            }
        }
        

        // Enemy soldierla carpisti mi
        private void OnTriggerStay2D(Collider2D other)
        { 
            isTrigger = true; 
            spriteRenderer.color = new Color(212f / 255f, 49f / 255f, 49f / 255f, 120f / 255f);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            isTrigger = false;
            spriteRenderer.color = _spriteRendererStartColor;
        }

        public void OnClick()
        {
            UIManager.instance.OpenPowerPlantPanel();
        }

    }
}
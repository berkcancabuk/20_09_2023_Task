using System.Collections;
using UnityEngine;
using System;
using Controller;
using View.Interface;

namespace Assets.Scripts.View
{
    public class EnemyAttackEventArgs : EventArgs
    {
        public Pointer3D SoldierPosition { get; set; }

        public EnemyAttackEventArgs(Pointer3D soldierPosition)
        {
            SoldierPosition = soldierPosition;
        }
    }

    public class EnemyClickedEventArgs : EventArgs
    {
    }

    public interface IEnemyView
    {
        event EventHandler<EnemyAttackEventArgs> OnAttack;
        event EventHandler<EnemyClickedEventArgs> OnClicked;
        Vector3 Position { get; set; }
        int Health { get; set; }
        void OnClick();
    }

    public class EnemyView : MonoBehaviour, IEnemyView
    {
        [SerializeField] private TileListClass tileListClass;

        public event EventHandler<EnemyAttackEventArgs> OnAttack = (sender, e) => { };
        public event EventHandler<EnemyClickedEventArgs> OnClicked = (sender, e) => { };

        public Vector3 position;
        public int enemyHealth;
        public int enemyAttack;

        private bool _isExitTrigger = false;

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
            get => enemyHealth;
            set
            {
                enemyHealth = value;
                transform.GetComponent<EnemyObjectClass>()._enemyHealth = enemyHealth;
            }
        }


        private void Update()
        {
            if (enemyHealth <= 0) Destroy(gameObject);
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(
                    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5)),
                    Vector2.zero);
                if (hit.transform.gameObject == gameObject)
                {
                    var eventArgs = new EnemyClickedEventArgs();
                    OnClicked(this, eventArgs);
                }
            }
        }

        /// <summary>
        /// IEnumerator that enables the soldier to attack the enemy every 3 seconds.
        /// </summary>
        /// <returns></returns>
        public IEnumerator EnemyAttack(EnemyAttackEventArgs enemyAttackEventArgs)
        {
            OnAttack(this, enemyAttackEventArgs);
            if (_isExitTrigger) yield break;
            yield return new WaitForSeconds(3);
            StartCoroutine(EnemyAttack(enemyAttackEventArgs));
        }

        // Enemy soldierla carpisti mi
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Soldier"))
            {
                var soldier = collision.transform.gameObject;
                var eventArgs = new EnemyAttackEventArgs(Pointer3D.ConvertVectorToPointer3D(soldier.transform.position));

                StartCoroutine(EnemyAttack(eventArgs));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Soldier")) _isExitTrigger = true;
        }

        public void OnClick()
        {
            UIManager.instance.OpenEnemyPanel(transform.gameObject);
        }
    }
}
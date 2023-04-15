using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using Assets.Scripts.Controller.Factory;

namespace Assets.Scripts.View
{
    
    public class SoldierClickedEventArgs : EventArgs
    {
    }

    public class SoldierAttackEventArgs : EventArgs
    {
        public Vector3? _enemyPosition { get; set; }
        public SoldierAttackEventArgs(Vector2 EnemyPosition)
        {
            _enemyPosition = EnemyPosition;
        }
        
    }

    public interface ISoldierView
    {
        event EventHandler<SoldierClickedEventArgs> OnClicked;
        event EventHandler<SoldierAttackEventArgs> OnAttack;

        Vector3 Position { set; }
        int Health { get; set; }

        void OnClick();
    }

    public class SoldierView : MonoBehaviour, ISoldierView
    {

        private GameObject _clickLastSoldier;
        [SerializeField] TileListClass tileListClass;
        private UIManager uIManager;
        public event EventHandler<SoldierClickedEventArgs> OnClicked = (sender, e) => { };
        public event EventHandler<SoldierAttackEventArgs> OnAttack = (sender, e) => { };
        public int soldierHealth;
        public int soldierPower;
        public int soldierLevel;
        public int soldierCount;
        [SerializeField] private GameObject _enemy;
        bool _isExitTrigger = false;
        public Vector3 Position { set { transform.position = value; } }

        public int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void Update()
        {
            if (soldierHealth <= 0)
            {
                Destroy(gameObject);
            }
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5)), Vector2.zero);
                if (hit.transform.gameObject == this.gameObject)
                {
                    var eventArgs = new SoldierClickedEventArgs();
                    OnClicked(this, eventArgs);
                }
            }
        }
        
        /// <summary>
        /// IEnumerator that enables the soldier to attack the enemy every 3 seconds.
        /// </summary>
        /// <returns></returns>
        public IEnumerator SoldierAttack(SoldierAttackEventArgs _SoldierAttackEventArgs)
        {
            OnAttack(this, _SoldierAttackEventArgs);
            // soldierHealth -= _enemy.GetComponent<EnemyObjectClass>()._enemyAttack;
            if (_isExitTrigger)
            {
                yield break;
            }
            yield return new WaitForSeconds(3);
            StartCoroutine(SoldierAttack(_SoldierAttackEventArgs));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                _enemy = collision.transform.gameObject;
                var eventArgs = new SoldierAttackEventArgs(_enemy.transform.position);
               
                StartCoroutine(SoldierAttack(eventArgs));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                _isExitTrigger = true;
            }

        }

        public void ClickedGO()
        {
            if (_clickLastSoldier != null)
            {
                _clickLastSoldier.GetComponent<BoxCollider2D>().enabled = true;
                _clickLastSoldier.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = ColorFactory.GetColor(ColorType.NotSelected);
                _clickLastSoldier = null;
                TileColliderOn();
            }
            if (transform.tag == "Soldier")
            {
                if (_clickLastSoldier == null)
                {
                    transform.GetChild(0).GetChild(0).GetComponent<Image>().color = ColorFactory.GetColor(ColorType.Selected);
                    //uIManager.OpenSoldiersPanel(GO.transform.gameObject);
                    transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    _clickLastSoldier = transform.gameObject;
                    TileColliderClose();
                }
            }
        }

        /// <summary>
        /// Disables the colliders on the tiles.
        /// </summary>
        public void TileColliderClose()
        {
            for (int i = 0; i < tileListClass.tiles.Count; i++)
            {
                tileListClass.tiles[i].GetComponent<BoxCollider2D>().enabled = false;
            }

        }

        /// <summary>
        /// Enabled the colliders on the tiles.
        /// </summary>
        public void TileColliderOn()
        {
            for (int i = 0; i < tileListClass.tiles.Count; i++)
            {
                tileListClass.tiles[i].GetComponent<BoxCollider2D>().enabled = true;
            }

        }

        public void OnClick()
        {
            ClickedGO();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Power
{
    ATTACK,
    DEFENSE
}

namespace Tamarin_Battle
{
    public class Card : MonoBehaviour
    {
        private bool clicked = true;
        public Power type { get; private set; }
        private float power_value;
        private GameScript manager;
        [HideInInspector]
        public Player current,next;
        private void Awake()
        {
            power_value = Random.Range(20, 31);
            type = (Power)(Random.Range(0, 2));
            manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameScript>();
            
        }

        private void Start()
        {
            StartCoroutine("clickable");
        }

        IEnumerator clickable()
        {
            yield return new WaitForSeconds(1.5f);
            clicked = false;
        }


        void OnMouseDown()
        {
            if (!clicked)
            {
                clicked = true;
                switch (type)
                {
                    case Power.ATTACK:
                        current.damage = power_value;
                        break;
                    case Power.DEFENSE:
                        current.shield = power_value;
                        break;
                }
                current.power_stat.text = power_value + "";
                print($"{GameScript.turn} {current.gameObject.tag} {type} {next.gameObject.tag} {power_value}");


                GameScript.turn++;
                if(GameScript.turn % 2 == 1)
                {
                    manager.make_deck();
                }
                if (GameScript.turn > 1 && (GameScript.turn % 2 == 0))
                { 
                    manager.StartCoroutine("next_round");
                }
            }

        }
    }
}

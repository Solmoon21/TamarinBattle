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
        private bool clicked = false;
        public Power type { get; private set; }
        private float power_value;
        private GameScript manager;
        private Player current,next;
        private void Awake()
        {
            power_value = Random.Range(20, 31);
            type = (Power)(Random.Range(0, 2));
            manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameScript>();
            
        }

        private void Start()
        {
            
        }


        void OnMouseDown()
        {
            if (!clicked)
            {
                clicked = true;
                if(GameScript.turn % 2 == 0)
            {
                    current = manager.player;
                    next = manager.enemy;
                }
            else
                {
                    current = manager.enemy;
                    next = manager.player;
                }
                switch (type)
                {
                    case Power.ATTACK:
                        current.damage = power_value;
                        break;
                    case Power.DEFENSE:
                        current.shield = power_value;

                        break;
                }
                current.shield_stat.text = power_value + "";
                print($"{GameScript.turn} {current.gameObject.tag} {type} {next.gameObject.tag} {power_value}");


                GameScript.turn++;
                manager.make_deck();
                if (GameScript.turn > 1 && (GameScript.turn % 2 == 0))
                {
                    manager.change_stat();
                    manager.StartCoroutine("next_round");
                }
            }

        }
    }
}

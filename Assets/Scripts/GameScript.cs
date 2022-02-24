using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamarin_Battle
{
    public class GameScript : MonoBehaviour
    {
        private string[] names = { "Player", "Opponent" };
        public Text turn_show,type1,type2;
        public static int turn = 0;
        public Player player,enemy;
        public GameObject card;
        public GameObject cardhold1, cardhold2;
        public Image Over;

        //private GameObject c1, c2;

        private void Start()
        {
            change_stat();
            make_deck();
        }

        private void Update()
        {
            if(player.health<=0f || enemy.health <= 0f)
            {
                clean(cardhold1);
                clean(cardhold2);
                Over.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        void clean(GameObject g)
        {
           

            if (g.transform.childCount > 0)
            {

                foreach (Transform child in g.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        public IEnumerator next_round()
        {
            yield return new WaitForSeconds(3f);
            stat_reset();
            change_stat();
        }

        public void stat_reset()
        {
            player.damage = 0f;
            enemy.damage = 0f;
            player.shield = 0f;
            enemy.shield = 0f;
        }

        public void change_stat()
        {
            player.damage -= enemy.shield;
            if(player.damage<0) { player.damage = 0f; }
            enemy.damage -= player.shield;
            if(enemy.damage<0) { enemy.damage = 0f; }

            player.health -= enemy.damage;
            enemy.health -= player.damage;


            
            player.health_stat.value = player.health;
            player.shield_stat.text = player.shield + "";
            enemy.health_stat.value = enemy.health;
            enemy.shield_stat.text = enemy.shield + "";

            
        }

        public void make_deck() {
            turn_show.text = names[turn % 2];
            clean(cardhold1);
            clean(cardhold2);
            GameObject c1 =  Instantiate(card, cardhold1.transform);
            GameObject c2 =  Instantiate(card, cardhold2.transform);
            type1.text = c1.GetComponentInChildren<Card>().type + "";
            type2.text = c2.GetComponentInChildren<Card>().type + "";
        }
    }
}
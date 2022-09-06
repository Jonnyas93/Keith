using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClickChicken {
    public class chickenMovement : MonoBehaviour
    {
        [SerializeField] private int lives;
        [SerializeField] private float btmRange;
        [SerializeField] private float topRange;
        [SerializeField] private float directionFrequency;

        private Animator anim;
        private SpriteRenderer sprite;

        [SerializeField] private bool win = false;
        [SerializeField] private bool hitStun = false;

        [SerializeField] private float x;
        [SerializeField] private float y;
        [SerializeField] private float blinkTimer = 0;
        [SerializeField] private float blinkLength = 3f;

        void Start()
        {
            x = 0f;
            y = 0f;
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
            InvokeRepeating("DirectionModifier", 0, directionFrequency);
            StartCoroutine(Movement());
        }

        void Update()
        {

        }

        IEnumerator Movement()
        {
            while (win == false)
            {
                transform.Translate(x * Time.deltaTime, y * Time.deltaTime, 0);
            }

            yield return null;
        }

        private void DirectionModifier()
        {
            x = Random.Range(btmRange, topRange);
            y = Random.Range(btmRange, topRange);
        }

        public void Hit()
        {
            if (hitStun == false)
            {
                lives--;

                if (lives > 0) { StartCoroutine(DamageRecieved()); }    // prevent damage flash when turned into egg
                
            }
            
            if (lives == 0)
            {
                anim.Play("hit");
                win = true;
            }
        }
        IEnumerator DamageRecieved()
        {
            blinkTimer = 0;
            Color color = sprite.color;
            hitStun = true;

            while (blinkTimer != blinkLength)
            {
                blinkTimer++;

                color.a = 0.3f;
                sprite.color = color;
                yield return new WaitForSeconds(0.1f);

                color.a = 1f;
                sprite.color = color;
                yield return new WaitForSeconds(0.1f);

                yield return null;
            }

            hitStun = false;
        }

        public int GetLives()
        {
            return lives;
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargedSpatula {
    public class csEnemy : MonoBehaviour
    {
        private bool defeated = false;
        private Vector3 direction;
        public Animator anim;

        void Start()
        {
            direction = new Vector3(Random.Range(0.06f, 0.1f), Random.Range(0.06f, 0.1f), 0);
        }

        void FixedUpdate()
        {
            if (defeated == true)
            {
                anim.Play("chicken_flung");
            }
        }

        public void defeatedStateChange()
        {
            defeated = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Combat;
using Overworld;
using UnityEngine.Analytics;
using Unity.Services.Analytics;

namespace DucksOnTheRoad {
    public class DuckRoadController : Minigame
    {
        private float[] yPositions = {-1f, -2.5f, -3.4f};
        private SceneTransitionScript SceneTransition; 
        public GameObject duckPrefab;
        public float MinimumSpawnDelay;
        public float MaximumSpawnDelay;
        public float minigameDuration;
        
        // Start is called before the first frame update
        void Start()
        {   
            SceneTransition = gameObject.GetComponent<SceneTransitionScript>();
            StartCoroutine(spawnDuck());
            StartCoroutine(endGame(minigameDuration));
            AnalyticsService.Instance.CustomData("DOTR", new Dictionary<string, object>());
        }

        // Update is called once per frame
        void Update()
        {
            
            var lane = Random.Range(0, 10000);
            if (lane < 10) spawnDuck();
        }

        IEnumerator spawnDuck(float waitTime = 0, int previousDuckLane = -1) {

            yield return new WaitForSeconds(waitTime);

            float nextDuckDelay = Random.Range(MinimumSpawnDelay, MaximumSpawnDelay);
            int lane; 
            do {
                lane = Random.Range(0, 3);
            } while (lane == previousDuckLane);


            var duck = Instantiate(duckPrefab, new Vector3(11.5f, yPositions[lane], 0), Quaternion.identity);
            var duckRenderer = duck.GetComponent<SpriteRenderer>();
            duckRenderer.sortingLayerID = SortingLayer.NameToID("Player");
            duckRenderer.sortingOrder = lane;

            StartCoroutine(spawnDuck(nextDuckDelay, lane));
        }

        IEnumerator endGame(float waitTime = 0)
        {
            yield return new WaitForSeconds(waitTime);
            SceneTransition.changeScene();
        }
    }
}
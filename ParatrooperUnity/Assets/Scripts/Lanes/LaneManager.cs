using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lanes
{
    public class LaneManager : MonoBehaviour
    {
        [SerializeField]
        private FlyHorizontal[] planePrefabs;

        [SerializeField]
        private float spawnDelay = 2.0f;

        [SerializeField]
        private bool autoSpawn = true;

        [SerializeField]
        private float doubleSpawnChance = 0.0f;

        private float delay;
        private Transform spawnLeft;
        private Transform spawnRight;
        private List<Transform> planes;

        public void SpawnLeft()
        {
            var plane = this.Spawn(this.spawnLeft);
            plane.FlyRight();
        }

        public void SpawnRight()
        {
            var plane = this.Spawn(this.spawnRight);
            plane.FlyLeft();
        }

        IEnumerator SpawnMultiple(int count, bool left)
        {
            this.SpawnOnSide(left);
            count--;

            float delay = this.spawnDelay / 2.0f;
            while(delay > 0.0f && count > 0)
            {
                delay -= Time.deltaTime;

                if(delay < 0.0f)
                {
                    this.SpawnOnSide(left);
                    count--;
                    delay = this.spawnDelay / 2.0f;
                }

                yield return null;
            }
        }

        private void SpawnOnSide(bool left)
        {
            if(left)
            {
                this.SpawnLeft();
            }
            else
            {
                this.SpawnRight();
            }
        }

        void Awake()
        {
            this.ResetDelay();
            
            this.spawnLeft = this.transform.Find("SpawnLeft");
            this.spawnRight = this.transform.Find("SpawnRight");
            this.planes = new List<Transform>(2);
        }

        void Update()
        {
            if(!this.autoSpawn) return;

            this.delay -= Time.deltaTime;

            if(this.planes.Count == 0 && this.delay < 0)
            {
                bool left = Random.value < 0.5f ? true : false;
                int spawnCount = 1 + (Random.value < this.doubleSpawnChance ? 1 : 0);
                StartCoroutine(SpawnMultiple(spawnCount, left));
            }
        }

        private FlyHorizontal Spawn(Transform spawn)
        {
            this.ResetDelay();

            var planeToSpawn = this.planePrefabs[Random.Range(0, this.planePrefabs.Length)];

            return PoolingFactory.SpawnOrRecycle<FlyHorizontal>(planeToSpawn.transform, spawn.position);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var plane = other.GetComponent<FlyHorizontal>();
            if(plane != null)
            {
                this.planes.Add(other.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var plane = other.GetComponent<FlyHorizontal>();
            if(plane != null)
            {
                this.ResetDelay();
                
                this.planes.Remove(other.transform);
                var pooled = other.transform.GetComponent<Pooled>();
                if(pooled != null)
                {
                    pooled.DestroyPooled();
                }
            }
        }
        
        private void ResetDelay()
        {
            this.delay = this.spawnDelay;
        }
    }
}

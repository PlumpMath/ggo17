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

        void Awake()
        {
            this.delay = this.spawnDelay;
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
                if(Random.value < 0.5f)
                {
                    this.SpawnLeft();
                }
                else
                {
                    this.SpawnRight();
                }
            }
        }

        private FlyHorizontal Spawn(Transform spawn)
        {
            this.delay = this.spawnDelay;

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
                this.planes.Remove(other.transform);
                var pooled = other.transform.GetComponent<Pooled>();
                if(pooled != null)
                {
                    pooled.DestroyPooled();
                }
            }
        }
    }
}

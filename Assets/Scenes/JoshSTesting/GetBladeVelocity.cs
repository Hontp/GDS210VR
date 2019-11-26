﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiCutter
{
    public class GetBladeVelocity : MonoBehaviour
    {
        Vector3 startPoint, endPoint;
        bool startCounting;
        float timeTravelled;
        public Vector3 minimumVelocity;
        private void Start()
        {
            startCounting = false;
            minimumVelocity = new Vector3(0.5f, 0.5f, 0.5f);
        }

        private void Update()
        {
            if (startCounting)
            {
                timeTravelled += Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                startPoint = this.transform.position;
                startCounting = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                endPoint = this.transform.position;
                startCounting = false;

                Vector3 compareDistance = startPoint - endPoint;
                Vector3 comparedDistance = new Vector3(compareDistance.x / timeTravelled, compareDistance.y / timeTravelled, compareDistance.z / timeTravelled);
                if (comparedDistance.x > minimumVelocity.x && comparedDistance.y > minimumVelocity.y && comparedDistance.z > minimumVelocity.z)
                {
                    other.GetComponent<GettingHit>().Hit(this.transform);
                }
            }
        }

    }
}


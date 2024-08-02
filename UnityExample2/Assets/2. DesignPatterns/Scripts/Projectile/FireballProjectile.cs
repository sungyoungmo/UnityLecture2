using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject{
    public class FireballProjectile : MonoBehaviour
    {
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void SetProjectile(float speed)
        {
            rb.velocity = Vector3.forward * speed;
        }

    }
}

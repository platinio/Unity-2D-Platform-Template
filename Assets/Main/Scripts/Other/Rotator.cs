using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamaga
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 axis = Vector3.zero;
        [SerializeField] private float speed = 0.0f;

        private void Update()
        {
            transform.Rotate(axis , speed * Time.deltaTime);
        }
    }

}

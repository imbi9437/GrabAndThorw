using System;
using System.Collections.Generic;
using _Project.Script.Manager;
using UnityEngine;

namespace _Project.Script.Editor
{
    public class WayPointDrawer : MonoBehaviour
    {
        [SerializeField] private Transform StartPoint;
        [SerializeField] private Transform EndPoint;
        [SerializeField] private Transform MergePoint;
        [SerializeField] private List<Transform> point;


        private void Start()
        {
            DefenceManager.Instance.SetPoints(StartPoint, EndPoint, MergePoint, point);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            if (point.Count <= 0)
            {
                Gizmos.DrawLine(StartPoint.position, EndPoint.position);
            }
            else
            {
                Gizmos.DrawLine(StartPoint.position, point[0].position);

                for (var i = 1; i < point.Count; i++)
                {
                    Gizmos.DrawLine(point[i].position, point[i - 1].position);
                }
                
                Gizmos.DrawLine(EndPoint.position, point[^1].position);
            }
        }

        private void Reset()
        {
            point = new List<Transform>();
            
            StartPoint = transform.Find("StartPoint");
            EndPoint = transform.Find("EndPoint");
            MergePoint = transform.Find("MergePoint");

            var parent = transform.Find("WayPoints");

            foreach (Transform child in parent)
            {
                point.Add(child);
            }
        }
    }
}
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Script.Extensions
{
    public static class NavMeshExtensions
    {
        public static readonly int NormalId = NavMesh.GetAreaFromName("NormalArea");
        public static readonly int HeroId = NavMesh.GetAreaFromName("HeroArea");
        public static readonly int MonsterId = NavMesh.GetAreaFromName("MonsterArea");
        
        public static readonly int NormalLayer = 1 << NormalId;
        public static readonly int HeroLayer = 1 << HeroId;
        public static readonly int MonsterLayer = 1 << MonsterId;
        
        public static bool IsCompleteMove(this NavMeshAgent agent) => agent.remainingDistance <= agent.stoppingDistance;
        public static bool IsCompleteComputePath(this NavMeshAgent agent) => agent.pathPending == false;

        public static bool GetNearestPoint(this Vector3 position, int areaMask, out Vector3 point)
        {
            bool isFind;
            float distance = 4f;
            NavMeshHit hit;
            do
            {
                isFind = NavMesh.SamplePosition(position, out hit, distance, areaMask);
                distance *= 2f;
            } while (isFind == false);
            
            point = hit.position;
            return true;
        }
    }
}

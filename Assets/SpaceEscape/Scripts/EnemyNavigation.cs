using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;
public class EnemyNavigation : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent myAgent;
    [SerializeField]
    Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        myAgent.SetDestination(playerTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

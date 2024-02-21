using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cow : MonoBehaviour
{
    [SerializeField] private GameObject _spawner;
    [SerializeField] private GameObject _loot;
    [SerializeField] private GameObject[] _target;
    [SerializeField] private GameObject _FinalTarget;

    private GameObject _realTarget;
    private NavMeshAgent _agent;
    private GameObject _beforeTarget;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetTargetPosition();
    }

    private void Update()
    {
        SetAgentPosition();
    }

    private void SetTargetPosition()
    {
        int random = Random.Range(0, _target.Length);

        if (_beforeTarget == _target[random])
        {
            SetTargetPosition();
        }
        else
        {
            _realTarget = _target[random];
            _beforeTarget = _realTarget;
        }
    }

    private void SetAgentPosition()
    {
        _agent.SetDestination(new Vector3(_realTarget.transform.position.x, _realTarget.transform.position.y, _realTarget.transform.position.z));
    }

    private void SpawnLoot()
    {
        Instantiate(_loot, _spawner.transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _FinalTarget)
        {
            SpawnLoot();
            SetTargetPosition();
        }
        else if (other.gameObject == _realTarget)
        {
            SetTargetPosition();
        }
    }
}
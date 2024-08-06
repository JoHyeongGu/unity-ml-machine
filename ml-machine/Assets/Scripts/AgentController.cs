using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentController : Agent
{
    [SerializeField] private Transform core;
    [SerializeField] private Transform[] cats;
    [SerializeField] private Transform[] houses;

    [SerializeField] private float speed = 2f;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(CreateRandomFloat(0.1f, 13f), 0.2f, CreateRandomFloat(0.1f, 5f));
        transform.rotation = Quaternion.Euler(0, 0, 0);
        core.localPosition = new Vector3(CreateRandomFloat(0.1f, 13f), -1.83f, CreateRandomFloat(0.1f, 5f));
        foreach (Transform cat in cats)
        {
            cat.localPosition = new Vector3(CreateRandomFloat(0.1f, 13f), 0.13f, CreateRandomFloat(0.1f, 5f));
        }
        foreach (Transform house in houses)
        {
            house.localPosition = new Vector3(CreateRandomFloat(0.1f, 13f), -1.11f, CreateRandomFloat(0.1f, 5f));
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 에이전트의 현재 위치
        sensor.AddObservation(transform.localPosition);

        // 코어의 현재 위치
        sensor.AddObservation(core.localPosition);

        // 각 고양이의 위치
        foreach (Transform cat in cats)
        {
            sensor.AddObservation(cat.localPosition);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (transform.position.y < -5f)
        {
            EndEpisode();
        }
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        // 이동 방향과 속도
        Vector3 moveTo = new Vector3(moveX, 0f, moveZ);
        if (moveTo != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveTo), Time.deltaTime * speed * 3);
            transform.localPosition += moveTo * speed * Time.deltaTime;
        }

        // 코어와의 거리 보상
        float distanceToCore = Vector3.Distance(transform.localPosition, core.localPosition);
        AddReward(1.0f / distanceToCore);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = -Input.GetAxis("Horizontal");
        continuousActions[1] = -Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Core"))
        {
            AddReward(10f);
            EndEpisode();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            AddReward(-3f);
            EndEpisode();
        }
        else if (other.gameObject.CompareTag("Cat"))
        {
            AddReward(-7f);
            EndEpisode();
        }
    }

    private float CreateRandomFloat(float min, float max)
    {
        float minus = Random.Range(-max, -min);
        float plus = Random.Range(min, max);
        int index = Random.Range(0, 2);
        return new float[] { minus, plus }[index];
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    public int openingDir;
    //1 right | 2 up | 3 left | 4 down

    private RoomTemplates _templates;
    private Transform _tf;
    private bool _spawned;

    private void Start()
    {
        Destroy(gameObject, 3f);
        _templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        _tf = transform;
        Invoke(nameof(Spawn), 0.1f);
    }

    private void Spawn()
    {
        if (_spawned) return;
        switch (openingDir)
        {
            case 1:
                //Spawn room with right door
                InstantiateRooms(_templates.rightRooms);
                break;
            case 2:
                //Spawn room with up door
                InstantiateRooms(_templates.topRooms);
                break;
            case 3:
                //Spawn room with left door
                InstantiateRooms(_templates.leftRooms);
                break;
            case 4:
                //Spawn room with down door
                InstantiateRooms(_templates.bottomRooms);
                break;
        }
        _spawned = true;
    }

    private void InstantiateRooms(IReadOnlyList<GameObject> rooms)
    {
        //Instantiate rooms
        //keep dungeons small
        if (Mathf.Abs(_tf.position.x) >= _templates.maxRoomDist || Mathf.Abs(_tf.position.y) >= _templates.maxRoomDist)
        {
            Instantiate(_templates.roomEnds[openingDir-1], _tf.position, Quaternion.identity);
        }
        else
        {
            Instantiate(rooms[Random.Range(0, rooms.Count)], _tf.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("SpawnPoint")) return;
        try
        {
            if (!other.GetComponent<RoomSpawner>()._spawned && !_spawned)
            {
                Instantiate(_templates.closedRoom, _tf.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        catch (Exception)
        {
            Destroy(gameObject);
        }

        _spawned = true;
    }
}

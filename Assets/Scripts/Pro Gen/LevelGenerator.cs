
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;

    [Header("Level Settings")]
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] float chunkLength = 10f;
    [Header("Chunk Speeds")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [Header("Gravity Settings")]
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;
    List<GameObject> chunks= new List<GameObject>();

    void Start()
    {
        spawnChunk();
    }

    void Update(){
        MoveChunks();
    }
    
    public void ChangeChunkMoveSpeed(float speedAmount){
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (newMoveSpeed != moveSpeed){
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);
            cameraController.ChangeCameraFOV(speedAmount);
        }

    }
    private void spawnChunk()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            float spawnPosition;
            spawnPosition = transform.position.z + (chunkLength * i);
            Vector3 chunkSpawn = new Vector3(transform.position.x, transform.position.y, spawnPosition);
            GameObject newChunk = Instantiate(chunkPrefab, chunkSpawn, Quaternion.identity, chunkParent);

            chunks.Add(newChunk);
        }
    }

    void MoveChunks(){
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));
            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                addChunk(i);
            }
        }
    }

    private void addChunk(int i)
    {
        float spawnPosition = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        Vector3 chunkSpawn = new Vector3(transform.position.x, transform.position.y, spawnPosition);
        GameObject newChunk = Instantiate(chunkPrefab, chunkSpawn, Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
    }
}

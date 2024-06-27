using Unity.VisualScripting;
using UnityEngine;

public class SpawnManagement : MonoBehaviour
{
    public Transform spawnRigthX;
    public Transform spawnRigthY;

    public Transform spawnLeftY;
    public Transform spawnLeftX;
    public Transform player;

    private float time;
    public float cooldown = 2f;

    public GameObject enemyToInstantiate;

    public void Start()
    {
        time = 0f;
    }

    public void Update()
    {
        time += Time.deltaTime;
        if ( time >= cooldown)
        {
            GameObject newEnemy = Instantiate(enemyToInstantiate, SelectSpawnPoint(), Quaternion.identity);
            time = 0f;
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        bool RightSide = Random.Range(0f, 1f) > .5f;

        if (RightSide)
        {
            spawnPoint.x = spawnRigthX.position.x;
            spawnPoint.y = Random.Range(spawnRigthX.position.y, spawnRigthY.position.y);
        }
        else
        {
            spawnPoint.x = spawnLeftX.position.x;
            spawnPoint.y = Random.Range(spawnLeftX.position.y, spawnLeftY.position.y);
        }
        return spawnPoint;
    }
}

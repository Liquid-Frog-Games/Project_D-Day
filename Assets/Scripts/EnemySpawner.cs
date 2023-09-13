using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner main;

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies;
    [SerializeField] private float enemiesPerSecond;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float difficultyScalingFactor;
    [SerializeField] private TextMeshProUGUI roundUI;

    public int currentWave = 1;
    public int enemySelectMax = 0;
  
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;


    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    void Awake()
    {
        baseEnemies = 8;
        enemiesPerSecond = 0.5f;
        timeBetweenWaves = 5f;
        difficultyScalingFactor = 0.75f;
        StartCoroutine(StartWave());
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    // Start is called before the first frame update
    void Start()
    {
        roundUI.text = currentWave.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if ((timeSinceLastSpawn >= (1f / enemiesPerSecond)) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
     
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        roundUI.text = currentWave.ToString();
        WaveDifficulty();
    }

    private void WaveDifficulty()
    {
        if(currentWave % 2 == 0){
            if(enemySelectMax < enemyPrefabs.Length)
            {
                enemySelectMax += 1;
            }
            timeBetweenWaves -= 0.2f;
            difficultyScalingFactor += 0.25f;
            StartCoroutine(StartWave());

        }
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemySelectMax)];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }


}

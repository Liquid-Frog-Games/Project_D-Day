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
    
    [SerializeField] private GameObject starWaveBtn;
    [SerializeField] private GameObject victoryScreen;
    public int waveGoal = 1;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies;
    [SerializeField] private float enemiesPerSecond;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private TextMeshProUGUI roundUI;

    public int currentWave = 1;
    public int enemySelectMax = 0;
  
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    public int enemiesLeftToSpawn;
    private bool isSpawning = false;


    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    void Awake()
    {
        baseEnemies = 8;
        enemiesPerSecond = 0.5f;
        timeBetweenWaves = 5f;
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

    //Startwave for the button
    public void StartWaveButton()
    {
        StartCoroutine(StartWave());
        starWaveBtn.SetActive(false);
    }

    //called at the end of the wave
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;

        if (currentWave == waveGoal)
        {
            LevelComplete();
            return;
        }

        NextWave();
    }

    public void NextWave()
    {
        currentWave++;
        roundUI.text = currentWave.ToString();
        WaveDifficulty();
    }

    private void LevelComplete()
    {
        Time.timeScale = 0f;
        victoryScreen.SetActive(true);
    }

    //increases the wave difficulty if the wave is an even number
    private void WaveDifficulty()
    {
        if(currentWave % 2 == 0){
            if(enemySelectMax < enemyPrefabs.Length)
            {
                enemySelectMax += 1;
            }
            timeBetweenWaves -= 0.2f;
            StartCoroutine(StartWave());

        }
        baseEnemies += 4;
        StartCoroutine(StartWave());
    }
    //on enemy destroyed
    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    //starts the wave
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }
    //spawns the enemy
    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemySelectMax)];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }
    //calculates the enemy per wave
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies);
    }


}

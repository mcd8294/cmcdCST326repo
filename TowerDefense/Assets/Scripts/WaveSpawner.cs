using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public float timeBetweenWaves = 5.5f;
    public Transform spawnPoint;
    public TextMeshProUGUI waveCountdownText;
    
    private float countdown = 2f;
    private int waveIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.SetText(string.Format("{0:00.00}", countdown));
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        Debug.Log("Wave Incoming!");
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

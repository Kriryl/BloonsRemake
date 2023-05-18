using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Spawner : MonoBehaviour
{
    public Bloon bloon;
    public BloonHirachy hirachy;

    public List<Bloon> bloons = new();

    public TextMeshProUGUI roundText, delayText;

    private void Start()
    {
        bloon = PrefabGetter.BaseBloon;
        hirachy = FindObjectOfType<BloonHirachy>();

        StartRound();
    }

    [Serializable]
    public class Round
    {
        public List<WaveRound> waves = new();
    }

    [Serializable]
    public class WaveRound
    {
        public Wave wave;
        [Tooltip("The delay between wave spawn")] public float delay = 1f;
    }

    [Serializable]
    public class Wave
    {
        public BloonType type;
        public int amount;
        [Tooltip("The delay between each bloon spawn.")] public float delay = 1f;
    }

    public List<Round> rounds = new();

    public float spawnTimer = 0f;
    public bool spawning = false;

    public int RoundIndex { get; set; } = 0;

    private void Update()
    {
        roundText.text = $"Round: {RoundIndex}";
        delayText.text = spawnTimer.ToString();
        delayText.gameObject.SetActive(!spawning);

        if (spawning) { return; }

        if (RoundIndex + 1 > rounds.Count) { return; }

        if (bloons.Count <= 0)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= 5)
            {
                OnRoundStart();
                StartRound();
            }
        }
    }

    public void OnRoundStart()
    {
        Main.Current.money += 100 + (10 * RoundIndex);
    }

    private void StartRound()
    {
        spawning = true;
        Round round = rounds[RoundIndex];
        if (round == null) { return; }

        _ = StartCoroutine(SpawnWaves(round));
        spawnTimer = 0f;
        RoundIndex++;
    }

    public IEnumerator SpawnWaves(Round round)
    {
        for (int i = 0; i < round.waves.Count; i++)
        {
            WaveRound wave = round.waves[i];
            _ = StartCoroutine(SpawnWave(wave.wave));

            if (i == round.waves.Count) { break; }

            yield return new WaitForSeconds(wave.delay);
        }
        spawning = false;
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        var info = hirachy.GetBloonInfo(wave.type);

        for (int i = 0; i < wave.amount; i++)
        {
            Bloon newBloon = Instantiate(bloon, transform.position, transform.rotation);

            newBloon.Init();
            newBloon.SetBloonInfo(info);

            yield return new WaitForSeconds(wave.delay);
        }
    }
}

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
    public bool roundHasEnded = false;
    public bool waiting = false;

    public int RoundIndex { get; set; } = 0;

    private void Update()
    {
        roundText.text = $"Round: {RoundIndex}";
        delayText.text = (Mathf.Round(spawnTimer * 10) / 10).ToString();
        delayText.gameObject.SetActive(waiting);

        if (RoundIndex + 1 > rounds.Count) { return; }

        if (roundHasEnded && !waiting)
        {
            OnRoundEnd();
            waiting = true;
        }

        if (!waiting) { return; }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 5)
        {
            waiting = false;
            StartRound();
        }
    }

    public void OnRoundEnd()
    {
        Main.Current.money += 100 + (10 * RoundIndex);
        spawnTimer = 0f;
        RoundIndex++;
    }

    private void StartRound()
    {
        roundHasEnded = false;
        Round round = rounds[RoundIndex];
        if (round == null) { return; }

        _ = StartCoroutine(SpawnWaves(round));
    }

    public IEnumerator SpawnWaves(Round round)
    {
        for (int i = 0; i < round.waves.Count; i++)
        {
            WaveRound waveRound = round.waves[i];

            if (waveRound == null) { break; }

            var wave = waveRound.wave;

            if (wave == null) { break; }

            var info = hirachy.GetBloonInfo(wave.type);

            for (int i2 = 0; i2 < wave.amount; i2++)
            {
                Bloon newBloon = Instantiate(bloon, transform.position, transform.rotation);

                newBloon.Init();
                newBloon.SetBloonInfo(info);

                yield return new WaitForSeconds(wave.delay);
            }

            if (i == round.waves.Count) { break; }

            yield return new WaitForSeconds(waveRound.delay);
        }

        yield return new WaitUntil(() => bloons.Count <= 0);
        roundHasEnded = true;
    }
}

using UnityEngine;
using TMPro;
using System.Collections;

public class WaveNumberDisplay : MonoBehaviour
{
    public TextMeshProUGUI waveNumberText;
    private WaveManager waveManager; // Reference to WaveManager

    private void Start()
    {
        Debug.Log("WaveNumberDisplay: Start method called.");
        // Find the WaveManager component on the parent GameObject and assign it to waveManager
        waveManager = transform.parent.GetComponent<WaveManager>();

        if (waveManager != null)
        {
            waveManager.waveChangedEvent.AddListener(OnWaveChanged);
            Debug.Log("WaveNumberDisplay: WaveManager found (GetComponent)!");
        }
        else
        {
            Debug.LogError("WaveNumberDisplay: WaveManager not found (GetComponent)!");
        }
    }

    private void OnEnable()
    {
        Debug.Log("WaveNumberDisplay: OnEnable method called.");
        // Subscribe to the waveChangedEvent after a short delay
        StartCoroutine(DelayedWaveManagerAccess());
    }

    IEnumerator DelayedWaveManagerAccess()
    {
        Debug.Log("WaveNumberDisplay: DelayedWaveManagerAccess coroutine started.");
        yield return new WaitForSeconds(0.2f);
        if (waveManager != null)
        {
            waveManager.waveChangedEvent.AddListener(OnWaveChanged);
            Debug.Log("WaveNumberDisplay: WaveManager found!");
        }
        else
        {
            Debug.LogError("WaveNumberDisplay: WaveManager not found!");
        }
    }

    private void OnDisable()
    {
        Debug.Log("WaveNumberDisplay: OnDisable method called.");
        // Unsubscribe from the waveChangedEvent when disabled
        if (waveManager != null)
        {
            waveManager.waveChangedEvent.RemoveListener(OnWaveChanged);
        }
    }

    private void OnWaveChanged(int currentWave)
    {
        Debug.Log("WaveNumberDisplay: OnWaveChanged method called.");
        // Update wave number using TMPro
        waveNumberText.text = "Wave " + (currentWave + 1);
    }
}
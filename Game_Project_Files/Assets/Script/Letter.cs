using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour, IDataPersistence
{
    [Header("UI Elements")]
    public Image CartaAberta;
    public GameObject Panel;
    public Image Papel;
    public Text Instrucao;

    [Header("Audio")]
    public AudioSource _openLetter;

    // Unique identifier for each letter object
    public string LetterID;

    // Letters already collected and loaded from save data
    private static HashSet<string> collectedLetters = new HashSet<string>();

    private void Start()
    {
        // Ensure the letter has a unique ID (fallback to object name)
        if (string.IsNullOrEmpty(LetterID))
            LetterID = gameObject.name;

        // If this letter was already collected in a previous session → remove it from the scene
        if (collectedLetters.Contains(LetterID))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            // Stop player movement before opening the letter
            player.StopMovement();

            StartCoroutine(AbrirCarta(player));
            _openLetter.Play();
        }
    }

    IEnumerator AbrirCarta(PlayerController player)
    {
        // Show first panel (envelope opened)
        Panel.SetActive(true);
        CartaAberta.gameObject.SetActive(true);

        // Wait for SPACE or auto-close after 4 seconds
        float timer = 0;
        while (!Input.GetKeyDown(KeyCode.Space) && timer < 4f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Show second panel (paper)
        CartaAberta.gameObject.SetActive(false);
        Papel.gameObject.SetActive(true);

        // Wait until player presses SPACE
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;

        // Close UI panels
        Papel.gameObject.SetActive(false);
        Panel.SetActive(false);

         Instrucao.gameObject.SetActive(true);
            yield return new WaitForSeconds(4f);
                Instrucao.gameObject.SetActive(false);

        // Allow player to move again
        player.ResumeMovement();

        // Mark this letter as collected
        collectedLetters.Add(LetterID);

        // Save progress to disk
        DataPersistenceManager.instance.SaveGame();

        // Disable the letter in the scene
        gameObject.SetActive(false);

        // Start the mission (your custom logic)
        DataPersistenceManager.instance.MissionCompleted = 0;
        DataPersistenceManager.instance.CurrentOrder = 1;
        PlayerPrefs.SetString("text", "Check if the key is on the laboratory shelf");
        PlayerPrefs.SetInt("_CanRun", 1);
    }

    // =====================
    //     SAVE SYSTEM
    // =====================

    public void LoadData(GameData data)
    {
        // Load all letters that were previously collected
        collectedLetters = new HashSet<string>(data.collectedLetters);

        // If this specific letter was already collected → destroy it
        if (collectedLetters.Contains(LetterID))
            Destroy(gameObject);
    }

    public void SaveData(GameData data)
    {
        // Add this letter's ID to the save file if it isn't there yet
        if (!data.collectedLetters.Contains(LetterID))
            data.collectedLetters.Add(LetterID);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundWordSpawner : MonoBehaviour
{
    [SerializeField]
    private List<string> WordList;
    [SerializeField]
    private GameObject TextPrefab;
    void Update()
    {
        if (!IsInvoking("SpawnWord"))
        {
            Invoke("SpawnWord", 0.3f);
        }
    }
    void SpawnWord()
    {
        GameObject spawnedText = Instantiate(TextPrefab, transform);
        spawnedText.GetComponent<Rigidbody>().velocity = transform.right * Random.Range(100,300);
        spawnedText.GetComponent<TextMeshProUGUI>().text = WordList[Random.Range(0, WordList.Count)];
        spawnedText.transform.localPosition = new Vector3(spawnedText.transform.localPosition.x, Random.Range(-500,500), spawnedText.transform.localPosition.z);
        Destroy(spawnedText, 15);
    }
}

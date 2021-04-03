using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundWordSpawner : MonoBehaviour
{
    public List<string> WordList;
    public GameObject textPrefab;
    void Update()
    {
        if (!IsInvoking("SpawnWord"))
        {
            Invoke("SpawnWord", 0.3f);
        }
    }
    void SpawnWord()
    {
        GameObject spawnedText = Instantiate(textPrefab, transform);
        spawnedText.GetComponent<Rigidbody>().velocity = transform.right * Random.Range(100,300);
        spawnedText.GetComponent<TextMeshProUGUI>().text = WordList[Random.Range(0, WordList.Count)];
        spawnedText.GetComponent<Transform>().transform.localPosition = new Vector3(spawnedText.transform.localPosition.x, Random.Range(-200,200), spawnedText.transform.localPosition.z);
        Destroy(spawnedText, 15);
    }
}

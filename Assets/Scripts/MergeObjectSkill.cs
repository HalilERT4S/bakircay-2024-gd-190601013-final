using DG.Tweening;
using Match;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeObjectSkill : MonoBehaviour
{
    [SerializeField] private MatchArea matchArea; // MatchArea referansý
    private bool isProcessing = false; // Ýþlemin devam ettiðini takip eden bayrak

    public void MergeObjects()
    {
        if (isProcessing)
        {
            Debug.LogWarning("MatchArea iþleme devam ediyor. Lütfen bekleyin!");
            return;
        }
        else
        {
            FindAndMatchObjects();
        }
    }

    private void FindAndMatchObjects()
    {
        isProcessing = true; // Ýþlem devam ediyor
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Moveable");

        if (objects.Length < 2)
        {
            Debug.LogWarning("Eþleþme yapmak için yeterli nesne bulunamadý!");
            return;
        }

        for (int i = 0; i < objects.Length; i++)
        {
            for (int j = i + 1; j < objects.Length; j++)
            {
                var item1 = objects[i].GetComponent<Item>();
                var item2 = objects[j].GetComponent<Item>();

                if (item1 != null && item2 != null && item1.IsMatching(item2))
                {
                    Debug.Log($"Eþleþen nesneler bulundu: {item1.name} ve {item2.name}");
                    MoveObjectsToMatchArea(item1.gameObject, item2.gameObject);
                    return;
                }
            }
        }

        Debug.LogWarning("Eþleþen nesne bulunamadý!");
    }

    private void MoveObjectsToMatchArea(GameObject object1, GameObject object2)
    {

        var rb1 = object1.GetComponent<Rigidbody>();
        var rb2 = object2.GetComponent<Rigidbody>();

        if (rb1 != null) rb1.isKinematic = true;
        if (rb2 != null) rb2.isKinematic = true;

        object1.transform.DOMove(matchArea.transform.position, 1f);
        object2.transform.DOMove(matchArea.transform.position, 1f);

        StartCoroutine(TriggerMatchArea(object1, object2));
    }

    private System.Collections.IEnumerator TriggerMatchArea(GameObject object1, GameObject object2)
    {
        yield return new WaitForSeconds(1.5f);

        var item1 = object1.GetComponent<Item>();
        var item2 = object2.GetComponent<Item>();

        if (item1 != null && item2 != null)
        {
            matchArea.currentObject = object1;
            matchArea.ChechMatch(object2.GetComponent<Collider>());
        }

        // Ýþlemin tamamlandýðýný kontrol etmek için biraz daha bekle
        yield return new WaitForSeconds(1f);

        // MatchArea'nýn içi tamamen boþ olduðunda iþlemi tamamla
        isProcessing = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMTimeChanger : MonoBehaviour
{
    public Animator staticAnim;
    public GameObject medieval, preHistory, sciFi, victorian;

    IEnumerator MMBackgroundShifting()
    {
        while (true)
        {
            medieval.SetActive(true);
            yield return new WaitForSeconds(8);
            staticAnim.SetTrigger("FadeIN");
            yield return new WaitForSeconds(1.5f);
            medieval.SetActive(false);
            preHistory.SetActive(true);
            yield return new WaitForSeconds(8);
            staticAnim.SetTrigger("FadeIN");
            yield return new WaitForSeconds(1.5f);
            preHistory.SetActive(false);
            sciFi.SetActive(true);
            yield return new WaitForSeconds(8);
            staticAnim.SetTrigger("FadeIN");
            yield return new WaitForSeconds(1.5f);
            sciFi.SetActive(false);
            victorian.SetActive(true);
            yield return new WaitForSeconds(8);
            staticAnim.SetTrigger("FadeIN");
            yield return new WaitForSeconds(1.5f);
            victorian.SetActive(false);
        }
    }

    private void Awake()
    {
        medieval.SetActive(false);
        preHistory.SetActive(false);
        sciFi.SetActive(false);
        victorian.SetActive(false);
        StartCoroutine(MMBackgroundShifting());
    }
}

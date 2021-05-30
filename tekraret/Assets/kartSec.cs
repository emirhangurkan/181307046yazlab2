using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kartSec : MonoBehaviour
{
    public string tiklanan = "";
    GameObject gm;
    public int sayac = 0, bittimi = 0;
    bool tiklanabilir = true;

    public GameObject completeLevelUI;
    public GameObject gameOverUI;


    IEnumerator bolumbitti()
    {
        yield return new WaitForSeconds(2.6f);
        completeLevelUI.SetActive(true);
        completeLevelUI.GetComponent<Animator>().SetTrigger("bolumbitti");
    }
    IEnumerator gameover()
    {
        yield return new WaitForSeconds(2.6f);
        gameOverUI.SetActive(true);
        gameOverUI.GetComponent<Animator>().SetTrigger("gameover");
    }
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (tiklanabilir && hit.transform != null && gm != hit.transform.gameObject)
                {
                    sayac++;
                    dondur(hit.transform.gameObject);
                    printTAG(hit.transform.gameObject);
                }
            }
        }
    }
    void dondur(GameObject kart)
    {
        kart.GetComponent<Animator>().SetTrigger("dondur");
    }
    IEnumerator yukselt(GameObject kart1, GameObject kart2)
    {
        yield return new WaitForSeconds(1);
        kart1.GetComponent<Animator>().SetTrigger("yukselt");
        kart2.GetComponent<Animator>().SetTrigger("yukselt");
    }
    IEnumerator kazandin()
    {
        yield return new WaitForSeconds(1.2f);
        tiklanabilir = true;
    }
    IEnumerator tersdondur(GameObject kart1, GameObject kart2)
    {
        yield return new WaitForSeconds(1.2f);
        kart1.GetComponent<Animator>().SetTrigger("tersdondur");
        kart2.GetComponent<Animator>().SetTrigger("tersdondur");
        gm = null;
        tiklanabilir = true;
    }
    private void printTAG(GameObject go)
    {
        if (go.tag == tiklanan && sayac == 2)
        {
            tiklanabilir = false;
            StartCoroutine(kazandin());
            StartCoroutine(yukselt(go, gm));
            Destroy(go, 2.2f);
            Destroy(gm, 2.2f);
            sayac = 0;
            bittimi++;
            if (bittimi == 5 || bittimi == 12 )// 5  9  12
            {
                StartCoroutine(bolumbitti());
            }
            else if (bittimi == 21)
            {
                StartCoroutine(gameover());
            }
            
        }
        if (go.tag != tiklanan && sayac == 2)
        {
            tiklanabilir = false;
            tiklanan = null;
            StartCoroutine(tersdondur(go, gm));
            sayac = 0;
        }
        tiklanan = go.tag;
        gm = go;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class nextScene : MonoBehaviour
{
    public GameObject[] levels;
    public int level;
    public void anasayfa()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void next()
    {
        level++;
        levels[level].SetActive(true);
    }
}

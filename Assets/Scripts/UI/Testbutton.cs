using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testbutton : MonoBehaviour
{
    public void TestLoad()
    {
        SceneManager.LoadScene("MapScene");
        Destroy(gameObject);
    }
}

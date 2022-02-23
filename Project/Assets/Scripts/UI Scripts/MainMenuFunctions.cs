using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
 public void startGame(){
     SceneManager.LoadScene("Game"); 
 }

 public void closeApp(){
     Debug.Log("Quit");
     Application.Quit();
 }
}

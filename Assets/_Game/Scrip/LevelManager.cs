using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
[SerializeField]private Button btnMainMenu;
[SerializeField]private GameObject mainMenu;
private void Start() {
    btnMainMenu.onClick.AddListener(MainMenu);
}
public void MainMenu(){
    mainMenu.SetActive(false);
}
}

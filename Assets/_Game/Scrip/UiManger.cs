using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject panelMenu, panelGameOver, panelSetting;
    [SerializeField] private Button btnSetting, btnRetry, btnContinue,btnRetryFi;
    [SerializeField] private GameObject btnLv; // prefab button
    [SerializeField] private GameObject body; // nơi chứa các button
    [SerializeField] public int numberLv = 5; // số lượng button muốn tạo
    private List<GameObject> spawnedObjects = new List<GameObject>(); // danh sách chứa các button đã tạo

    private void Start()
    {
        SpawnLevels(); // Sinh các button
        panelMenu.SetActive(true);
        btnRetry.onClick.AddListener(Retry);
        btnContinue.onClick.AddListener(Continue);
        btnRetryFi.onClick.AddListener(Retry);
    }
    private void Update()
    {
        Debug.Log(GameManager.instance.IsGameOver);
        btnSetting.onClick.AddListener(Setting);
        GameOver();
    }

    public void Retry()
    {
        GameManager.instance.RetryGame();
        panelSetting.SetActive(false);
        panelGameOver.SetActive(false);
        Time.timeScale = 1;
    }
    public void Continue()
    {
        panelSetting.SetActive(false);
        Time.timeScale = 1;
    }
    public void Setting()
    {
        panelSetting.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameOver()
    {
        if(GameManager.instance.IsGameOver==true){
             panelGameOver.SetActive(true);
             Debug.Log("GameOver");
        }
       
    }

    public void SpawnLevels()
    {
        for (int i = 1; i <= numberLv; i++)
        {
            // Tạo button mới từ prefab
            GameObject setParent = Instantiate(btnLv, btnLv.transform.position, Quaternion.identity);
            setParent.transform.SetParent(body.transform); // Đặt button làm con của body
            spawnedObjects.Add(setParent); // Lưu button vào danh sách

            // Đăng ký sự kiện onClick cho từng button
            Button buttonComponent = setParent.GetComponent<Button>();
            int buttonIndex = i; // Lưu chỉ số để sử dụng trong lambda

            // Gán hàm xử lý sự kiện
            buttonComponent.onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    private void OnButtonClick(int index)
    {
        panelMenu.SetActive(false);
        // Log ra chỉ số của button đã nhấn
        GameManager.instance.MapIndex = index;
        Debug.Log("Button clicked with index: " + index);
        GameManager.instance.LoadMap(index);
    }
}

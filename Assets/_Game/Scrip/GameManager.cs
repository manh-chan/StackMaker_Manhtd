using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    [SerializeField]private Button btnReset;
    [SerializeField] private string baseMapName = "Map";
    [SerializeField] private int retryIndex;
    [SerializeField] public int mapMax;
    private GameObject currentMap;
    private GameObject currentplayer;
    [SerializeField]private GameObject prefabPlayer;
    private bool isGameOver = true;
    [SerializeField]
    private int mapIndex;

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public int MapIndex { get => mapIndex; set => mapIndex = value; }

    private void Awake()
    {
        if (instance == null)instance = this;
    }
    private void Start() {
        StartGame();
        // btnReset.onClick.AddListener(RestartGame);
    }
    public void StartGame(){
        SpawnPlayer();
        isGameOver=false;
    }
    // private void RestartGame(){
    //     UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    // }
     public void LoadMap(int index)
    {
        // Xóa map hiện tại nếu có
        if (currentMap != null) 
        {
            Destroy(currentMap,0.1f);
            Destroy(currentplayer,0.1f);
            Debug.Log("destroyMap");
        }

        // Tạo tên cho map (ví dụ "Map1", "Map2")
        string mapName = baseMapName + index;
        GameObject mapPrefab = Resources.Load<GameObject>("Prefab/" + mapName);

        if (mapPrefab != null)
        {
            retryIndex = index;
            // Instantiate map mới
            currentMap = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);
            SpawnPlayer();
            
            // playerPos.transform.position = new Vector3(4.45f,2.72f,-11.47f);
           
            Debug.Log("Map loaded: " + mapName);
        }
        else
        {
            Debug.LogError("Map not found: " + mapName);
        }
    }
    public void FinishGame(){
      
        if (!isGameOver)
        {
            
            // isGameOver = true;  // Đặt trạng thái kết thúc game
            Debug.Log("Finish");

            // Tăng chỉ số map và load map tiếp theo
            mapIndex++;
            if (mapIndex > mapMax) 
            {
                mapIndex = 1;  // Nếu vượt qua số map tối đa, quay lại map đầu tiên
            }

            LoadMap(mapIndex);
        }
    }
     public void RetryGame() {
        Debug.Log("Retry Game!");
        isGameOver = false;
        LoadMap(retryIndex); 
        Debug.Log(retryIndex);// Tải lại map hiện tại
    }
    public void GameOver(){
       Time.timeScale=0;
       isGameOver = true;
    }
    public void SpawnPlayer(){
        if(currentplayer != null) Destroy(currentplayer);
        // isGameOver = false;
        Vector3 posPlayer = new Vector3(4.45f,2.72f,-11.47f);
        currentplayer=Instantiate(prefabPlayer , posPlayer , quaternion.identity);
    }
}

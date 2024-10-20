using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstAidToolManager : MonoBehaviour
{
    public static FirstAidToolManager Instance { get; private set; }

    public ToolData[] toolDataArray;         // Array of ScriptableObject tools
    public GameObject toolButtonPrefab;      // Prefab for tool buttons that spawn after confirmation
    public Transform toolButtonContainer;    // Container where tool buttons will appear
    public GameObject selectionPanel;        // UI Panel where tool selection happens
    public Button confirmButton;             // Confirm button in the UI

    public Camera mainCamera; // Drag your main camera here in the inspector
    public LayerMask layerMask; // Add the layer you want the ray to hit, like a ground plane

    private List<int> selectedTools = new List<int>();  // Indexes of selected tools
    private GameObject toolInScene;          // Active tool in the game scene
    public List<GameObject> decalTreatment = new List<GameObject>();
    int toolIndex;
    public GameObject wrongToolWarning;

    int woundIndex;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Hook up the Confirm button to the confirmation method
        confirmButton.onClick.AddListener(ConfirmToolSelection);
    }

    private void Update()
    {
        if (toolInScene != null)
        {
            // Get the mouse position in screen space
            Vector3 mousePos = Input.mousePosition;

            // Create a ray from the camera towards the mouse position
            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            // Perform the raycast, checking if it hits something
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                toolInScene.transform.position = hit.point;
                if (Input.GetMouseButtonDown(1))
                {
                    
                    LevelManager.Instance.triesUsed++;
                    if (hit.collider.CompareTag("Treatable"))
                    {
                        if (hit.transform.gameObject.GetComponent<WoundDetail>().medToolIndex[woundIndex] == toolIndex)
                        {
                            decalTreatment[woundIndex].SetActive(true);
                            woundIndex++;
                        }
                        else WrongToolUsed();
                    }
                    else WrongToolUsed();
                }
            }
        }
    }
    public void SelectTool(int toolIndex)
    {
        if (!selectedTools.Contains(toolIndex))
        {
            selectedTools.Add(toolIndex);  // Add the selected tool
        }
        else
        {
            selectedTools.Remove(toolIndex);  // Unselect the tool if clicked again
        }
    }

    public void ConfirmToolSelection()
    {
        // After confirming, hide the selection panel and display the selected tools in the game
        selectionPanel.SetActive(false);

        // Instantiate tool buttons in the game based on the selected tools
        foreach (int toolIndex in selectedTools)
        {
            GameObject toolButton = Instantiate(toolButtonPrefab, toolButtonContainer);
            toolButton.GetComponentInChildren<Text>().text = toolDataArray[toolIndex].toolName;

            int index = toolIndex;  // Copy index to avoid closure issues in lambda
            toolButton.GetComponent<Button>().onClick.AddListener(() => SpawnToolModel(index));
        }
    }

    public void SpawnToolModel(int toolIndex)
    {
        // Remove the previous tool if it exists
        if (toolInScene != null)
        {
            Destroy(toolInScene);
        }

        // Spawn the new tool in the scene based on the tool prefab
        toolInScene = Instantiate(toolDataArray[toolIndex].toolPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        this.toolIndex = toolIndex;
    }

    public void WrongToolUsed()
    {
        wrongToolWarning.SetActive(true);
    }
    public void CheckWinCondition()
    {
        PlayerProgressManager.Instance.CheckWinCondition(woundIndex, decalTreatment.Count);
    }
}

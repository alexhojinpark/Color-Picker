using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // 1. Use Color List
    // 2. Use method like Random.COlorHSV()
    [SerializeField]
    private Color[] colorPalette;
    [SerializeField]
    private float difficultyModifier; // The higher, the more different

    [SerializeField][Range(2,5)]
    private int blockCount = 2;
    [SerializeField]
    private BlockSpawner blockSpawner;

    //Block information list that is created
    private List<Block> blockList = new List<Block>();

    private Color currentColor;
    private Color otherOneColor;
    
    private int score;
    private int otherBlockIndex; // Index of otherOneColor
    
    public TextMeshProUGUI textScore;

    private void Awake()
    {
        blockList = blockSpawner.SpawnBlocks(blockCount);
        for (int i = 0; i < blockList.Count; i++)
        {
            blockList[i].Setup(this);
        }
        SetColors();
    }

    private void SetColors()
    {
        // Change block color, harder and harder as it changes
        difficultyModifier *= 0.92f;

        // Default block color
        Color currentColor = colorPalette[Random.Range(0, colorPalette.Length)];
        
        // Correct block color
        float diff = (1.0f/255.0f) * difficultyModifier;
        otherOneColor = new Color(currentColor.r - diff, currentColor.g - diff, currentColor.b - diff);
        
        // Index of Correct block
        otherBlockIndex = Random.Range(0, blockList.Count);
        // Debug.Log(otherBlockIndex); // AnswerBlock index

        // Block and OtherOneBlock color settings
        for (int i = 0; i < blockList.Count; i++)
        {
            if (i == otherBlockIndex)
            {
                blockList[i].Color = otherOneColor;
            }
            else 
            {
                blockList[i].Color = currentColor;
            }
        }
    }

    public void CheckBlock(Color color)
    {
        //
        if (blockList[otherBlockIndex].Color == color)
        {
            SetColors();
            score++;
            textScore.text = score.ToString();
            Debug.Log(score);
        }
        else 
        {
            Debug.Log("Wrong");
            UnityEditor.EditorApplication.ExitPlaymode();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Board : MonoBehaviour
{
    [SerializeField] Color colorLight, colorDark;

    private MeshRenderer[,] squareRenderer;

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnValidate()
    {
        UpdateColours();
    }

    void CreateBoard()
    {
        Shader squareShader = Shader.Find("Unlit/Color");
        squareRenderer = new MeshRenderer[8, 8];

        for (int rank = 0; rank < 8; rank++)
        {
            for (int file = 0; file < 8; file++)
            {
                // Create square
                Transform square = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
                square.parent = transform;
                square.name = $"Square {rank} {file}";
                square.position = new Vector2(-3.5f + file, -3.5f + rank);
                Material squareMaterial = new Material(squareShader);

                // Set color
                bool isLightSquare = (file + rank) % 2 != 0;
                squareMaterial.color = (isLightSquare) ? colorDark : colorLight;

                // Insert in array
                squareRenderer[file, rank] = square.gameObject.GetComponent<MeshRenderer>();
                squareRenderer[file, rank].material = squareMaterial;
            }

        }
    }

    private void UpdateColours()
    {
        for (int rank = 0; rank < 8; rank++)
        {
            for (int file = 0; file < 8; file++)
            {
                if (squareRenderer[file, rank] != null)
                {
                    Material squareMaterial = squareRenderer[file, rank].material;
                    bool isLightSquare = (file + rank) % 2 != 0;
                    squareMaterial.color = isLightSquare ? colorDark : colorLight;
                }
            }
        }
    }
}
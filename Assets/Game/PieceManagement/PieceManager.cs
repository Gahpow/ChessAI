using static PieceNotation;

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEditor;

public class PieceManager : MonoBehaviour
{
    static string STARTING_POSITION_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

    Board _board;

    static Dictionary<string, string> _pieceNotationToName = new Dictionary<string, string>()
    {
        {"k", "King"}, {"q", "Queen"}, {"r", "Rook"},
        {"b", "Bishop"}, {"n", "Knight"}, {"p", "Pawn"}
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadFENString(STARTING_POSITION_FEN);
    }

    void Awake()
    {
        _board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        Piece.InitializeStaticReferences(_board, this);
        Piece.LoadSprites();
    }

    // Update is called once per frame
    void Update()
    {
    }
    //void CreatePiece(string pieceNotation, int rank, int file)
    //{
    //    if (!validNotation.Contains(pieceNotation)) 
    //    {
    //        Debug.Log("Invalid Notation");
    //        return;
    //    }

    //    // Create Game Object
    //    GameObject piece = new GameObject($"{pieceNotation}{rank}{file}");
    //    piece.layer = 1;
    //    piece.transform.parent = this.transform; 

    //    // Add Sprite Renderer Component
    //    SpriteRenderer pieceSprite = piece.AddComponent<SpriteRenderer>();

    //    pieceSprite.sprite = _pieceSprites[pieceNotation];

    //    // Set variables
    //    pieceSprite.transform.SetPositionAndRotation(_board.squareRenderer[rank-1, file-1].transform.position, _pieceRotation);
    //    pieceSprite.transform.localScale = _pieceScale;
    //}

    void CreatePiece(string pieceNotation, int rank, int file)
    {
        if (!validNotation.Contains(pieceNotation))
        {
            Debug.Log($"Incorrect notation {pieceNotation}");
        }
        GameObject pieceObject = new GameObject($"{pieceNotation} {rank} {file}");

        Type t = Type.GetType(_pieceNotationToName[pieceNotation.ToLower()]);
        Piece piece = (Piece)pieceObject.AddComponent(t);

        piece.Initialize(pieceNotation, rank, file);
    }

    void LoadFENString(string sequence)
    {
        int rank = 1;
        int file = 8;

        foreach (char c in sequence)
        {
            if (char.IsLetter(c))
            {
                CreatePiece(c.ToString(), rank, file);
                rank++;
            }
            else if (char.IsDigit(c))
            {
                int interval = int.Parse(c.ToString());
                if (interval <= 8) 
                {
                    rank += interval;
                }
            }
            else if (c == '/')
            {
                rank = 1;
                file--;
            }
        }
    }
}

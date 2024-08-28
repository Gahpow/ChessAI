using static PieceNotation;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    static string IMAGES_PATH = "Images/";

    static Dictionary<string, Sprite> _pieceSprites = new();

    static Quaternion _pieceRotation = new Quaternion(0, 0, 0, 1);

    static Vector3 _pieceScale = new Vector3(0.9f, 0.9f, 0.9f);

    static Board _board;
    
    static PieceManager _manager;

    SpriteRenderer _pieceSprite;

    public int _rank, _file;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        
    }

    public static void InitializeStaticReferences(Board board, PieceManager manager)
    {
        _board = board;
        _manager = manager;
    }

    public void Initialize(string pieceNotation, int rank, int file)
    {
        // Create Game Object
        gameObject.layer = 1;
        gameObject.transform.parent = _manager.transform;

        // Add Sprite Renderer Component
        _pieceSprite = gameObject.AddComponent<SpriteRenderer>();
        _pieceSprite.sprite = _pieceSprites[pieceNotation];

        // Set variables
        _rank = rank;
        _file = file;
        _pieceSprite.transform.SetPositionAndRotation(_board.squareRenderer[rank - 1, file - 1].transform.position, _pieceRotation);
        _pieceSprite.transform.localScale = _pieceScale;
    }

    public static void LoadSprites()
    {
        // Black pieces
        _pieceSprites.Add(KING_BLACK, Resources.Load<Sprite>($"{IMAGES_PATH}black-king"));

        _pieceSprites.Add(QUEEN_BLACK, Resources.Load<Sprite>($"{IMAGES_PATH}black-queen"));

        _pieceSprites.Add(ROOK_BLACK, Resources.Load<Sprite>($"{IMAGES_PATH}black-rook"));

        _pieceSprites.Add(BISHOP_BLACK, Resources.Load<Sprite>($"{IMAGES_PATH}black-bishop"));

        _pieceSprites.Add(KNIGHT_BLACK, Resources.Load<Sprite>($"{IMAGES_PATH}black-knight"));

        _pieceSprites.Add(PAWN_BLACK, Resources.Load<Sprite>($"{IMAGES_PATH}black-pawn"));

        // White pieces
        _pieceSprites.Add(KING_WHITE, Resources.Load<Sprite>($"{IMAGES_PATH}white-king"));

        _pieceSprites.Add(QUEEN_WHITE, Resources.Load<Sprite>($"{IMAGES_PATH}white-queen"));

        _pieceSprites.Add(ROOK_WHITE, Resources.Load<Sprite>($"{IMAGES_PATH}white-rook"));

        _pieceSprites.Add(BISHOP_WHITE, Resources.Load<Sprite>($"{IMAGES_PATH}white-bishop"));

        _pieceSprites.Add(KNIGHT_WHITE, Resources.Load<Sprite>($"{IMAGES_PATH}white-knight"));

        _pieceSprites.Add(PAWN_WHITE, Resources.Load<Sprite>($"{IMAGES_PATH}white-pawn"));

        foreach (var sprite in _pieceSprites)
        {
            if (sprite.Value == null)
            {
                Debug.Log($"Missing resource for piece {sprite.Key}");
            }
        }
    }

}

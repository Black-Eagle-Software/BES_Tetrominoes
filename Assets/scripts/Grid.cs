using UnityEngine;
using System.Collections;
using Assets.managers;

namespace Assets.scripts {
    //modified from http://noobtuts.com/unity/2d-tetris-game
    public class Grid : Singleton<Grid> {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        void Awake() {
            GameGrid = new Transform[ Width, Height ];
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {

        }

        public Vector2 RoundVector2(Vector2 v ) {
            return new Vector2( Mathf.Round( v.x ), Mathf.Round( v.y ) );
        }

        public bool IsInsideBorder(Vector2 position ) {
            return ( ( int )position.x >= 0 && ( int )position.x < Width && ( int )position.y >= 0 );
        }

        public void DeleteRow(int y ) {
            for ( int x = 0; x < Width; x++ ) {
                Destroy( GameGrid[ x, y ].gameObject );
                GameGrid[ x, y ] = null;
            }
        }

        public void DecreaseRow(int y ) {
            for ( int x = 0; x < Width; x++ ) {
                if(GameGrid[x, y ] != null ) {
                    //move one towards bottom
                    GameGrid[ x, y - 1 ] = GameGrid[ x, y ];
                    GameGrid[ x, y ] = null;

                    //update block positions
                    GameGrid[ x, y - 1 ].position += new Vector3( 0, -1, 0 );
                }
            }
        }

        public void DecreaseRowsAbove(int y ) {
            for ( int i = y; i < Height; i++ ) {
                DecreaseRow( i );
            }
        }

        public bool IsRowFull(int y ) {
            for ( int x = 0; x < Width; x++ ) {
                if ( GameGrid[ x, y ] == null ) {
                    return false;
                }                
            }
            return true;
        }

        public void DeleteFullRows() {
            for ( int y = 0; y < Height; y++ ) {
                if ( IsRowFull( y ) ) {
                    DeleteRow( y );
                    DecreaseRowsAbove( y + 1 );
                    y--;
                }
            }
        }

        #endregion


        #region Fields

        public int Width = 10;
        public int Height = 20;
        public Transform[, ] GameGrid;

        #endregion

    }

}
using UnityEngine;
using System.Collections;
using Assets.managers;

namespace Assets.scripts {
    //modified from http://noobtuts.com/unity/2d-tetris-game
    public class Group : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            if ( !IsValidGridPosition() ) {
                Debug.Log( "Game over!" );
                Destroy( gameObject );
            }
        }

        // Update is called once per frame
        void Update() {
            if ( !isDownKeyPressed ) {
                fallTimeStep = 0.5f;
            }
            if ( shouldReceiveInputs ) {
                MoveOnKeyPress( KeyCode.LeftArrow, new Vector3( -1, 0, 0 ) );
                MoveOnKeyPress( KeyCode.RightArrow, new Vector3( 1, 0, 0 ) );

                if ( Input.GetKeyDown( KeyCode.UpArrow ) ) {
                    transform.Rotate( 0, 0, -90 );
                    if ( IsValidGridPosition() ) {
                        UpdateGrid();
                    } else {
                        transform.Rotate( 0, 0, 90 );
                    }
                }

                if ( Input.GetKeyDown( KeyCode.DownArrow ) ) {
                    MoveDown();
                }
                /*if ( Input.GetKey( KeyCode.DownArrow ) ) {
                    isDownKeyPressed = true;
                    fallTimeStep = 0.5f;
                    MoveDown();
                }*/
                isDownKeyPressed = false;
            }
        }

        void FixedUpdate() {
            if ( shouldReceiveInputs && Time.time - lastFall >= fallTimeStep ) {
                MoveDown();

                lastFall = Time.time;
            }

            if ( transform.childCount == 0 ) {
                Destroy( gameObject );
            }
        }

        private void MoveDown() {
            transform.position += new Vector3( 0, -1, 0 );

            if ( IsValidGridPosition() ) {
                UpdateGrid();
            } else {
                transform.position += new Vector3( 0, 1, 0 );
                Grid.Instance.DeleteFullRows();

                SpawnManager.Instance.SpawnNext();

                //enabled = false;
                shouldReceiveInputs = false;
            }
        }

        void MoveOnKeyPress( KeyCode key, Vector3 newDirection ) {
            if ( Input.GetKeyDown( key ) ) {
                transform.position += newDirection;

                if ( IsValidGridPosition() ) {
                    UpdateGrid();
                } else {
                    transform.position += new Vector3( newDirection.x * -1, newDirection.y * -1, newDirection.z * -1 );
                }
            }
        }

        bool IsValidGridPosition() {
            foreach ( Transform child in transform ) {
                var gi = Grid.Instance;
                var v = gi.RoundVector2( child.position );

                if ( !gi.IsInsideBorder( v ) ) {
                    return false;
                }

                if ( gi.GameGrid[ ( int )v.x, ( int )v.y ] != null && gi.GameGrid[ ( int )v.x, ( int )v.y ].parent != transform ) {
                    return false;
                }
            }
            return true;
        }

        void UpdateGrid() {
            var gi = Grid.Instance;
            for ( int y = 0; y < gi.Height; y++ ) {
                for ( int x = 0; x < gi.Width; x++ ) {
                    if ( gi.GameGrid[ x, y ] != null ) {
                        if ( gi.GameGrid[ x, y ].parent == transform ) {
                            gi.GameGrid[ x, y ] = null;
                        }
                    }
                }
            }

            foreach ( Transform child in transform ) {
                var v = gi.RoundVector2( child.position );
                gi.GameGrid[ ( int )v.x, ( int )v.y ] = child;
            }
        }

        #endregion


        #region Fields

        bool isDownKeyPressed = false;
        bool shouldReceiveInputs = true;
        float lastFall = 0;
        float fallTimeStep;
        float originalFallTimeStep;

        #endregion

    }

}
using UnityEngine;
using System.Collections;
using Assets.scripts;
using System;

namespace Assets.managers {
    //modified from http://noobtuts.com/unity/2d-tetris-game
    public class SpawnManager : Singleton<SpawnManager> {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            SpawnNext();
            SpawnNext();    //hacky-hack to populate the nextgroup slot in the beginning
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {

        }

        public void SpawnNext() {
            if ( !_hasNextGroup ) {
                var i = UnityEngine.Random.Range( 0, Groups.Length );
                _nextGroup = ( GameObject )Instantiate( Groups[ i ], NextSpawnLocation, Quaternion.identity );
                var ngg = _nextGroup.GetComponent<Group>();
                ngg.ShouldCheckPositionValid = false;
                var v = ( GroupType[] )Enum.GetValues( typeof( GroupType ) );
                ngg.Type = v[ i ];
                _hasNextGroup = true;
            } else {
                _nextGroup.transform.position = GridSpawnLocation;
                var ngg = _nextGroup.GetComponent<Group>();
                ngg.ShouldCheckPositionValid = true;
                ngg.FallTimeStep = Difficulty > 15 ? .25f : 1f - ( Difficulty / 20f );
                OnSpawnedGroup( ngg.Type );
                _hasNextGroup = false;
                SpawnNext();
            }
        }

        protected virtual void OnSpawnedGroup( GroupType t ) {
            var handler = this.SpawnedGroup;
            if ( handler != null ) {
                handler( t );
            }
        }

        #endregion


        #region Fields

        private GameObject _nextGroup;
        private bool _hasNextGroup = false;

        public GameObject[] Groups;
        public Vector2 GridSpawnLocation;
        public Vector2 NextSpawnLocation;
        public int Difficulty;

        public delegate void SpawnGroup( GroupType t );
        public event SpawnGroup SpawnedGroup;

        #endregion

    }
}

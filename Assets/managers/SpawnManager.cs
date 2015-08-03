using UnityEngine;
using System.Collections;
using Assets.scripts;

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
                var i = Random.Range( 0, Groups.Length );
                _nextGroup = ( GameObject )Instantiate( Groups[ i ], NextSpawnLocation, Quaternion.identity );
                _nextGroup.GetComponent<Group>().ShouldCheckPositionValid = false;
                _hasNextGroup = true;
            } else {
                _nextGroup.transform.position = GridSpawnLocation;
                _nextGroup.GetComponent<Group>().ShouldCheckPositionValid = true;
                _hasNextGroup = false;
                SpawnNext();
            }
        }

        #endregion


        #region Fields

        private GameObject _nextGroup;
        private bool _hasNextGroup = false;

        public GameObject[] Groups;
        public Vector2 GridSpawnLocation;        
        public Vector2 NextSpawnLocation;

        #endregion

    }
}

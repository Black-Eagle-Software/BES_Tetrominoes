using UnityEngine;
using System.Collections;

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
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {

        }

        public void SpawnNext() {
            var i = Random.Range( 0, Groups.Length );
            var sg = Instantiate( Groups[ i ], SpawnLocation, Quaternion.identity );            
        }

        #endregion


        #region Fields
        
        public GameObject[] Groups;
        public Vector2 SpawnLocation;

        #endregion

    }
}

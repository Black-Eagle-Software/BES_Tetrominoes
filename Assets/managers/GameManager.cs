using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.scripts;

namespace Assets.managers {
    public class GameManager : Singleton<GameManager> {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            SpawnManager.Instance.SpawnedGroup += GroupSpawned;

            Istats.text = "0";
            Jstats.text = "0";
            Lstats.text = "0";
            Ostats.text = "0";
            Sstats.text = "0";
            Tstats.text = "0";
            Zstats.text = "0";
        }

        

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {

        }

        private void GroupSpawned( GroupType t ) {
            switch ( t ) {
                case GroupType.I:
                    IncrementStatText( Istats );
                    break;
                case GroupType.J:
                    IncrementStatText( Jstats );
                    break;
                case GroupType.L:
                    IncrementStatText( Lstats );
                    break;
                case GroupType.O:
                    IncrementStatText( Ostats );
                    break;
                case GroupType.S:
                    IncrementStatText( Sstats );
                    break;
                case GroupType.T:
                    IncrementStatText( Tstats );
                    break;
                case GroupType.Z:
                    IncrementStatText( Zstats );
                    break;
                default:
                    break;
            }
        }

        void IncrementStatText(Text target ) {
            target.text = ( int.Parse( target.text ) + 1 ).ToString();
        }

        #endregion


        #region Fields

        public Text Istats;
        public Text Jstats;
        public Text Lstats;
        public Text Ostats;
        public Text Sstats;
        public Text Tstats;
        public Text Zstats;

        #endregion

    }
}
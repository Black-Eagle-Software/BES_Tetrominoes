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
            var sm = SpawnManager.Instance;
            sm.SpawnedGroup += GroupSpawned;
            Grid.Instance.DidDeleteRow += DidDeleteRow;

            Istats.text = "0";
            Jstats.text = "0";
            Lstats.text = "0";
            Ostats.text = "0";
            Sstats.text = "0";
            Tstats.text = "0";
            Zstats.text = "0";

            Score.text = "0";
            Lines.text = "0";
            LinesSpecial.text = "";

            Level = 0;
            LevelT.text = Level.ToString();
            sm.Difficulty = Level;
            _levelLinesOrig = LevelLinesReqd;

            _shouldIncrementScore = false;
            _linesCount = 0;

            sm.StartSpawning();
        }

        private void DidDeleteRow() {
            _shouldIncrementScore = true;
            _linesCount++;
            /*IncrementText( Score, 40 * ( Level + 1 ) );
            IncrementText( Lines, 1 );*/
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {
            if ( _shouldTimeSpecialText && Time.time - _specialTextTime >= _specialTextTimeout ) {
                LinesSpecial.text = "";
                _specialTextTime = Time.time;
            }

            if ( _shouldIncrementScore ) {
                LinesSpecial.text = "";
                switch ( _linesCount ) {
                    case 1:
                        IncrementText( Score, 40 * ( Level + 1 ) );
                        break;
                    case 2:
                        IncrementText( Score, 100 * ( Level + 1 ) );
                        LinesSpecial.text = "DOUBLE!";
                        _shouldTimeSpecialText = true;
                        _specialTextTime = Time.time;
                        break;
                    case 3:
                        IncrementText( Score, 300 * ( Level + 1 ) );
                        LinesSpecial.text = "TRIPLE!!";
                        _shouldTimeSpecialText = true;
                        _specialTextTime = Time.time;
                        break;
                    case 4:
                        IncrementText( Score, 1200 * ( Level + 1 ) );
                        LinesSpecial.text = "QUADRUPLE!!!";
                        _shouldTimeSpecialText = true;
                        _specialTextTime = Time.time;
                        break;
                }
                IncrementText( Lines, _linesCount );
                LevelLinesReqd -= _linesCount;
                _linesCount = 0;
                _shouldIncrementScore = false;
            }

            if ( int.Parse( Lines.text ) > 0 || LevelT.text != Level.ToString() ) {
                if ( LevelLinesReqd == 0 ) {
                    Level++;
                    SpawnManager.Instance.Difficulty = Level;
                    LevelLinesReqd = _levelLinesOrig;
                }
                LevelT.text = Level.ToString();
            }
        }

        private void GroupSpawned( GroupType t ) {
            switch ( t ) {
                case GroupType.I:
                    IncrementText( Istats, 1 );
                    break;
                case GroupType.J:
                    IncrementText( Jstats, 1 );
                    break;
                case GroupType.L:
                    IncrementText( Lstats, 1 );
                    break;
                case GroupType.O:
                    IncrementText( Ostats, 1 );
                    break;
                case GroupType.S:
                    IncrementText( Sstats, 1 );
                    break;
                case GroupType.T:
                    IncrementText( Tstats, 1 );
                    break;
                case GroupType.Z:
                    IncrementText( Zstats, 1 );
                    break;
                default:
                    break;
            }
        }

        void IncrementText( Text target, int amount ) {
            target.text = ( int.Parse( target.text ) + amount ).ToString();
        }

        #endregion


        #region Fields

        private bool _shouldIncrementScore;
        private int _linesCount;
        private bool _shouldTimeSpecialText;
        private float _specialTextTime;
        private float _specialTextTimeout = 3f;
        private int _levelLinesOrig;

        public Text Istats;
        public Text Jstats;
        public Text Lstats;
        public Text Ostats;
        public Text Sstats;
        public Text Tstats;
        public Text Zstats;

        public Text Score;
        public Text Lines;
        public Text LevelT;
        public Text LinesSpecial;

        public int Level;
        public int LevelLinesReqd = 10;

        #endregion

    }
}
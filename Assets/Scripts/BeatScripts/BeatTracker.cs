﻿using System;
using UnityEngine;

namespace Assets.Scripts.BeatScripts {
    public class BeatTracker: MonoBehaviour {
        private const float ScaleTrashold = 0.1f;
        private const float MinBeatCooldown = 0.35f;

        public float BeatLowerLimit;
        public float BeatUpperLimit;
        private float _beatCooldown;

        private bool _isBeatThisTurn;
        private AudioSource _track;
        public event EventHandler BeatEvent;

        private void Start() {
            _track = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();

            // Null action to avoid null exception
            BeatEvent += delegate { };
            //audio.time = 6;
        }

        private void Update() {
            float c1 = GetBassBeat();
            Debug.DrawLine(new Vector3(0, 0), new Vector3(0, c1*4), Color.red);

            // Beat Tracking

            _beatCooldown -= Time.deltaTime;
            if (_beatCooldown < 0){
                _beatCooldown = 0;
            }

            if (_isBeatThisTurn && c1 < BeatLowerLimit){
                _isBeatThisTurn = false;
            }

            if (!_isBeatThisTurn && c1 > BeatLowerLimit && _beatCooldown == 0){
                _isBeatThisTurn = true;
                _beatCooldown = MinBeatCooldown;
                BeatAction();
            }
        }

        public float GetBassBeat() {
            float[] spectrum = _track.GetSpectrumData(1024, 0, FFTWindow.Hamming);
            //	    var c3 : float = spectrum[11] + spectrum[12] + spectrum[13];
            //	    var c4 : float = spectrum[22] + spectrum[23] + spectrum[24];
            //	    var c5 : float = spectrum[44] + spectrum[45] + spectrum[46] + spectrum[47] + spectrum[48] + spectrum[49]; 
            //        float c1 = spectrum[2] + spectrum[3] + spectrum[4];
            float c1 = spectrum[2] + spectrum[3];

            return c1;
        }

        private void FixedUpdate() {
            //        float[] spectrum = trek.GetSpectrumData(1024, 0, FFTWindow.Hamming);
            //        float c1 = spectrum[2] + spectrum[3] + spectrum[4];
            //        Debug.Log(c1);
        }

        private void BeatAction() {
            BeatEvent(null, null);
        }
    }
}
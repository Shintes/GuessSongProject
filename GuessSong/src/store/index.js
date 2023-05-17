"use strict";
import axios from 'axios';
Object.defineProperty(exports, "__esModule", { value: true });
import Vuex from 'vuex';
export default new Vuex.Store({
    state: {
        authenticated: false,
        results: [],
        songHistory: [],
        score: 0,
        name: "",
        scoreBonusArtist: 1,
        scoreBonusTrack: 1,
        alreadyGuessedArtist: false,
        alreadyGuessedTrack: false,
        extraScoreMatch: false,
        artistName: "",
        trackName: "",
        artworkUrl100: "",
        artistNameHistory: "",
        trackNameHistory: "",
        previewUrl: "",
        round: 0,
        isPlaying: false,
        volume: 0.2
    },
    mutations: {

        SET_AUTH: (function (state, auth) { return state.authenticated = auth; }),
        setResults(state, results) {
            state.results = results
            state.previewUrl = results.previewUrl
            state.artistName = results.artistName
            state.trackName = results.trackName
            state.artworkUrl100History = results.artworkUrl100
            state.artistNameHistory = results.artistName
            state.trackNameHistory = results.trackName
        },
        setScore(state, score) {
            state.score = score
        },
        setRound(state, round) {
            state.round = round
        },
        setName(state, name) {
            state.name = name
        },
        setScoreBonusArtist(state, scoreBonusArtist) {
            state.scoreBonusArtist = scoreBonusArtist
        },
        setScoreBonusTrack(state, scoreBonusTrack) {
            state.scoreBonusTrack = scoreBonusTrack
        },
        setAlreadyGuessedArtist(state, alreadyGuessedArtist) {
            state.alreadyGuessedArtist = Boolean(alreadyGuessedArtist);
        },
        setAlreadyGuessedTrack(state, alreadyGuessedTrack) {
            state.alreadyGuessedTrack = Boolean(alreadyGuessedTrack);
        },
        setExtraScoreMatch(state, extraScoreMatch) {
            state.extraScoreMatch = Boolean(extraScoreMatch);
        },
        setArtistName(state, artistName) {
            state.artistName = String(artistName);
        },
        setArtistNameHistory(state, artistNameHistory) {
            state.artistNameHistory = String(artistNameHistory);
        },
        setTrackName(state, trackName) {
            state.trackName = String(trackName);
        },
        setTrackNameHistory(state, trackNameHistory) {
            state.trackNameHistory = String(trackNameHistory);
        },
        setArtworkurl100History(state, artworkUrl100History) {
            state.artworkUrl100History = String(artworkUrl100History);
        },
        setPreviewUrl(state, previewUrl) {
            state.previewUrl = String(previewUrl);
        },
        incrementRound(state) {
            state.round++;
        },
        incrementScore(state) {
            state.score++;
        },
        addIntoSongHistory(state, song) {
            state.songHistory.push(song);
        },

        clearHistory(state) {
            state.songHistory = []
        },
        setVolume(state, volume) {
            state.volume = volume
        }
    },
    actions: {
        setAuth: (function (_a, auth) {
            var commit = _a.commit;
            return commit('SET_AUTH', auth);
        }),
        async loadSongsRap({ commit }) {
            var result = await axios.get("http://localhost:8000/api/GetSongs/rap", {
                headers: {
                'Access-Control-Allow-Origin': '*'
                },
            })
            commit("setResults", result.data);
        },
        async loadSongsRock({ commit }) {
            var result = await axios.get("http://localhost:8000/api/GetSongs/rock",{
                headers: {
                    'Access-Control-Allow-Origin': '*'
                    },
            });
            commit("setResults", result.data);
        },
        async loadSongsHits({ commit }) {
            var result = await axios.get("http://localhost:8000/api/GetSongs/hits",{
                headers: {
                    'Access-Control-Allow-Origin': '*'
                    },
            });
            commit("setResults", result.data);
        },
        setClearState({ commit }) {
            commit('setScoreBonusArtist', 0),
                commit('setScoreBonusTrack', 0),
                commit('setAlreadyGuessedArtist', false),
                commit('setAlreadyGuessedTrack', false),
                commit('setExtraScoreMatch', false),
                commit('setArtistName', ""),
                commit('setTrackName', "")
        },

        setClearScore({ commit }) {
            commit('setScore', 0)
        },

        setClearRound({ commit }) {
            commit('setRound', 0)
        },
        incrementCounterUp({ commit }) {
            commit('incrementRound')
        },
        checkAlreadyGuessedArtist({ commit }) {
            commit('incrementScore'),
                commit('setScoreBonusArtist', 1),
                commit('setAlreadyGuessedArtist', true),
                commit('setArtistName', "")
        },
        checkAlreadyGuessedTrack({ commit }) {
            commit('incrementScore'),
                commit('setScoreBonusTrack', 1),
                commit('setAlreadyGuessedTrack', true),
                commit('setTrackName', "")
        },
        checkScoreBonusArtist({ commit }) {
            commit('incrementScore'),
                commit('setExtraScoreMatch', true)
        },

        addCurrentSongToHistory({ state }) {
            this.commit('addIntoSongHistory',
                {
                    artistNameHistory: state.artistNameHistory,
                    trackNameHistory: state.trackNameHistory,
                    artworkUrl100History: state.artworkUrl100History
                })
        },
        deleteSongHistory({ commit }) {
            commit('clearHistory')
        },
        setVolume({commit}, volume) {
            commit('setVolume', volume)
        }
    },
})

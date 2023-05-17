<template>
    <props-category Rock="Rock" Rap="Rap" Hits="Hits" :HandleClickRock="handleClickRock"
        :HandleClickRap="handleClickRap" :HandleClickHits="handleClickHits"></props-category>
    <p>{{this.Genre}}</p>

    <body>
        <home-page ref="HomePage"></home-page>
        <h1 class="Title">Guess the Song</h1>
        <p v-if="(artist != null)">{{artist}}</p>
        <p v-if="(track != null)">{{track}}</p>
        <img v-if="(artwork != null)" v-bind:src="artwork" class="imgGuessed" />
        <get-score-vue ref="getScoreVue">Highscore:</get-score-vue>
        <p class="Round"> Round: {{ round }}</p>
        <h5 class="Score">Score: {{ score }}</h5>
        <div class="btnContainer">
            <div class="btnCenter">
                <audio-component ref="audioComponent"></audio-component>
                <button class="btnSongs" v-on:click="this.btnNextSongTrue()">Next Song</button>
                <button class="btnSongs" v-on:click="this.btnPlayPauseAudio()">Play/Pause Song</button>
            </div>
        </div>
        <ul class='tracksUl'>
            <li class="tracksLi">
                <div class="songs" v-for="( song, index ) in songHistory" :key="index">
                    <div class="song-info">
                        <div>{{ song.artistNameHistory }} {{ song.trackNameHistory }}</div>
                        <img v-bind:src="song.artworkUrl100History" class="center" />
                    </div>
                </div>
            </li>
        </ul>
        <input type="text" v-model="keyword" v-on:keyup.enter="this.processUserInput(), this.handleClear()"
            class="form-control" placeholder="Track || Artist" />
    </body>
</template>
<style src= "./style.css"></style>

<script>
import PropsCategoryVue from './PropsCategory.vue';
import AudioComponent from './AudioComponent.vue';
import GetScoreVue from './GetScore.vue';
import HomePage from './HomePage.vue';
export default {
    name: "CategoryPage",
    data() {
        return {
            keyword: "",
            playPauseAudio: false,
            artist: "",
            track: "",
            artwork: "",
            Genre:"Rock",
        }
    },
    computed: {
        round() {
            return this.$store.state.round
        },
        score() {
            return this.$store.state.score
        },
        scoreBonusArtist() {
            return this.$store.state.scoreBonusArtist
        },
        scoreBonusTrack() {
            return this.$store.state.scoreBonusTrack
        },
        alreadyGuessedTrack() {
            return this.$store.state.alreadyGuessedTrack
        },
        alreadyGuessedArtist() {
            return this.$store.state.alreadyGuessedArtist
        },
        artistName() {
            return this.$store.state.artistName
        },
        extraScoreMatch() {
            return this.$store.state.extraScoreMatch
        },
        trackName() {
            return this.$store.state.trackName
        },
        artworkUrl100() {
            return this.$store.state.artworkUrl100
        },
        songHistory() {
            return this.$store.state.songHistory
        },
        previewUrl() {
            return this.$store.state.previewUrl
        },
    },

    components: { "audio-component": AudioComponent, "get-score-vue": GetScoreVue, "props-category": PropsCategoryVue, "home-page": HomePage },
    methods: {
        handleClickRock() {
            this.Genre = "Rock"
        },
        handleClickRap() {
            this.Genre = "Rap"
        },
        handleClickHits() {
            this.Genre = "Hits"
        },
        btnPlayPauseAudio() {
            if (this.playPauseAudio == true) {
                this.audioPlay()
                this.playPauseAudio = false;
            }
            else {
                this.audioStop()
                this.playPauseAudio = true;
            }
        },
        async btnNextSongTrue() {
            this.artist = "";
            this.track = "";
            this.artwork = "";
            if (this.$store.state.round == 10) {
                this.Update();
                this.$store.dispatch('setClearRound');
                this.$store.dispatch('setClearScore');
                this.audioStop()
            }
            if (this.$store.state.round == 1){
                this.$store.dispatch('deleteSongHistory');
            }
            if (this.$store.state.round != 0) {
                this.audioStop();
                this.$store.dispatch('addCurrentSongToHistory');
            }
            this.$store.dispatch('setClearState');

            await this.$store.dispatch('loadSongs' + this.Genre);

            this.$store.dispatch('incrementCounterUp');
            this.audioPlay();
            this.HighScore();
        },
        handleClear() {
            this.keyword = '';
        },
        beforeDestroy() {
            clearInterval(this.timer);
        },
        audioPlay() {
            this.$refs.audioComponent.play();
        },
        audioStop() {
            this.$refs.audioComponent.stop();
        },
        HighScore() {
            this.$refs.getScoreVue.HighScore();
        },
        Update() {
            this.$refs.getScoreVue.updateScore();
        },

        processUserInput(str1 = this.keyword, str2 = this.$store.state.trackName, str3 = this.$store.state.artistName) {
            const ToBeRemoved = [" ",",","&", ";", ":", ".", "<", ">", "{", "}", "|", "'", "\"", "\\", "!", "@", "#", "$", "%", "^", "*", ")", "(", "-", "_", "+", "=",];
            for (let i = 0; i < ToBeRemoved.length; i++) {
                
                str1 = str1.replaceAll(ToBeRemoved[i], "");
                str2 = str2.replaceAll(ToBeRemoved[i], "");
                str3 = str3.replaceAll(ToBeRemoved[i], "");
                
                str3 = str3.replace(/ *\([^)]*\) */g, "");
                str2 = str2.replace(/ *\([^)]*\) */g, "");
                str1 = str1.replace(/ *\([^)]*\) */g, "");
            }
            const strUserInput = str1.toLowerCase();
            const strTrack = str2.toLowerCase();
            const strArtist = str3.toLowerCase();
            if (this.$store.state.alreadyGuessedArtist == false) {
                if (strUserInput == strArtist){
                    this.$store.dispatch('checkAlreadyGuessedArtist');
                    this.artist = this.$store.state.artistNameHistory;
                }
            }
            if (this.$store.state.alreadyGuessedTrack == false) {
                if (strUserInput == strTrack) {
                    this.$store.dispatch('checkAlreadyGuessedTrack');
                    this.track = this.$store.state.trackNameHistory
                }
            }
            if (this.$store.state.scoreBonusArtist == 1 && this.$store.state.scoreBonusTrack == 1) {
                if (this.$store.state.extraScoreMatch == false) {
                    {
                        this.$store.dispatch('checkScoreBonusArtist');
                        this.artwork = this.$store.state.artworkUrl100History;
                    }
                }
            }
        },
    }
}
</script>
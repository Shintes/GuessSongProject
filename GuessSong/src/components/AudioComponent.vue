<template>
    <div>
        <input type="range" id="volumeControl" v-on:change="audioVolumeChanged" v-model="audioVolume"/>
    </div>
</template>
<script>
export default {
    name: "AudioComponent",
    data() {
        return {
            player: null,
            audioVolume: this.$store.state.volume * 100
        };
    },
    methods: {
        audioVolumeChanged(e) {
            this.$store.dispatch('setVolume',  e.currentTarget.value / 100);
            this.player.volume = this.$store.state.volume
        },
        play() {
            this.player.src = this.$store.state.previewUrl;
            this.player.load();
            if (this.player.paused) {
                this.player.play();
                this.duration = this.player.duration;
            } else {
                this.player.pause();
            }

        },
        stop() {
            if(this.player != null) {
                this.player.pause();
            }
        },
    },
    mounted() {
        this.player = new Audio()
        this.player.volume = this.$store.state.volume
    },
    unmounted() {
        this.stop()
    }
}

</script>
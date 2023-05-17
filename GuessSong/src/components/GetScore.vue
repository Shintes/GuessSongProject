<template>

    <body>
        <p>Highscore:{{ resultsScore.points }}</p>
    </body>
</template>

<script>
import axios from 'axios'
export default {
    name: "ScorePage",
    data() {
        return {
            resultsScore: [],
        };
    },
    computed: {
        score() {
            return this.$store.state.score
        },
    },
    methods: {
        async HighScore() {
            if (localStorage.getItem("token") != null) {
                const responseScore = await axios.get('http://localhost:8000/api/score/score?id=' + localStorage.getItem('id'), {
                    headers: {
                        "Authorization": "Bearer " + localStorage.getItem("token")
                    }
                });
                this.resultsScore = await responseScore.data;
            }
        },

        async updateScore() {
            if (localStorage.getItem("token") != null) {
                await axios.post('http://localhost:8000/api/Score/Create?', {
                    id: this.resultsScore.id,
                    date: this.resultsScore.date,
                    points: this.$store.state.score,
                    accountId: this.resultsScore.accountId,},
                    {
                    headers: {
                        "Authorization": "Bearer " + localStorage.getItem("token")
                    }
                    })
            }
        }
    },
}
</script>
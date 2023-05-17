<template>

    <body class="leaderboardsPage">
        <ul class='leaderboardUl'>
            <select  class="selectHighScores" v-model = "selectedOption" @change="valueHasChanged">
                <option v-for="option in options" :key="option.Id">{{option.leaderboard}}</option>
            </select>
             <li class="leaderboardLi">
                <div class="LeaderboardDiv2" v-if="results.length">
                    <div v-for="user in results" :key="user.AccountId"> Name: {{ user.userName }} Score: {{ user.score }}</div>
                </div>
            </li>
        </ul>
    </body>
</template>

<script>
import axios from 'axios';
export default {
    name: "LeaderboardsPage",
    data() {
        return {
            results: [],
            selectedOption: "",
            options: [{
            leaderboard: "Top Scores",
            Id: 1,
          },
          {
            leaderboard: "Daily",
            Id: 2,
          },
          {
            leaderboard: "Monthly",
            Id: 3,
          },
          {
            leaderboard: "Yearly",
            Id: 4,
          },
        ]
        }
    },

    async mounted() {
    },

    methods: {
        async Topleaderboards() {
            const response = await axios.get("http://localhost:8000/api/Leaderboard/leaderboard");
            this.results = await response.data;
        },
        async Monthlyleaderboards() {
            const response = await axios.get("http://localhost:8000/api/Leaderboard/monthlyLearderboard");
            this.results = await response.data;
        },
        async Dailyleaderboards() {
            const response = await axios.get("http://localhost:8000/api/Leaderboard/dailyLeaderboard");
            this.results = await response.data;
        },
        async Yearlyleaderboards() {
            const response = await axios.get("http://localhost:8000/api/Leaderboard/yearlyLeaderboard");
            this.results = await response.data;
        },

        valueHasChanged(event) {
            var option = event.target.value;
            switch (option) {
                case "Top Scores":
                return this.Topleaderboards()
                case "Daily":
                return this.Dailyleaderboards()
                case "Monthly":
                return this.Monthlyleaderboards()
                case "Yearly":
                return this.Yearlyleaderboards()
            }
        }
    },
}
</script>

<style>

    .selectHighScores{
      display: flex;
      flex-direction: column;
      align-items: center;
      padding: 6px 14px;
      border-radius: 26px;
      color: #3D3D3D;
      background: #fff;
      border: none;
      box-shadow: 0px 0.5px 1px rgba(0, 0, 0, 0.1);
      touch-action: manipulation;
      justify-content: center;
      width: 310px;
    }
    
    .leaderboardLi {
    
        margin: 0 2px 2px 0;
        padding: 8px;
        min-height: 300px;
        box-shadow: 0 1px 2px rgba(0, 0, 0, .075);
        border-radius: 6px;
        border: 1px solid #ccc;
        width: 300px;
    }
    
    .tleaderboardUl {
        width: 330px;
        max-height: 240px;
        line-height: 18px;
        margin: 18px 0;
        max-height: 240px;
        overflow: auto;
        padding: 0;
        list-style: none;
    }
    
    .LeaderboardDiv2 {
        margin-right: 15px;
    }
    
    .leaderboardsPage {
        padding-left: 0;
        position: relative;
        top: 170px;
    }
    </style>
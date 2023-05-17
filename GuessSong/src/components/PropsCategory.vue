<template>
    <div class="SelectCategory">
        <button @click="HandleClickRock">{{Rock}}</button>
        <button @click="HandleClickRap">{{Rap}}</button>
        <button @click="HandleClickHits">{{Hits}}</button>
    </div>
</template>

<script>
    export default{
        props:{
            Rock: {
                required: true,
                type: String
            },
            HandleClickRock:{
                required: true,
                type:Function
            },
            Rap: {
                required: true,
                type: String
            },
            HandleClickRap:{
                required: true,
                type:Function
            },
            Hits: {
                required: true,
                type: String
            },
            HandleClickHits:{
                required: true,
                type:Function
            }
            
        }
    }
</script>

<style>
    
.SelectCategory {
  margin-left: 50%;
  margin: 0;
  display: flex;
  justify-content: center;
  align-content: center;
  top: 50%;
  -ms-transform: translateY(-50%);
  transform: translateY(-50%);
}
</style>
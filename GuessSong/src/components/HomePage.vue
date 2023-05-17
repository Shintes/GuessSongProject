<style>
.welcomeMesage{
  position:absolute;
  align-items: center;
  right: 10%;
  
}
.btn-primary {
  position:absolute;
  align-items: center;
  right: 20%;
}


</style>


  <template>
    <h3 class="welcomeMesage">{{ message }}</h3>
    <a href="javascript:void(0)" class="welcomeMesage"
       @click="logout"
    >Logout</a>
</template>

<script>
import {onMounted, ref} from 'vue';
import axios from 'axios';
import {useRouter} from "vue-router";
export default {
  name: "HomePage",
  setup() {
    const message = ref('');
    const router = useRouter();
    onMounted(async () => {
      try {
        if(localStorage.getItem("token")!=null){
        const {data} = await axios.get('http://localhost:8000/api/Account/user',{
          headers:{ 
            "Authorization": "Bearer " + localStorage.getItem("token")  
          }
        });
        localStorage.setItem('id', data.id);
        message.value = `Hi ${data.userName}`;
      }
      } catch (e) {
        await router.push('/');
      }
    });
    const logout = async () => {
      await axios.post('http://localhost:8000/api/Account/logout', {}, { withCredentials: true });
      axios.defaults.headers.common['Authorization'] = '';
      localStorage.clear('id')
      await router.push('/login');
    }
    return {
      message,
      logout
    }
  }
}
</script>
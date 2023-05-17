
<template>
  <main class="form-signin">
    <form @submit.prevent="submit">
      <h1 class="h3 mb-3 fw-normal">Please sign in</h1>
      <h1 class="h3 mb-3 fw-normal" v-if="(invalidAccountOrPassword == true)">Email or Password is wrong</h1>
      <div class="form-floating">
        <input type="email" class="form-control" name="email" placeholder="name@example.com">
        <label>Email</label>
      </div>

      <div class="form-floating">
        <input type="password" class="form-control" name="password" placeholder="Password">
        <label>Password</label>
      </div>

      <button class="btn-login" type="submit">Submit</button>
    </form>
  </main>
</template>

<script>
import { useRouter } from "vue-router";
import axios from "axios";
export default {
  data() {
    return {
      invalidAccountOrPassword: false
    }
  },
  name: "LoginAccount",
  setup() {
    const router = useRouter();
    const submit = async e => {
      const form = new FormData(e.target);
      const inputs = Object.fromEntries(form.entries());
      var response = await axios.post('http://localhost:8000/api/Account/login', inputs, {
        withCredentials: true,
        validateStatus: function () {
          return true;
        }
      });
      if (response.status ===  401) {
        this.invalidAccountOrPassword = true;
      }
      if (response.status === 200) {
        axios.defaults.headers.common['Authorization'] = `Bearer ${response.data}`;
        localStorage.setItem('token', response.data);
        router.push('/')
        this.invalidAccountOrPassword = false;
      }
    }

    return {
      submit
    }
  },
}
</script>

<style>
.fw-normal {
  width: 100%;
  text-align: center;
  height: 150px;
  line-height: 5;
}

.container {
  height: 100px;
  position: relative;
}

.vertical-center {
  margin: 0;
  position: absolute;
  top: 50%;
  left: 50%;
  -ms-transform: translate(-50%, -50%);
  transform: translate(-50%, -50%);
}

.btn-login {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 6px 14px;
  border-radius: 6px;
  color: #3D3D3D;
  background: #fff;
  border: none;
  box-shadow: 0px 0.5px 1px rgba(0, 0, 0, 0.1);
  touch-action: manipulation;
  justify-content: center;
  width: 50%;
  margin-left: 25%;
}

.btn-login:focus {
  box-shadow: 0px 0.5px 1px rgba(0, 0, 0, 0.1), 0px 0px 0px 3.5px rgba(58, 108, 217, 0.5);
  outline: 0;
}
</style>
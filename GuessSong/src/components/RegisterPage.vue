<template>
  <main class="form-signin">
    <form @submit.prevent="submit">
      <h1 class="h3 mb-3 fw-normal">Please register</h1>

      <div class="form-floating">
        <input class="form-control" name="Username" placeholder="User Name">
      </div>

      <div class="form-floating">
        <input type="email" class="form-control" name="email" placeholder="name@example.com">
      </div>

      <div class="form-floating">
        <input type="password" class="form-control" name="password" placeholder="Password">
      </div>
      <div class="container">
        <div class="vertical-center">
          <button class="btn-login" type="submit">Submit</button>
        </div>
      </div>
    </form>
  </main>
</template>

<script>
import axios from 'axios';
import { useRouter } from "vue-router";
export default {
  name: "RegisterPage",
  data() {
    return {
    }
  },
  setup() {
    const router = useRouter();
    const submit = async e => {
      const form = new FormData(e.target);
      const inputs = Object.fromEntries(form.entries());
      await axios.post('http://localhost:8000/api/Account/register', inputs)
      await router.push('/login');
    }
    return {
      submit
    }
  }
}
</script>
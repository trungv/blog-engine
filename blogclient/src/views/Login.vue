<template>
<div class="content">
  <div  v-if="!isAuthenticated">
    <div class="login-form">
      <h2 class="text-center mb-5">Login</h2>
      <form>
        <div class="form-group">
          <label for="inputdefault">Username:</label>
          <input class="form-control" v-validate="'required'" data-vv-as="Username" type="text" name="username">
          <span class="text-danger" v-show="errors.has('username')">{{ errors.first('username') }}</span>
        </div>
        <div class="form-group">
          <label for="inputdefault">Password:</label>
          <input class="form-control" v-validate="'required'" data-vv-as="Password" type="password" name="password">
          <span class="text-danger" v-show="errors.has('password')">{{ errors.first('password') }}</span>
        </div>
        <div class="form-group text-center">
          <span class="text-danger">{{msg}}</span>
          <button type="button" class="btn btn-primary mt-2 w-100" v-on:click="login()">Login</button>
        </div>
      </form>
    </div>
  </div>
</div>
</template>

<script>
  import accountService from "../services/account.service.js"
  export default {
    name: "Login",
    props: {
      msg: String
    },
    computed: {
      isAuthenticated() {
        return this.$store.state.isAuthenticated;
      }
    },
    data () {
      return {
        user:{
          username:"",
          password:""
        },

      }
    },
    methods:{
      login(){
        this.$validator.validate().then(result => {
          if (result) {
            this.user.username = document.getElementsByName("username")[0].value;
            this.user.password = document.getElementsByName("password")[0].value;
            accountService.login(this.user).then(data => {
              if(data.success){
                this.$store.state.isAuthenticated = true;
                window.localStorage.setItem('token', data.token)
                this.$router.push('admin');
                //TODO: luu localstroge, route toi page admin, doi thanh logout.
              } else {
                this.msg = "Username or Password is not correct."
                //TODO: thong bao meo thanh cong.
              }
              console.log(data);
            })
          }
        });        
      },
      logout(){
        this.isAuthenticated = false;
      }
    }
  }
</script>

<style>
  .login-form{
    width: 30%;
    margin: 0 35% 0 35%;
  }
</style>

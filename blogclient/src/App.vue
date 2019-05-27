<template>
  <div id="app">
    <div class="container">
      <div class="nav">
         <vs-navbar v-model="activeItem" class="nabarx">
          <vs-navbar-title>
            <h2>Blog Tech</h2>
          </vs-navbar-title>
          <vs-spacer></vs-spacer>
          <vs-navbar-item index="0">
            <router-link to="/">Home</router-link>
          </vs-navbar-item>
          <vs-navbar-item index="1">
            <router-link to="/login" v-if="!isAuthenticated">Login</router-link>
            <router-link to="/admin" v-if="isAuthenticated">Admin</router-link>
          </vs-navbar-item>
        </vs-navbar>
      </div>
      <div>
        <router-view/>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "App",
    props: {
      msg: String
    },
    computed: {
      isAuthenticated() {
        return this.$store.state.isAuthenticated;
      }
    }
  }
</script>

<style lang="scss">
  .nav{
    margin-top: 20px;
    padding-bottom: 30px;
  }
</style>

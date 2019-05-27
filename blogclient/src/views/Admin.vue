<template>
  <div class="admin" v-if="isAuthenticated">
      <div class="row">
        <div class="col-md-3">
           <div class="row">
            <vs-list>
              <vs-list-header title="Menu"></vs-list-header>
                <div>
                  <router-link to="/admin/categoriesmanage"><vs-list-item          
                    title="Categories" 
                    subtitle="Manage all Categories">
                    </vs-list-item> 
                  </router-link>    
                </div>
                <div>
                  <router-link to="/admin/postmanage"><vs-list-item          
                    title="Posts" 
                    subtitle="Manage all Posts"></vs-list-item>    
                  </router-link> 
                </div>
                <div v-on:click="logout()">
                  <router-link to="#"><vs-list-item          
                    title="Logout" 
                    subtitle="Exit your account"></vs-list-item>    
                  </router-link> 
                </div>
              </vs-list>
          </div>
        </div>
        <div class="col-md-9">
          <router-view/>
        </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "Admin",
    props: {
      msg: String
    },
    computed: {
      isAuthenticated() {
        return this.$store.state.isAuthenticated;
      }
    },
    methods:{
      loadPage(){
        if(!this.isAuthenticated) {
          this.$router.push('/login');
        }  
      },
      logout(){
        window.localStorage.setItem('token', null)
        this.$store.state.isAuthenticated = false;
        this.$router.push('/login');
      }
    },
    created(){
      this.loadPage();
    }
  }
</script>
<template>
  <div class="categories">
    <div class="row">
      <vs-chip v-for="item in categories"
          :key="item.name"
          color="rgb(113, 108, 103)">
        {{item.name}}
      </vs-chip>
    </div>
    <div class="row">
      <vs-list>
        <vs-list-header title="Categories"></vs-list-header>
        <div v-for="item in categories" :key="item.name" v-on:click="onClick(item.name)">
         <router-link to="#"><vs-list-item          
          :title="item.name" 
          :subtitle="item.description"></vs-list-item></router-link>     
        </div>
      </vs-list>
    </div>
  </div>
</template>

<script>
import categoriesService from "../services/categories.service.js"
export default {
  name: "Categories",
  props: {
    msg: String
  },
  data () {
    return {
      categories: []
    }
  },
  methods: {
    loadCategories () {
      categoriesService.getCategories().then(data => {
        this.categories = data
        console.log(data)
      })
    },
    onClick(categoriesName){
      //console.log(categoriesName);
      this.$store.commit("loadPostByCategory", categoriesName);
    }
  },
  created () {
      this.loadCategories()
      console.log(this.$route.query.page)
    }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
    // .categories{
    //     background-color: #2A2B26;
    //     height: 500px;
    // }
    // .button-category{
    //     width: 100%;
    //     font-size: 0.8em;
    //     font-weight: bold;
    // }
</style>
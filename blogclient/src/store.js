import Vue from "vue";
import Vuex from "vuex";
import postService from "./services/post.service"

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    posts:[],
    messagePostList:"The latest posts:",
    isAuthenticated: false
  },
  mutations: {
    loadPostByCategory (state, categoriesName) {
      postService.getPostbyCategory(categoriesName).then(data => {
        state.posts = data
        if(data.length > 0){
          state.messagePostList = `The posts in ${categoriesName}:`
        } else {
          state.messagePostList = `No post in ${categoriesName}:`
        }
        
        console.log(data)
      })
    }
  },
  actions: {}
});

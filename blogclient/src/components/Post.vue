<template>
  <div class="post">
    <div class=message>
      <h4>{{messagePostList}}</h4>
    </div>
    <div>
        <post-card v-for="item in posts"
        :key="item.name"
        :item="item">
        </post-card>
    </div>
  </div>
</template>

<script>
import PostCard from "./PostCard.vue"
import postService from "../services/post.service.js"
export default {
  name: "Post",
  components: {
      'post-card': PostCard
    },
  props: {
    msg: String
  },
  computed: {
    posts() {
      return this.$store.state.posts;
    },
    messagePostList() {
      return this.$store.state.messagePostList;
    }
  },
  methods: {
      loadPostTop (num) {
        postService.getPostTop(num).then(data => {
          this.$store.state.posts = data
          console.log(data)
        })
      }
    },
  created(){
    this.loadPostTop(10);
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
  .message{
    margin-bottom: 30px;
  }
</style>
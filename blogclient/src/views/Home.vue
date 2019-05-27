<template>
  <div class="home">
      <div class="row">
        <div class="col-md-3">
          <Categories/>
        </div>
        <div class="col-md-9">
          <Post/>
        </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import Categories from "@/components/Categories.vue";
import Post from "@/components/Post.vue";

export default {
  name: "home",
  components: {
    Categories,
    Post
  }
};
</script>

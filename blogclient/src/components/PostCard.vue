<template>
  <div class="card">
    <!-- <div class="card-content">
      <slot name="title"></slot>
      <slot name="content"></slot>
    </div> -->
    <!-- <footer class="card-footer">
      <a class="card-footer-item" :href="link"
      target="_blank">Read More</a>
    </footer> -->
      <vs-card class="cardx">
        <div slot="header">
          <h5>
            {{item.shortDescription}}
          </h5>
        </div>
        <div slot="media">
          <img src="../assets/blogthum.png" alt="" height="100" width="42">
        </div>
        <div>
          <p>{{item.content}}</p>
          <span>{{item.createdDate}}</span>
        </div>
        <div slot="footer">
          <vs-row vs-justify="flex-end">
            <vs-button color="rgb(42,97,230)" color-text="rgb(50,50,50)">Read more</vs-button>
          </vs-row>
        </div>
      </vs-card>
  </div>
</template>
<script>
  export default {
    props: {
        item:{}
    }
  }
</script>
<style scoped>
  .card{
    margin: 5px 0 5px 0;
    border: none;
  }
</style>
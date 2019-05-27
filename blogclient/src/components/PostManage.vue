<template>
  <div class="postManage">
    <div class="data-table" v-if="!editable">
      <h3>Post List: </h3>
      <div class="float-sm-right mb-2">
        <button type="button" class="btn btn-primary" v-on:click="onAdd()">Add Post</button>
      </div>
      <table class="table table-hover">
        <thead>
          <tr>
            <th>Slug</th>
            <th>Short Description</th>
            <th>Created Date</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody v-for="item in posts" :key="item.createdDate">
          <tr>
            <td>{{item.slug}}</td>
            <td>{{item.shortDescription}}</td>
            <td>{{item.createdDate}}</td>
            <td>
              <a href="#" v-on:click="onEdit(item)">Edit</a>|<a href="#" v-on:click="onDelete(item.slug)">Delete</a>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="edit-mode" v-if="editable">
      <h4>Input Post:</h4>
      <form>
        <div class="form-group">
          <label for="inputdefault">Slug:</label>
          <input class="form-control" type="text" v-model="editItem.Post.slug" :disabled="saveMode=='edit'?true:false" v-validate="'required'" data-vv-as="Slug" name="slug">
          <span class="text-danger" v-show="errors.has('slug')">{{ errors.first('slug') }}</span>
        </div>
        <div class="form-group" v-if="saveMode=='add'?true:false">
          <label for="sel1">Category:</label>
          <select class="form-control" v-model="editItem.CategoriesName" v-validate="'required'" data-vv-as="Category" name="category">
            <option disabled value="">Please select one</option>
            <option v-for="cate in categories" :key="cate.name">{{cate.name}}</option>
          </select>
          <span class="text-danger" v-show="errors.has('category')">{{ errors.first('category') }}</span>
        </div>
        <div class="form-group">
          <label for="inputdefault">Short Description:</label>
          <textarea class="form-control" rows="2" v-model="editItem.Post.shortDescription" v-validate="'required'" data-vv-as="Short Description" name="short-description"></textarea>
          <span class="text-danger" v-show="errors.has('short-description')">{{ errors.first('short-description') }}</span>
        </div>
        <div class="form-group">
          <label for="inputdefault">Thumbnail Image:</label>
          <input class="form-control" type="text" value="blogthum.png" v-model="editItem.Post.thumbnailImage" disabled>
          <img class="mt-2" src="../assets/blogthum.png" alt="">
        </div>          
        <div class="form-group">
          <label for="inputdefault">Content:</label>
          <textarea class="form-control" rows="15" v-model="editItem.Post.content" v-validate="'required'" data-vv-as="Content" name=content></textarea>
          <span class="text-danger" v-show="errors.has('content')">{{ errors.first('content') }}</span>
        </div>
        <div>
          <button type="button" class="btn btn-primary m-1" v-on:click="onSave(editItem)">Save</button>
          <button type="button" class="btn btn-default m-1" v-on:click="onCancel()">Cancel</button>
        </div>
      </form>
    </div>
    <div id="jump-to"></div>        
    <div class="centerx">
      <vs-popup class="holamundo" title="Notification" :active.sync="popupActivo">
        <p>
          {{message}}
        </p>
      </vs-popup>
    </div>   
  </div>
</template>

<script>
import postService from "../services/post.service.js"
import categoriesService from "../services/categories.service.js"
export default {
  name: "PostManage",
  props: {
    msg: String
  },
  data () {
    return {
      posts: [],
      categories: [],
      editItem: {
        Post:{
          thumbnailImage:"blogthum.png"
        },
        CategoriesName:""
      },
      editable: false,
      popupActivo:false,
      message: "",
      saveMode: ""
    }
  },
  methods: {
    loadAllPost () {
      postService.getAllPost().then(data => {
        this.posts = data
        console.log(data)
      })
    },
    loadCategories () {
        categoriesService.getCategories().then(data => {
          this.categories = data
          console.log(data)
        })
    },
    onAdd(){
      this.editable = true;
      this.saveMode = "add";
      this.editItem = {
        Post:{
          thumbnailImage:"blogthum.png"
        },
        CategoriesName:""
      };
      // document.getElementById('jump-to').scrollIntoView();
    },
    onEdit(item){
      Object.assign(this.editItem.Post, item)
      this.editable = true;
      this.saveMode = "edit";
    },
    onSave(item){
      this.$validator.validate().then(result => {
        if (result) {
                if (this.saveMode === "edit"){
        postService.editPost(item.Post).then(data => {
          if(data == true){
            this.editable = false;
            this.loadAllPost();
            this.message = "Update Successful";
            this.popupActivo=true;
          } else {
            this.editable = false;
            this.message = "Update Failed";
            this.popupActivo=true;
          }
          console.log(data)
        })    
        console.log(item);  
      } else {
        postService.createPost(item).then(data => {
          if(data == true){
            this.editable = false;
            this.loadAllPost();
            this.message = "Create Successful";
            this.popupActivo=true;
          } else {
            this.editable = false;
            this.message = "Create Failed";
            this.popupActivo=true;
          }
          console.log(data)
        })  
        console.log(item);    
      }
        }
      }); 
    },
    onCancel(){
      this.editItem.Post = {};
      this.editItem.CategoriesName = "";
      this.editable = false;
      this.saveMode = "";
    },
    onDelete(slug){
      postService.deletePost(slug).then(data => {
        if(data == true){
          this.loadAllPost();
          this.message = "Delete Successful";
          this.popupActivo=true;
        } else {
          this.message = "Delete Failed";
          this.popupActivo=true;
        }
        console.log(data)
      })  
    }
  },
  created(){
    this.loadAllPost();
    this.loadCategories();
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

</style>
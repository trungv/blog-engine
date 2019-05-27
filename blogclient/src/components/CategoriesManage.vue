<template>
  <div class="categoriesManage">
    <div class="data-table" v-if="!editable">
      <h3>Categories List: </h3>
      <div class="float-sm-right mb-2">
        <button type="button" class="btn btn-primary" v-on:click="onAdd()">Add Category</button>
      </div>
      <table class="table table-hover">
        <thead>
          <tr>
            <th>Category Name</th>
            <th>Description</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody v-for="item in categories" :key="item.name">
          <tr>
            <td>{{item.name}}</td>
            <td>{{item.description}}</td>
            <td>
              <a href="#" v-on:click="onEdit(item)">Edit</a>|<a href="#" v-on:click="onDelete(item.name)">Delete</a>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="edit-mode" v-if="editable">
      <h4>Input Category:</h4>
      <form>
        <div class="form-group">
          <label for="inputdefault">Category Name:</label>
          <input name="cate-name" v-validate="{ required: true, regex: /^[a-zA-Z0-9]*$/ }" data-vv-as="Category Name" class="form-control" type="text" v-model="editItem.name" :disabled="saveMode=='edit'?true:false">
          <span class="text-danger" v-show="errors.has('cate-name')">{{ errors.first('cate-name') }}</span>
        </div>
        <div class="form-group">
          <label for="inputdefault">Description:</label>
          <textarea name="description" v-validate="'required'" data-vv-as="Description" class="form-control" rows="7" v-model="editItem.description"></textarea>
          <span class="text-danger" v-show="errors.has('description')">{{ errors.first('description') }}</span>
        </div>
        <div>
          <button type="button" class="btn btn-primary m-1" v-on:click="onSave(editItem)">Save</button>
          <button type="button" class="btn btn-default m-1" v-on:click="onCancel()">Cancel</button>
        </div>
      </form>
    </div>
    <!-- <div id="jump-to"></div>         -->
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
import categoriesService from "../services/categories.service.js"

export default {
  name: "Categories",
  props: {
    msg: String
  },
  data () {
    return {
      categories: [],
      editItem: {},
      editable: false,
      popupActivo:false,
      message: "",
      saveMode: ""
    }
  },
  methods: {
    loadCategories () {
      categoriesService.getCategories().then(data => {
        this.categories = data
        console.log(data)
      })
    },
    onEdit(item){
      Object.assign(this.editItem, item)
      this.editable = true;
      this.saveMode = "edit";
    },
    onAdd(){
      this.editable = true;
      this.saveMode = "add";
      this.editItem = {};
      // document.getElementById('jump-to').scrollIntoView();
    },
    onDelete(categoriesName){
      categoriesService.deleteCategories(categoriesName).then(data => {
        if(data == true){
          this.loadCategories();
          this.message = "Delete Successful";
          this.popupActivo=true;
        } else {
          this.message = "Delete Failed";
          this.popupActivo=true;
        }
        console.log(data)
      })  
    },
    onSave(item){
      this.$validator.validate().then(result => {
        if (result) {
          if (this.saveMode === "edit"){
            categoriesService.editCategories(item).then(data => {
              if(data == true){
                this.editable = false;
                this.loadCategories();
                this.message = "Update Successful";
                this.popupActivo=true;
              } else {
                this.editable = false;
                this.message = "Update Failed";
                this.popupActivo=true;
              }
              console.log(data)
            })      
          } else {
            categoriesService.createCategories(item).then(data => {
              if(data == true){
                this.editable = false;
                this.loadCategories();
                this.message = "Create Successful";
                this.popupActivo=true;
              } else {
                this.editable = false;
                this.message = "Create Failed";
                this.popupActivo=true;
              }
              console.log(data)
            })    
          }
        }
      });
    },
    onCancel(){
      this.editItem = {};
      this.editable = false;
      this.saveMode = "";
    }
  },
  created () {
      this.loadCategories()
    }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

</style>
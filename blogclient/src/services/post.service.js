import axios from "axios"

axios.defaults.baseURL = 'http://localhost:44367/api/'

const token = window.localStorage.getItem('token')
const config = {
  headers: {
    Authorization :`Bearer ${token}`
  }  
}

const postService = {
    getPostTop (num) {
      return new Promise((resolve) => {
        axios.get(`Post/Newest/${num}`)
          .then(response => {
            resolve(response.data);
          })
      })
    },
    getAllPost () {
      return new Promise((resolve) => {
        axios.get(`Post`)
          .then(response => {
            resolve(response.data);
          })
      })
    },
    getPostbyCategory (categoriesName) {
      return new Promise((resolve) => {
        axios.get(`Post/${categoriesName}`,categoriesName,config)
          .then(response => {
            resolve(response.data);
          })
      })
    },
    createPost (item) {
      return new Promise((resolve) => {
        axios.post(`Post`,item,config)
          .then(response => {
            resolve(response.data);
          })
      })
    },
    editPost (item) {
      return new Promise((resolve) => {
        axios.put(`Post`,item,config)
          .then(response => {
            resolve(response.data);
          })
      })
    },
    deletePost (slug) {
      return new Promise((resolve) => {
        axios.delete(`Post/${slug}`,config)
          .then(response => {
            resolve(response.data);
          })
      })
    }
  }

  export default postService
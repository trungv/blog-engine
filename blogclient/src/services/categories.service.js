import axios from "axios"

axios.defaults.baseURL = 'http://localhost:44367/api/'

const token = window.localStorage.getItem('token')
const config = {
  headers: {
    Authorization :`Bearer ${token}`
  }  
}

const categoriesService = {
    getCategories () {
      return new Promise((resolve) => {
        axios.get('Categories')
          .then(response => {
            resolve(response.data);
          })
      })
    },
    editCategories (categoryItem) {
      return new Promise((resolve) => {
        axios.put('Categories', categoryItem, config)
          .then(response => {
            resolve(response.data);
          })
      })
    },
    createCategories (categoryItem) {
      return new Promise((resolve) => {
        axios.post('Categories', categoryItem, config)
          .then(response => {
            resolve(response.data);
          })
      })
    },
    deleteCategories (categoriesName) {
      return new Promise((resolve) => {
        axios.delete(`Categories/${categoriesName}`,config)
          .then(response => {
            resolve(response.data);
          })
      })
    }
  }

  export default categoriesService
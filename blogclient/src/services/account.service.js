import axios from "axios"

const accountService = {
    login (user) {
        return new Promise((resolve) => {
          axios.post('http://localhost:9000/api/User', user)
            .then(response => {
              resolve(response.data);
            })
        })
      }
  }

  export default accountService
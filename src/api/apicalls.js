import { API, Auth } from 'aws-amplify';

const baseApiUrl = '/todoitems/';
const idApiUrl = '/todoitem/';
const apiName = "apitest";

const api = {
  async fetchTodoItems () {    
    const path = baseApiUrl;
    const myInit = { 
      response: true,
      isBase64Encoded: true,
      headers: { 
        Authorization: `${(await Auth.currentSession()).getIdToken().getJwtToken()}`,
      },
    };
    console.log("request ", myInit);
    const data = await API
                          .get(apiName, path, myInit)
                          .then(response => {
                            console.log("Debug :: Response: ", response.data);
                            console.log("Debug :: Status: ", response.status);
                            return response.data
                          })
                          .catch(error => {
                            console.log("error:", error.response);
                          });
      return data;      
  },
  
  async fetchTodoItem (itemID) {    
    const path = idApiUrl + itemID;
    const myInit = { 
      response: true,
      isBase64Encoded: true,
      headers: { 
        Authorization: `${(await Auth.currentSession()).getIdToken().getJwtToken()}`,
      },
    };
    console.log("request ", myInit);   
    const data = await API
                          .get(apiName, path, myInit)
                          .then(response => {
                            console.log("Debug :: Response: ", response.data);
                            console.log("Debug :: Status: ", response.status);
                            return response.data
                          })
                          .catch(error => {
                            console.log("error:", error.response);
                          });
      return data;      
  },

  async createTodoItem(todoBody) {
    const path = baseApiUrl
    const myInit = { 
      response: true,
      isBase64Encoded: true,
      body: todoBody,
      'Content-Type': 'application/json',
      headers: { 
        Authorization: `${(await Auth.currentSession()).getIdToken().getJwtToken()}`,
      },
    };
    console.log("request ", myInit);   
    const data = await API
                          .post(apiName, path, myInit)
                          .then(response => {
                            console.log("Debug :: Response: ", response.data);
                            console.log("Debug :: Status: ", response.status);
                            return response.data
                          })
                          .catch(error => {
                            console.log("error:", error.response);
                          });
      return data;      
  },
}

export default api;
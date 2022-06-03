<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
      <button v-on:click="pingDummy">Send Dummy Get</button>
      <button v-on:click="pingApitest">Send Apitest Get</button>
      <br />
      {{ info }}
  </div>
</template>

<script>
import { API, Auth } from 'aws-amplify'

export default {
  name: 'ApiCall',
  props: {
    msg: String
  },
  data: function() {
    return {
      info: null
    };
  },
  methods: {
    pingApitest: async function () {
      const apiName = "apitest";
      const path = "/api/v1/1";
      const myInit = { 
        response: true,
        httpMethod: "GET",
        isBase64Encoded: true,
        pathParameters: {
          "id": "1"
        },
        headers: { 
          Authorization: `${(await Auth.currentSession()).getIdToken().getJwtToken()}`,
        },
      };
      console.log("request ", myInit);
      API
        .get(apiName, path, myInit)
        .then(response => {
          this.info = response.data;
          console.log("response: ", response.data);
          console.log("Status: ", response.status);
        })
        .catch(error => {
          console.log("error:", error.response);
        });
    },
    pingDummy: async function () {
      const apiName = "apitest";
      const path = "/dummy";
      const myInit = { 
        response: true,
        headers: { 
          Authorization: `${(await Auth.currentSession()).getIdToken().getJwtToken()}`,
        },
      };
      console.log("request ", myInit);
      API
        .get(apiName, path, myInit)
        .then(response => {
          this.info = response.data;
          console.log("response: ", response.data);
          console.log("Status: ", response.status);
        })
        .catch(error => {
          console.log("error:", error.response);
        });
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>

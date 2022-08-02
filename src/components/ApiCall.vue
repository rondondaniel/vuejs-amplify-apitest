<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
      <button v-on:click="createItem">Create Todo Item</button>
      <button v-on:click="fetchItems">Fetch All Todo Items</button>
      <button v-on:click="fetchItem">Fetch Todo Item</button>
      <br />
      {{ info }}
  </div>
</template>

<script>
import api from '@/api/apicalls'

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
    fetchItems: async function () {
      this.info = await api.fetchItems();
    },
    fetchItem: async function () {
      this.info = await api.fetchTodoItem("8");
    },
    createItem: async function () {
      const body = {
            id: 8,
            name: 'Test Me',
            isComplete: false,
      }
      this.info = await api.createTodoItem(body);
    },    
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

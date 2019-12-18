<template>
  <div>

    <div v-if="user && configuration">
      <h4>{{user.name}}'s Configuration</h4>
      <ul>
        <li v-for="config in configuration">{{config}}</li>
      </ul>
    </div>
    <button @click="getConfiguration" v-if="user">Refresh</button>
  </div>
</template>

<script>
export default {
  name: "Configuration",
  data: function() {
    return { configuration: null };
  },
  methods: {
    async getConfiguration() {
      this.configuration = await this.$RdostrService.getConfiguration(
        this.$AuthService
      );
      console.log(this.configuration);
    }
  },
  computed: {
    user: function() {
      return this.$AuthService.getUser();
    }
  },
  created: function(){
      if (this.user) this.getConfiguration()
  }
};
</script>

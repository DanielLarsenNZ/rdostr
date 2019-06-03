<template>
  <div>

    <div v-if="user && stations">
      <h4>{{user.name}}'s Stations</h4>
      <ul>
        <li v-for="station in stations">{{station}}</li>
      </ul>
    </div>
    <button @click="getStations" v-if="user">Refresh</button>
  </div>
</template>

<script>
export default {
  name: "Stations",
  data: function() {
    return { stations: null };
  },
  methods: {
    async getStations() {
      this.stations = await this.$RdostrService.getStations(
        this.$AuthService
      );
      console.log(this.stations);
    }
  },
  computed: {
    user: function() {
      return this.$AuthService.getUser();
    }
  },
  created: function(){
      if (this.user) this.getStations()
  }
};
</script>

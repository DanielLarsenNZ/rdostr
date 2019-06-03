<template>
  <div id="app">
    <HelloWorld/>
    <Stations/>
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld'
import Stations from './components/Stations'

export default {
  name: 'App',
  components: {
    HelloWorld,
    Stations
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  margin-top: 60px;
}
</style>

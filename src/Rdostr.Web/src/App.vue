<template>
  <div id="app">
    <HelloWorld/>
    <Stations/>
    <Configuration/>
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld'
import Stations from './components/Stations'
import Configuration from './components/Configuration'

export default {
  name: 'App',
  components: {
    HelloWorld,
    Stations,
    Configuration
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

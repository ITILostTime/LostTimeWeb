<template>
  <div id="app">
    <app-header></app-header>
    <div class="container">
      <router-view class="child"></router-view>
    </div>
    <app-footer></app-footer>
  </div>
</template>

<script>
import Header from './Includes/Header.vue'
import Footer from './Includes/Footer.vue'

import AuthService from '../services/AuthService'
import { mapGetters, mapActions } from 'vuex'
import '../directives/requiredProviders'

export default {
  name: 'app',
  components: {
      'app-header': Header,
      'app-footer': Footer,
    },
  computed: {   
    auth: () => AuthService
  },
  
}
</script>

<style lang="less" scoped>
  .progress {
    margin: 0px;
    padding: 0px;
    height: 5px;
  }

  a.router-link-active {
    font-weight: bold;
  }
</style>
<style lang="less">
  @import "../styles/global.less";
body{
  //background:#382f24;
  background-image:url('../../dist/img/wood-diff.jpg');
    color:black;
    font-family:"Sylfaen", serif;

  
}
#app .backgrey
{
    background:#544545;
}
#app .row
{
    padding-bottom:10px;
}
a
{
  color:darkgrey;
}
</style>
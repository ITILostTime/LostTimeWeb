<template>
  <div>
    <nav class="navbar navbar-default">
      <div class="container-fluid">
        <div class="navbar-header">
          <a class="navbar-brand" href="#">
            <i class="fa fa-laptop"></i>
            LostTimeWeb
          </a>
        </div>
        <ul class="nav navbar-nav navbar-right">
          <li><router-link to="/">Home</router-link></li>
          <li><router-link to="/support">Support</router-link></li>
          <li><router-link to="/download">Download</router-link></li>
          <li><router-link to="/forum">Forum</router-link></li>
          <li><router-link to="/login">Login</router-link></li>
        </ul>
      </div>
    </nav>

      <div class="container">
        <router-view class="child"></router-view>
      </div>
      <div>
        <router-link to="/about">A-propos</router-link>
      </div>
  </div>
</template>

<script>

import AuthService from '../services/AuthService'
import { mapGetters, mapActions } from 'vuex'
import '../directives/requiredProviders'

export default {
  computed: {   
    auth: () => AuthService
  }
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
</style>
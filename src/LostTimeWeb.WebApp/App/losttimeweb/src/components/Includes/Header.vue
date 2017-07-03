<template>
    <header>
        <nav class="navbar navbar-default " id="head">
            <div class="container-fluid">
                <div class="navbar-header">
                    <router-link to="/">
                        <img src="../../../dist/img/logo_noParticle_shadow.png" alt="brand"/> 
                    </router-link>
                </div>
                <ul class="nav navbar-nav navbar-center ">
                    <li class="brand-title"><router-link to="/" >LostTime</router-link></li>
                    <li><router-link to="/">Accueil</router-link></li>
                    <li><router-link to="/download">Téléchargement</router-link></li>                    
                    <li><router-link to="/support">Support</router-link></li>
                    <li  v-if="auth.isConnected" ><router-link :to="`/user/${auth.id}`">Profil</router-link></li>
                    <!--<li><router-link to="/forum">Forum</router-link></li>-->                    
                    <!--<li><router-link to="/tchat">Tchat</router-link></li>-->
                    <li  v-if="auth.isConnected"><router-link to="/quest">Quètes</router-link></li>
                    <li ><router-link to="/admin" v-if="auth.role== 'ADMIN' && auth.isConnected">Administration</router-link></li>
                    <li  v-if="!auth.isConnected"><router-link to="/login">Se connecter</router-link></li>
                </ul>
                <div class="collapse navbar-collapse" ><!--IF CONNECTED -->
                    <ul class="nav navbar-nav navbar-right"  v-if="auth.isConnected">
                        <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">{{ auth.email }} <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><router-link to="/logout">Se déconnecter</router-link></li>
                        </ul>
                        </li>
                    </ul>
                </div>
            </div> <!--div contaier fluid-->
        </nav>
    </header>
</template>
<script>
import AuthService from '../../services/AuthService'
import { mapGetters, mapActions } from 'vuex'
import '../../directives/requiredProviders'

export default {
    name: 'header',
    computed: {
    auth: () => AuthService,
    //...mapGetters(['isLoading'])
  }
}
</script>
<style lang="less">

.navbar
{
    background-color:grey;
    border:darkgrey;
    max-height:50px;
    font-color:black;
}
#head a
{
  color:white;
}
#head img
{
     max-height:150px;
     transform:scale(1.5) translateY(10px);
}
#head .navbar-header
{
    position:absolute;
    z-index:2000;
    //background-color:#9f9080;
    background-color:none;
    max-width:180px; 
    //border: 2px solid;
    //border-bottom-left-radius: 2em;
    //border-bottom-right-radius: 2em;
    padding:5px;
    //box-shadow:2px 2px 6px black;
}
#head .brand-title
{
    margin-top:-5px;
    padding-left:30px;
    padding-top:8px;
    height:80px;
    width:220px;
    background:brown;
}
#head .brand-title a
{
    font-family:"The Goldsmith Vintage", "Impact", serif;
    font-size:4em;
    text-decoration:none;
    color:#493836;
    background: -webkit-linear-gradient(#c6823c, #483e2f);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    padding-bottom:20px;
}
.navbar-center
{
    margin-left:130px;
}
</style>
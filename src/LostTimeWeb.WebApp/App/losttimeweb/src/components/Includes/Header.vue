<template>
    <header id="head">
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <router-link to="/" class="navbar-brand">
                        <img src="../../../dist/img/logo_noParticle_shadow.png" alt="brand"/> 
                    </router-link>
                    <span class="brand-title"><router-link to="/" >LostTime</router-link></span>
                </div>
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav navbar-center">
                        <li><router-link to="/">Accueil</router-link></li>
                        <li><router-link to="/download">Téléchargement</router-link></li>                    
                        <li><router-link to="/support">Support</router-link></li>
                        <li><router-link to="/about">Developpement</router-link></li>
                        <li  v-if="auth.isConnected" ><router-link :to="`/user/${auth.id}`">Profil</router-link></li>
                        <li  v-if="auth.isConnected"><router-link to="/quest">Quètes</router-link></li>
                        <li ><router-link to="/admin" v-if="auth.role== 'ADMIN' && auth.isConnected">Administration</router-link></li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li v-if="!auth.isConnected"><router-link to="/login">Se connecter</router-link></li>
                        <li class="dropdown" v-if="auth.isConnected">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">{{ auth.email }} <span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><router-link to="/logout">Se déconnecter</router-link></li>
                                </ul>
                            </li>
                        </li>
                    </ul>
                </div><!-- /.navbar-collapse -->
            </div><!-- /.container-fluid -->
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
#head
{
    position:relative;
    z-index:800;
}
.navbar
{
    //background-color:grey;
    border:darkgrey;
    max-height:50px;
    //font-color:black;
    box-shadow:black 2px 2px 8px;
}
#head a
{
  //color:white;
}
#head img
{
     max-height:150px;
     transform:scale(1.5) translateY(10px);
}
#head .navbar-brand
{
    display:inline-block;
    position:absolute;
    background:none;
    max-width:180px; 
    //border: 2px solid;
    z-index:2000;

    padding:5px;
    //box-shadow:2px 2px 6px black;
}
#head .brand-title
{
    display:inline-block;
    margin-top:-20px;
    padding-top:10px;
    padding-left:25px;
    //padding-top:8px;
    height:80px;
    width:200px;
    margin-left:150px;
    //background:brown;
    //z-index:1000;
    
    background-color:#27221f;
    border-bottom-left-radius: 2em;
    border-bottom-right-radius: 2em;
}
#head .in,.collapsing
{
    background:grey;
    z-index:3000;
    //text-align:right;
    width:200px;
    position: absolute;
    right:15px;
    top:50px;

}
#head .brand-title a
{
    //display:inline-block;
    font-family:"the_goldsmith_vintagevintage", "Impact", serif;
    font-size:4em;
    text-decoration:none;
    color:#493836;
    background: -webkit-linear-gradient(#c6823c, #483e2f);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    //padding-bottom:20px;
}
@media (max-width: 768px) 
{
    #head .navbar-brand img
    {
        transform:scale(1.0) translate(-20px, -20px);
    }
    #head .brand-title
    {
        padding-left:20px;
        margin-left:120px;
    }
}
@media (max-width:  420px) 
{
    #head .navbar-brand
    {
        display:none;
    }
    #head .brand-title
    {
        padding-left:20px;
        margin-left:0px;
    }
}
</style>